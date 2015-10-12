'use strict';

$('.dtpicker').pickadate({
    format: 'yyyy-mm-dd'
});

$('.tmpicker').pickatime({
    min: [7,30],
    max: [18,0]
    });

$('#frmProductsAll').hide();
$('#btnShowProducts').click(function(){
    $('#frmProductsAll').show();
    $('#frmProductsButton').hide();
});

$('#frmVouchersAll').hide();
$('#btnShowVouchers').click(function(){
    $('#frmVouchersAll').show();
    $('#frmVouchersButton').hide();
});

$(".btnUpload").click(function() {
    var input = $(document.createElement('input'));
    input.attr("type", "file");
    input.trigger('click');
    return false;
});

$('.btnDownload').on('click', function() {
    var a = document.createElement('a');
    a.download = 'myImage.png';
    a.href = '/images/eSalon.png';
    a.click();
});

$(function () { $('#roleTree').jstree({
    "core" : {
        "themes" : {
            "variant" : "large"
        }
    },
    "checkbox" : {
        "keep_selected_style" : false
    },
    "plugins" : [ "wholerow", "checkbox" ]
    });
});

$(function addRowHandlers() {
    var table = document.getElementsByClassName("table-radio");
    for (var i = 0; i < table.length; i++) {
        var rows = table[i].getElementsByTagName("tr");
        for (var j = 0; j < rows.length; j++) {
            var currentRow = table[i].rows[j];
            var createClickHandler =
                function(row)
                {
                    return function() {
                        row.getElementsByTagName("input")[0].checked = "checked";
                     };
                };

            currentRow.onclick = createClickHandler(currentRow);
        };
    };

    $('.table-radio tr').click(function(){
         $('.active').removeClass('active')
         $(this).addClass("active");
    });
});

$(function getCurrentDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth()+1; //January is 0!
    var yyyy = today.getFullYear();

    if(dd<10) {
        dd='0'+dd
    }

    if(mm<10) {
        mm='0'+mm
    }

    today = yyyy+'-'+mm+'-'+dd;
    return today;
});

