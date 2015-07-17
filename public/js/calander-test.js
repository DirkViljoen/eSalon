'use strict';

$('#calendar').fullCalendar({
    header: {
        center: 'month,agendaFourDay' // buttons for switching between views
    },
    views: {
        agendaFourDay: {
            type: 'agenda',
            duration: { days: 4 },
            buttonText: '4 day',
            theme: true
        }
    }
});
