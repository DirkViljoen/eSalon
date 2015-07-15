'use strict';

$('#calendar').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
        defaultView: 'agendaWeek',
        minTime: '07:00:00',
        maxTime: '18:00:00',
        height: 650,
        aspectRatio: 2,
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
