'use strict';

$('.navigation').fullCalendar({
        defaultView: 'agendaDay',
        minTime: '00:00:00',
        maxTime: '00:00:00',
        height: 50,
        editable: false,
        eventLimit: true, // allow "more" link when too many events
        theme: true
    });

$('.times').fullCalendar({
        defaultView: 'agendaDay',
        minTime: '07:00:00',
        maxTime: '18:00:00',
        height: 690,
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        theme: true,
    });

$('.employee1').fullCalendar({
        defaultView: 'agendaDay',
        dayClick: function(date, jsEvent, view) {
            window.location = '/booking/add';
        },
        eventClick: function(calEvent, jsEvent, view) {
            window.location = '/booking/update';
        },
        minTime: '07:00:00',
        maxTime: '18:00:00',
        height: 600,
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        theme: true,
        eventTextColor: '#000',
        events: [
            {
                title: 'Mandy W, Root colour - Long',
                start: '2015-07-26T07:30:00',
                end: '2015-07-26T08:30:00',
                backgroundColor: '#FFB0B0'
            },
            {
                title: 'Susan K, Haircut - Medium',
                start: '2015-07-26T08:30:00',
                end: '2015-07-26T09:00:00',
                backgroundColor: '#B0FFB1'
            },
            {
                title: 'Jess T, Full colour - Long',
                start: '2015-07-26T14:00:00',
                end: '2015-07-26T07:30:00',
                backgroundColor: '#FFFCB0'
            },
            {
                title: 'Jack A',
                start: '2015-07-21T08:45:00'
            },
            {
                title: 'Karen F',
                start: '2015-07-22T13:00:00'
            },
            {
                title: 'Eveline Y',
                start: '2015-07-22T13:30:00'
            }
        ]
    });

$('.employee2').fullCalendar({
        header: {
        },
        dayClick: function(date, jsEvent, view) {
            window.location = '/booking/add';
        },
        eventClick: function(calEvent, jsEvent, view) {
            window.location = '/booking/update';
        },
        defaultView: 'agendaDay',
        minTime: '07:00:00',
        maxTime: '18:00:00',
        height: 600,
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        theme: true,
        eventTextColor: '#000',
        events: [
            {
                title: 'Karla E, Full colour - Long',
                start: '2015-07-26T07:30:00',
                end: '2015-07-26T09:00:00',
                backgroundColor: '#B0FFB1'
            },
            {
                title: 'Will S, Haircut - Short',
                start: '2015-07-26T08:15:00',
                end: '2015-07-26T08:45:00',
                backgroundColor: '#B0FFB1'
            },
            {
                title: 'Jenny T, Full colour - Long',
                start: '2015-07-26T09:00:00',
                end: '2015-07-26T11:30:00',
                backgroundColor: '#B0FFB1'
            },
            {
                title: 'Mandy W',
                start: '2015-07-21T08:30:00',
                backgroundColor: 'red'
            },
            {
                title: 'Susan K',
                start: '2015-07-21T9:45:00',
                backgroundColor: 'red'
            },
            {
                title: 'Jess T',
                start: '2015-07-22T14:00:00',
                backgroundColor: 'red'
            },
            {
                title: 'Jack A',
                start: '2015-07-22T08:45:00',
                backgroundColor: 'red'
            },
            {
                title: 'Karen F',
                start: '2015-07-20T13:00:00',
                backgroundColor: 'red'
            },
            {
                title: 'Eveline Y',
                start: '2015-07-20T13:30:00',
                backgroundColor: 'red'
            }
        ]
    });

$('.employee3').fullCalendar({
        header: {
        },
        dayClick: function(date, jsEvent, view) {
            window.location = '/booking/add';
        },
        eventClick: function(calEvent, jsEvent, view) {
            window.location = '/booking/update';
        },
        defaultView: 'agendaDay',
        minTime: '07:00:00',
        maxTime: '18:00:00',
        height: 600,
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        theme: true,
        events: [
            {
                title: 'Off day',
                start: '2015-07-26T07:00',
                end: '2015-07-26T18:00',
                backgroundColor: '#3C3C3C'
            },
            {
                title: 'Susan K',
                start: '2015-07-22T9:45:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Jess T',
                start: '2015-07-20T14:00:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Jack A',
                start: '2015-07-20T08:45:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Karen F',
                start: '2015-07-21T13:00:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Eveline Y',
                start: '2015-07-21T10:30:00',
                backgroundColor: 'lime'
            }
        ]
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

$('#employeeSchedule').fullCalendar({
    header: {
        left: 'prev,next today',
        center: 'title',
        right: 'month,agendaWeek,agendaDay'
    },
    defaultView: 'month',
    editable: true,
    eventLimit: true,
    theme: true,
    dayClick: function(date, jsEvent, view) {
        window.location = '/employee/schedule-edit';
    },
    eventClick: function(calEvent, jsEvent, view) {
        window.location = '/employee/schedule-edit';
    },
    events: [
        {
            title: 'Off day',
            start: '2015-07-24',
            backgroundColor: 'gray'
        },
        {
            title: 'Holiday',
            start: '2015-06-07',
            end: '2015-07-19',
            backgroundColor: 'gray'
        },
        {
            title: 'Mandy W',
            start: '2015-07-22T08:30:00',
            backgroundColor: 'red'
        },
        {
            title: 'Susan K',
            start: '2015-07-22T9:45:00',
            backgroundColor: 'red'
        },
        {
            title: 'Jess T',
            start: '2015-07-20T14:00:00',
            backgroundColor: 'red'
        },
        {
            title: 'Jack A',
            start: '2015-07-20T08:45:00',
            backgroundColor: 'red'
        },
        {
            title: 'Karen F',
            start: '2015-07-21T13:00:00',
            backgroundColor: 'red'
        },
        {
            title: 'Eveline Y',
            start: '2015-07-21T10:30:00',
            backgroundColor: 'red'
        }
    ]

});
