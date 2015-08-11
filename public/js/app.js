'use strict';

$('.dtpicker').pickadate({})

$('.tmpicker').pickatime({
    min: [7,30],
    max: [18,0]
    })

$(function() {
    $( "#slider" ).slider({
        value:30,
        min: 10,
        max: 180,
        step: 5,
        slide: function( event, ui ) {
            $( "#amount" ).val( ui.value + ' minutes');
        }
    });
    $( '#amount' ).val( $( "#slider" ).slider( "value" ) + ' minutes' );
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
}); });

$(function addRowHandlers() {
    var table = document.getElementsByClassName("table-clickable");
    var i;
    var j;
    alert(table.length);
    for (i = 0; i < table.length; i++) {
        var rows = table[i].getElementsByTagName("tr");
        alert(rows.length);
        for (j = 0; j < rows.length; j++) {
            var currentRow = table[i].rows[j];
            var createClickHandler =
                function(row)
                {
                    return function() {
                        var cell = row.getElementsByTagName("td")[0];
                        var id = cell.innerHTML;
                        alert("id:" + id);
                     };
                };

            currentRow.onclick = createClickHandler(currentRow);
        }
    }
});
