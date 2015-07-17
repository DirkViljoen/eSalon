'use strict';

$('.dtpicker').pickadate({})

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

$('.navigation').find('.fc-view-container').each(function(i, el) {
    $(el).hide();
});

$('.employee').find('.fc-toolbar').each(function(i, el) {
    $(el).hide();
});

$('.times').find('.fc-toolbar').each(function(i, el) {
    $(el).hide();
});

$('.employee').find('.fc-axis').each(function(i, el) {
    $(el).hide();
});

$('.calendar').find('.fc-head').each(function(i, el) {
    $(el).hide();
});

$('.calendar').find('.fc-day-grid').each(function(i, el) {
    $(el).hide();
});

$('#btn-cal-prev').click(function() {
    var a = new Date((new Date($('#cal-nav').val())).valueOf() - 1000*3600*24);
    $('#cal-nav').val(a.toUTCString());
    calchange();
});

$('#btn-cal-next').click(function() {
    var a = new Date((new Date($('#cal-nav').val())).valueOf() + 1000*3600*24);
    $('#cal-nav').val(a.toUTCString());
    calchange();
});

$('#cal-nav').change(function() {
    calchange();
});

function calchange(){
    $('.employee').fullCalendar( 'gotoDate', $('#cal-nav').val() );
    $('.employee').find('.fc-axis').each(function(i, el) {
        $(el).hide();
    });
    $('.calendar').find('.fc-head').each(function(i, el) {
        $(el).hide();
    });
    $('.calendar').find('.fc-day-grid').each(function(i, el) {
        $(el).hide();
    });
}
