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

        btn = document.getElementById("btnSend");
        btn.addEventListener("click", preparMail, false);

        btn = document.getElementById("btnCancel");
        btn.addEventListener("click", (function (e) {
            this.disabled = true;
            if (asynCancel != null)
            {// cancel operation
                asynCancel.cancel();
            }
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

        document.getElementById("text_c").innerText = "Hi,\r\n\r\nThis is a simple test email sent from Javascript Windows 8 Store Applicatoin (Metro) project.\r\nPlease do not reply.\r\n";
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

    function preparMail(eventInfo) {

        document.getElementById("pgBar").value = 0;
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
        var cc = document.getElementById("textCc").value.trim();
        if (to == "" && cc == "") {
            (new Windows.UI.Popups.MessageDialog("Please input a recipient at least!", "Error Information")).showAsync();
            document.getElementById("textTo").value = "";
            document.getElementById("textCc").value = "";
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


        // For evaluation usage, please use "TryIt" as the license code, otherwise the 
        // "Invalid License Code" exception will be thrown. However, the trial object only can be used 
        // with developer license

        // For licensed usage, please use your license code instead of "TryIt", then the object
        // can used with published windows store application.

        // if trial version is expired or invalid license code, it will throw an exception.
        var oMail = new EASendMailRT.SmtpMail("TryIt");
        
        var oServer = new EASendMailRT.SmtpServer(server);
        if (bAuth) {
            oServer.user = user;
            oServer.password = password;
        }

        if (bSSL) {
            oServer.connectType = EASendMailRT.SmtpConnectType.connectSSLAuto;
        }

        var oMail = new EASendMailRT.SmtpMail("TryIt");
        oMail.from = new EASendMailRT.MailAddress(from);
        oMail.to = new EASendMailRT.AddressCollection(to);
        oMail.cc = new EASendMailRT.AddressCollection(cc);
        oMail.subject = document.getElementById("textSubject").value;

        // add attachments
        var promiseArray = m_atts.map(oMail.addAttachmentAsync, oMail);
        WinJS.Promise.join(promiseArray)
        .then(function (e) {
            // text/plain
            if (document.getElementById("lstFormat").value == 0) {

                oMail.textBody = document.getElementById("text_c").innerText;
                document.getElementById("btnSend").disabled = true;
                document.getElementById("btnCancel").disabled = false;

                sendMail(oServer, oMail);
                return;
            }
           
            // text/html
            var html = editor.document.body.innerHTML;
            html = "<html><head><meta charset=\"utf-8\" /></head><body style=\"font-family:Calibri;font-size: 15px;\">" + html + "<body></html>";

            // import html to body text and also add the images linked in the html to embedded images automatically.
            oMail.importHtmlAsync(html,
                             "",
                             (EASendMailRT.ImportHtmlBodyOptions.importLocalPictures |
                             EASendMailRT.ImportHtmlBodyOptions.importHttpPictures |
                             EASendMailRT.ImportHtmlBodyOptions.importCss))
        .then(function (e) {
                                 document.getElementById("btnSend").disabled = true;
                                 document.getElementById("btnCancel").disabled = false;
                                 asynCancel = null;
                                 sendMail(oServer, oMail);
                             });
        },

        // error handle
        function (e)
        {
            if (oMail.lastErrorMessage != "") {
                document.getElementById("textStatus").innerText = oMail.lastErrorMessage;
            }
            else {
                document.getElementById("textStatus").innerText = e[0].message;
            }
            return;
        }

        );
    }


    function sendMail( oServer, oMail )
    {
        var oSmtp = new EASendMailRT.SmtpClient();

        // EASendMail Events Handlers
        oSmtp.addEventListener("connected",
            (function (e) {
                document.getElementById("textStatus").innerText = "Connected";
            }));

        oSmtp.addEventListener("authorized",
            (function (e) {
                document.getElementById("textStatus").innerText = "Authorized";
            }));

        oSmtp.addEventListener("securing",
            (function (e) {
                document.getElementById("textStatus").innerText = "Securing...";
            }));

        oSmtp.addEventListener("sendingdatastream",
            (function (e) {
                document.getElementById("textStatus").innerText = "Sending data ...";
                var pg = document.getElementById("pgBar");
                pg.max = e.total;
                pg.value = e.sent;
            }));

        document.getElementById("textStatus").innerText = "Connecting ...";

        // get a cancellation handler
        asynCancel = oSmtp.sendMailAsync(oServer, oMail).then(function (e) {
            document.getElementById("btnSend").disabled = false;
            document.getElementById("btnCancel").disabled = true;

            document.getElementById("textStatus").innerText = "Completed";
            oSmtp.close();
        },
        // error handle
        function (e) {

            document.getElementById("btnSend").disabled = false;
            document.getElementById("btnCancel").disabled = true;

            // because javascript exception only gives the stack trace messages, but it is not
            // real description of exception, so we give a property lastErrorMessage for javascript.
            if (oSmtp.lastErrorMessage != "") {
                document.getElementById("textStatus").innerText = oSmtp.lastErrorMessage;
            }
            else {
                document.getElementById("textStatus").innerText = e.message;
            }
            oSmtp.close();
        }
        );

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