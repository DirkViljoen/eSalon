'use strict';

$('.dtpicker').pickadate()

$('.tmpicker').pickatime({
    min: [7,30],
    max: [18,0]
    })

$(function() {
    $( "#slider" ).slider({
        value:15,
        min: 15,
        max: 180,
        step: 15,
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

$('.fc-time-grid-event').click(function(){
    window.location = '/booking/update';
});
