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
        minTime: '07:00:00',
        maxTime: '18:00:00',
        height: 600,
        editable: true,
        eventLimit: true, // allow "more" link when too many events
        theme: true,
        events: [
            {
                title: 'All Day Event',
                start: '2015-07-01'
            },
            {
                title: 'Long Event',
                start: '2015-07-07',
                end: '2015-07-10'
            },
            {
                id: 999,
                title: 'Repeating Event',
                start: '2015-07-09T16:00:00'
            },
            {
                id: 999,
                title: 'Repeating Event',
                start: '2015-07-16T16:00:00'
            },
            {
                title: 'Conference',
                start: '2015-07-11',
                end: '2015-07-13'
            },
            {
                title: 'Meeting',
                start: '2015-07-12T10:30:00',
                end: '2015-07-12T12:30:00'
            },
            {
                title: 'Lunch',
                start: '2015-07-12T12:00:00'
            },
            {
                title: 'Meeting',
                start: '2015-07-12T14:30:00'
            },
            {
                title: 'Happy Hour',
                start: '2015-07-12T17:30:00'
            },
            {
                title: 'Dinner',
                start: '2015-07-12T20:00:00'
            },
            {
                title: 'Birthday Party',
                start: '2015-07-13T07:00:00'
            },
            {
                title: 'Click for Google',
                url: 'http://google.com/',
                start: '2015-07-28'
            }
        ]
    });

$('.employee2').fullCalendar({
        header: {
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
                title: 'All Day Event',
                start: '2015-07-01',
                backgroundColor: 'red'
            },
            {
                title: 'Long Event',
                start: '2015-07-07',
                end: '2015-07-10',
                backgroundColor: 'red'
            },
            {
                id: 999,
                title: 'Repeating Event',
                start: '2015-07-09T16:00:00',
                backgroundColor: 'red'
            },
            {
                id: 999,
                title: 'Repeating Event',
                start: '2015-07-16T16:00:00',
                backgroundColor: 'red'
            },
            {
                title: 'Conference',
                start: '2015-07-11',
                end: '2015-07-13',
                backgroundColor: 'red'
            },
            {
                title: 'Meeting',
                start: '2015-07-12T10:30:00',
                end: '2015-07-12T12:30:00',
                backgroundColor: 'red'
            },
            {
                title: 'Lunch',
                start: '2015-07-12T12:00:00',
                backgroundColor: 'red'
            },
            {
                title: 'Meeting',
                start: '2015-07-12T14:30:00',
                backgroundColor: 'red'
            },
            {
                title: 'Happy Hour',
                start: '2015-07-12T17:30:00',
                backgroundColor: 'red'
            },
            {
                title: 'Dinner',
                start: '2015-07-12T20:00:00',
                backgroundColor: 'red'
            },
            {
                title: 'Birthday Party',
                start: '2015-07-13T07:00:00',
                backgroundColor: 'red'
            },
            {
                title: 'Click for Google',
                url: 'http://google.com/',
                start: '2015-07-28',
                backgroundColor: 'red'
            }
        ]
    });

$('.employee3').fullCalendar({
        header: {
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
                title: 'All Day Event',
                start: '2015-07-01',
                backgroundColor: 'lime'
            },
            {
                title: 'Long Event',
                start: '2015-07-07',
                end: '2015-07-10',
                backgroundColor: 'lime'
            },
            {
                id: 999,
                title: 'Repeating Event',
                start: '2015-07-09T16:00:00',
                backgroundColor: 'lime'
            },
            {
                id: 999,
                title: 'Repeating Event',
                start: '2015-07-16T16:00:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Conference',
                start: '2015-07-11',
                end: '2015-07-13',
                backgroundColor: 'lime'
            },
            {
                title: 'Meeting',
                start: '2015-07-12T10:30:00',
                end: '2015-07-12T12:30:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Lunch',
                start: '2015-07-12T12:00:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Meeting',
                start: '2015-07-12T14:30:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Happy Hour',
                start: '2015-07-12T17:30:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Dinner',
                start: '2015-07-12T20:00:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Birthday Party',
                start: '2015-07-13T07:00:00',
                backgroundColor: 'lime'
            },
            {
                title: 'Click for Google',
                url: 'http://google.com/',
                start: '2015-07-28',
                backgroundColor: 'lime'
            }
        ]
    });
