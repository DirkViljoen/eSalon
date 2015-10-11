//  ===============================================================================
// |    THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF      |
// |    ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO    |
// |    THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A         |
// |    PARTICULAR PURPOSE.                                                    |
// |    Copyright (c)2013  ADMINSYSTEM SOFTWARE LIMITED                         |
// |
// |    Project: It demonstrates how to use EASendMail to send text/html email with 
// |             javascript in Windows 8 Application.
// |
// |    File: Form1 : implementation file
// |
// |    Author: Ivan Lui ( ivan@emailarchitect.net )
//  ===============================================================================
(function () {
    "use strict";
    var appViewState = Windows.UI.ViewManagement.ApplicationViewState;
    var ui = WinJS.UI;
    var htmlinited = false;
    var editor;
    var asynCancel = null;
    var m_atts = new Array();
    var m_task = new Array();
    var arTo = new Array();
    var m_curTask = 0;
    var m_cancel = false;
    var m_total = 0;
    var m_success = 0;
    var m_failed = 0;

    Array.prototype.removeByVal = function (val) {
        for (var i = 0; i < this.length; i++) {
            if (this[i] == val) {
                this.splice(i, 1);
                i--;
                break;
            }
        }
        return this;
    }

    ui.Pages.define("/default.html", {
        // This function is called whenever a user navigates to this page. It
        // populates the page elements with the app's data.
        ready: function (element, options) {
            init_gui();
        },

        unload: function () {

        }

    });

    function init_gui() {
        var btn = document.getElementById("btnCancel");
        btn.setAttribute("disabled", "disabled");

        var lstFormat = document.getElementById("lstFormat");
        lstFormat.addEventListener("change", onbodyformatchange, false);


        var rThreads = document.getElementById("rThreads");
        document.getElementById("displaythreads").innerText = rThreads.value;
        rThreads.addEventListener("change", (function (e) {
            document.getElementById("displaythreads").innerText = this.value;
        }), false);


        btn = document.getElementById("btnSend");
        btn.addEventListener("click", preparMail, false);

        btn = document.getElementById("btnCancel");
        btn.addEventListener("click", (function (e) {
            m_cancel = true;
            this.disabled = true;
            m_task.map((function(e)
            {
                e.cancel();
                m_curTask--;
            }));
        }), false);

        btn = document.getElementById("btnClose");
        btn.addEventListener("click", (function (e) {
            document.getElementById("divResult").style.display = "none";
        }), false);

        document.getElementById("btnClear").addEventListener("click",
            (function (e) {
                m_atts = new Array();
                document.getElementById("textAtts").value = "";
            }), false);


        document.getElementById("btnAtts").addEventListener("click",
            (function (e) {

                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.viewMode = Windows.Storage.Pickers.PickerViewMode.List;
                picker.suggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.picturesLibrary;
                picker.fileTypeFilter.replaceAll([".png", ".jpg", ".jpeg", ".gif", ".bmp", ".txt"]);

                var atts = document.getElementById("textAtts").value;

                picker.pickMultipleFilesAsync().then(function (files) {
                    var count = files.length;
                    for (var i = 0; i < count; i++) {
                        var file = files[i];
                        m_atts.push(file.path);
                        atts += file.name;
                        atts += ";";
                    }
                    document.getElementById("textAtts").value = atts;
                });

            }), false);

        document.getElementById("textAtts").disabled = true;

        document.getElementById("textUser").disabled = true;
        document.getElementById("textPassword").disabled = true;

        document.getElementById("chkAuth").addEventListener("click",
            (function (e) {
                document.getElementById("textUser").disabled = !this.checked;
                document.getElementById("textPassword").disabled = !this.checked;
            }), false);

        document.getElementById("text_c").innerText = "Hi,\r\n\r\nThis is a simple test email sent from Javascript Windows 8 Store Applicatoin (Metro) project with multiple threads.\r\nPlease do not reply.\r\n";
    }

    function onbodyformatchange(eventInfo) {

        var lstFormat = document.getElementById("lstFormat");
        var i = lstFormat.selectedIndex;
        if (i == 0) {
            document.getElementById("textComposer").style.display = "";
            document.getElementById("htmlComposer").style.display = "none";
            if (htmlinited) {

                if (document.getElementById("chkHTML").checked) {
                    editor.document.body.innerHTML = editor.document.body.innerText;
                    document.getElementById("chkHTML").checked = false;
                }
                document.getElementById("text_c").innerText = editor.document.body.innerText;
                hide_editor_submenu();
            }
        }
        else {
            document.getElementById("textComposer").style.display = "none";
            document.getElementById("htmlComposer").style.display = "";
            if (!htmlinited) {
                init_htmleditor();
            }
            else {
                editor.document.body.innerText = document.getElementById("text_c").innerText;
            }
        }
    }

    function addAttachments( oServer, oMail, atts, index )
    {
        if (atts.length == 0) {
            sendMail(oServer, oMail, index);
            return;
        }

        var promiseArray = atts.map((function (e) {
            return oMail.addAttachmentAsync(e);
        }));
       
        WinJS.Promise.join(promiseArray).then(function () {
            sendMail(oServer, oMail, index);
        });
    }

    function importBody(oServer, oMail, atts, index, bodytext )
    {
        oMail.importHtmlAsync(bodytext,
                             "",
                             (EASendMailRT.ImportHtmlBodyOptions.importLocalPictures |
                             EASendMailRT.ImportHtmlBodyOptions.importHttpPictures |
                             EASendMailRT.ImportHtmlBodyOptions.importCss))
        .then(function () {
            addAttachments(oServer, oMail, atts, index);
        });
    }

    function submitMail(from,
         subject, server, user, password, auth, ssl,
         bodyformat, bodytext, atts, startindex)
    {
        var tb = document.querySelector("#divResult div table tbody");
        for (var i = startindex; i < arTo.length; i++) {
            var maxthreads = document.getElementById("rThreads").value;
            if (m_curTask >= maxthreads)
            {
                setTimeout((function () { submitMail(from, subject, server, user, password, auth, ssl, bodyformat, bodytext, atts, i);}), 100);
                return;
            }
           
            var index = i + 1;
            var tr = document.createElement("tr");
            if (m_cancel) {
               
                tr.innerHTML = "<td>" + index + "</td><td>" + arTo[i] + "</td><td id =\"lstStatus" + index + "\" >Operation was cancelled!</td>";
                tb.appendChild(tr);
                continue;
            }

            tr.innerHTML = "<td>" + index + "</td><td>" + arTo[i] + "</td><td id =\"lstStatus" + index + "\" >Queued</td>";
            tb.appendChild(tr);
             

            m_curTask++;
            var maxthreads = document.getElementById("rThreads").value;
            var addr = arTo[i];
            // For evaluation usage, please use "TryIt" as the license code, otherwise the 
            // "Invalid License Code" exception will be thrown. However, the trial object only can be used 
            // with developer license

            // For licensed usage, please use your license code instead of "TryIt", then the object
            // can used with published windows store application.

            // if trial version is expired or invalid license code, it will throw an exception.
            var oMail = new EASendMailRT.SmtpMail("TryIt");
            oMail.from = new EASendMailRT.MailAddress(from);
            oMail.to = new EASendMailRT.AddressCollection(addr);
            oMail.subject = subject;


            var oServer = new EASendMailRT.SmtpServer(server);
            if (auth) {
                oServer.user = user;
                oServer.password = password;
            }

            if (ssl) {
                oServer.connectType = EASendMailRT.SmtpConnectType.connectSSLAuto;
            }

            
            if (bodyformat == 0) {
                oMail.textBody = bodytext;
                addAttachments(oServer, oMail, atts, index);
            }
            else {
                var html = bodytext;
                html = "<html><head><meta charset=\"utf-8\" /></head><body style=\"font-family:Calibri;font-size: 15px;\">" + html + "<body></html>";
                importBody(oServer, oMail, atts, index, html);
            }
        }

        if (m_curTask > 0)
        {
            setTimeout((function () { submitMail(from, subject, server, user, password, auth, ssl, bodyformat, bodytext, atts, arTo.length); }), 100);
            return;
        }

        document.getElementById("btnSend").disabled = false;
        document.getElementById("btnCancel").disabled = true;
        document.getElementById("btnClose").style.display = "";

    }

    function preparMail(eventInfo) {

        document.getElementById("textStatus").innerText = "Ready";
        if (document.getElementById("chkHTML").checked && htmlinited) {
            editor.document.body.innerHTML = editor.document.body.innerText;
            document.getElementById("chkHTML").checked = false;
        }

        var from = document.getElementById("textFrom").value.trim();
        if (from == "") {
            (new Windows.UI.Popups.MessageDialog("Please input From address!", "Error Information")).showAsync();
            document.getElementById("textFrom").value = "";
            document.getElementById("textFrom").focus();
            return;
        }

        var to = document.getElementById("textTo").value.trim();
       
        if (to == "") {
            (new Windows.UI.Popups.MessageDialog("Please input a recipient at least!", "Error Information")).showAsync();
            document.getElementById("textTo").value = "";
            document.getElementById("textTo").focus();
            return;
        }

        var server = document.getElementById("textServer").value.trim();
        if (server == "") {
            (new Windows.UI.Popups.MessageDialog("Please input server address", "Error Information")).showAsync();
            document.getElementById("textServer").value = "";
            document.getElementById("textServer").focus();
            return;
        }

        var bAuth = document.getElementById("chkAuth").checked;
        var bSSL = document.getElementById("chkSSL").checked;
        var user = document.getElementById("textUser").value.trim();
        var password = document.getElementById("textPassword").value.trim();
        if (bAuth) {
            if (user == "") {
                (new Windows.UI.Popups.MessageDialog("Please input user name!", "Error Information")).showAsync();
                document.getElementById("textUser").value = "";
                document.getElementById("textUser").focus();
                return;
            }

            if (password == "") {
                (new Windows.UI.Popups.MessageDialog("Please input password!", "Error Information")).showAsync();
                document.getElementById("textPassword").value = "";
                document.getElementById("textPassword").focus();
                return;
            }
        }

        var ar = to.split("\n");

        arTo = new Array();
        for (var i = 0; i < ar.length; i++) {
            var addr = ar[i].trim();
            if (addr != "")
                arTo.push(addr);
        }

        if (arTo.length == 0) {
            (new Windows.UI.Popups.MessageDialog("Please input a recipient at least!", "Error Information")).showAsync();
            document.getElementById("textTo").value = "";
            document.getElementById("textTo").focus();
            return;
        }

        var subject = document.getElementById("textSubject").value;
        var bodyformat = document.getElementById("lstFormat").value;
        var bodytext = "";
        if (bodyformat == 0) {
            bodytext = document.getElementById("text_c").innerText;
        }
        else {
            bodytext = editor.document.body.innerHTML;
        }

        document.getElementById("btnSend").disabled = true;
        document.getElementById("btnCancel").disabled = false;

        document.getElementById("btnClose").style.display = "none";
        var results = document.getElementById("divResult");
        results.style.position = "absolute";
        results.style.top = 0;
        results.style.left = 0;
        results.style.display = "";

        var tb = document.querySelector("#divResult div table tbody");
        tb.innerHTML = "";
        m_curTask = 0;
        m_task = new Array();
        m_cancel = false;

        m_total = arTo.length;
        m_success = 0;
        m_failed = 0;
        document.getElementById("textStatus").innerText = "Total: " + m_total + ", success: " + m_success + ", failed: " + m_failed;
        submitMail(from, subject, server, user, password, bAuth, bSSL, bodyformat, bodytext, m_atts, 0);
    }

    function updateStatus(index, status){
        document.getElementById("lstStatus" + index).innerText = status;
    }

    function sendMail( oServer, oMail, index )
    {
        var oSmtp = new EASendMailRT.SmtpClient();

        // EASendMail Events Handlers
        oSmtp.addEventListener("connected",
            (function (e) {
                updateStatus( index, "Connected" );
            }));

        oSmtp.addEventListener("authorized",
            (function (e) {
                updateStatus( index, "Authorized" );
            }));

        oSmtp.addEventListener("securing",
            (function (e) {
                updateStatus( index, "Securing...");
            }));

        oSmtp.addEventListener("sendingdatastream",
            (function (e) {
                updateStatus( index, "Sending data " + e.sent + "/" + e.total + " ..." );
            }));

        updateStatus( index,"Connecting ..." );

        // get a cancellation handler;
        asynCancel = oSmtp.sendMailAsync(oServer, oMail).then(function (e) {
            updateStatus( index, "Completed");
            oSmtp.close();
            m_curTask--;

            m_success++;
            document.getElementById("textStatus").innerText = "Total: " + m_total + ", success: " + m_success + ", failed: " + m_failed;
        },
        // error handle
        function (e) {
            // because javascript exception only gives the stack trace messages, but it is not
            // real description of exception, so we give a property lastErrorMessage for javascript.
            if (oSmtp.lastErrorMessage != "") {
                updateStatus( index, oSmtp.lastErrorMessage );
            }
            else {
                updateStatus( index,  e.message);
            }
            oSmtp.close();
            m_curTask--;
            m_failed++;
            document.getElementById("textStatus").innerText = "Total: " + m_total + ", success: " + m_success + ", failed: " + m_failed;
        }
        );

        m_task.push(asynCancel);

    }

    //HTML Editor

    function hide_editor_submenu() {
        document.getElementById("sizepicker").style.display = "none";
        document.getElementById("fontpicker").style.display = "none";
        document.getElementById("colorpicker").style.display = "none";
        document.getElementById("linkPanel").style.display = "none";
    }

    function init_htmleditor() {

        editor = document.getElementById("html_c").contentWindow;
        editor.document.designMode = "On";
        editor.document.contentEditable = true;
        editor.document.open();
        editor.document.write("<div>&nbsp;</div>");
        editor.document.close();
        editor.document.body.style.fontFamily = "Calibri"
        editor.document.body.style.fontSize = "16px";
        editor.document.charset = "utf-8";

        htmlinited = true;
        editor.document.body.innerText = document.getElementById("text_c").innerText;

        var vcmd = document.getElementById("e_bold");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("Bold", false, "");
            editor.focus();
        }), false);


        vcmd = document.getElementById("e_italic");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("Italic", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_underline");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("Underline", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_insertorderedlist");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("InsertOrderedList", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_insertunorderedlist");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("InsertUnorderedList", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_outdent");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("Outdent", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_indent");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("Indent", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_justifyleft");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("JustifyLeft", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_justifycenter");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("JustifyCenter", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_justifyright");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("JustifyRight", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_hr");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            editor.document.execCommand("InsertHorizontalRule", false, "");
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_font");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            var vmenu = document.getElementById("fontpicker");
            if (vmenu.style.display == "none") {
                var fonts = vmenu.querySelectorAll("a");
                for (var i = 0; i < fonts.length; i++) {
                    fonts[i].style.fontFamily = fonts[i].innerText;
                }
                hide_editor_submenu();
                vmenu.style.display = "";
            }
            else {
                vmenu.style.display = "none";
            }

            editor.focus();
        }), false);

        vcmd = document.getElementById("e_size");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            var vmenu = document.getElementById("sizepicker");
            if (vmenu.style.display == "none") {
                vmenu.style.width = "100px";
                hide_editor_submenu();
                vmenu.style.display = "";
            }
            else {
                vmenu.style.display = "none";
            }
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_color");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            var vmenu = document.getElementById("colorpicker");
            if (vmenu.style.display == "none") {
                vmenu.style.width = "500px";
                var colls = this.parentElement.querySelectorAll("#colorpicker div a");
                for (var i = 0; i < colls.length; i++) {
                    colls[i].style.backgroundColor = colls[i].getAttribute("title");
                }
                hide_editor_submenu();
                vmenu.style.display = "";

            }
            else {
                vmenu.style.display = "none";
            }

            editor.focus();
        }), false);


        vcmd = document.getElementById("e_link");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();
            editor.focus();
            var vmenu = document.getElementById("linkPanel");
            if (vmenu.style.display == "none") {
                hide_editor_submenu();
                vmenu.style.display = "";

            }
            else {
                vmenu.style.display = "none";
            }
            editor.focus();
        }), false);

        vcmd = document.getElementById("e_image");
        vcmd.addEventListener("click", (function (e) {
            e.preventDefault();

            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.viewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.suggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.picturesLibrary;
            picker.fileTypeFilter.replaceAll([".png", ".jpg", ".jpeg", ".gif", ".bmp"]);

            picker.pickSingleFileAsync().then(function (file) {
                if (file == null)
                    return;

                var ext = file.path;
                var pos = ext.lastIndexOf('.');
                if (pos != -1) {
                    ext = ext.substring(pos + 1).toLowerCase();
                }

                var ct = "image/jpeg";
                if (ext == "png") {
                    ct = "image/png";
                }
                else if (ext == "gif") {
                    ct = "image/gif";
                }
                else if (ext == "bmp") {
                    ct = "image/bmp";
                }

                Windows.Storage.StorageFile.getFileFromPathAsync(file.path).then(function (f) {
                    Windows.Storage.FileIO.readBufferAsync(f).then(function (buffer) {
                        var data = Windows.Security.Cryptography.CryptographicBuffer.encodeToBase64String(buffer);

                        var filepath = file.path;
                        editor.focus();
                        editor.document.execCommand("insertImage", false, file.path);
                        editor.focus();
                        var images = editor.document.getElementsByTagName("img");

                        for (var i = 0; i < images.length; i++) {
                            var img = images[i];

                            var src = img.src;
                            src = src.replace(/\%20/g, " ").toLowerCase();
                            src = src.replace(/file:[/]+/, "");
                            src = src.replace(/\//g, "\\");

                            if (src == filepath.replace(/\//g, "\\").toLowerCase()) {
                                img.src = "data:" + ct + ";base64," + data;
                                img.alt = file.name;
                                break;
                            }
                        }
                    });
                });

            });


        }), false);

        document.getElementById("mailpage").addEventListener("click",
            (function (e) {

                var pid = e.target.id;
                if (pid != "e_font" && pid != "e_size" && pid !== "img_color" &&
                    pid != "e_color" && pid != "e_link" && pid != "img_link" &&
                    pid != "linksource") {
                    hide_editor_submenu();
                }
            }), false);

        var colls = document.querySelectorAll("#colorpicker div a");
        for (var i = 0; i < colls.length; i++) {
            colls[i].addEventListener("click", (function (e) {
                e.preventDefault();
                editor.focus();
                editor.document.execCommand("forecolor", false, this.getAttribute("title"));
                editor.focus();
            }), false);
        }

        colls = document.querySelectorAll("#fontpicker a");
        for (var i = 0; i < colls.length; i++) {
            colls[i].addEventListener("click", (function (e) {
                e.preventDefault();
                editor.focus();
                editor.document.execCommand("fontname", false, this.innerText);
                editor.focus();
            }), false);
        }

        colls = document.querySelectorAll("#sizepicker a");
        for (var i = 0; i < colls.length; i++) {
            colls[i].addEventListener("click", (function (e) {
                e.preventDefault();
                editor.focus();
                editor.document.execCommand("fontsize", false, this.innerText);
                editor.focus();
            }), false);
        }

        document.getElementById("create_link").addEventListener("click",
            (function (e) {

                var v = document.getElementById("linksource").value;
                if (v != "") {
                    editor.focus();
                    editor.document.execCommand("CreateLink", false, v);
                    editor.focus();
                }
                else {
                    editor.focus();
                    editor.document.execCommand("UnLink", false, "");
                    editor.focus();
                }
            }), false);

        document.getElementById("chkHTML").addEventListener("click",
            (function (e) {

                if (this.checked) {
                    var text = editor.document.body.innerHTML;
                    editor.document.body.innerText = text;
                    editor.document.body.innerHTML = "<div style=\"font-size:12px;font-family:Courier New\">" + editor.document.body.innerHTML + "</div>";

                }
                else {
                    editor.document.body.innerHTML = editor.document.body.innerText;
                }
            }), false);

        editor.document.addEventListener("click",
             (function (e) {
                 hide_editor_submenu();
             }), false);
    }

})();