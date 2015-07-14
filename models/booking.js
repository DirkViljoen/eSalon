'use strict';


module.exports = function BookingModel() {
    return {
        name: 'booking',
        bkgClient: '2',
        bkgStylist: 'Cherise',
        dateBookingStart: '12/08/2015',
        arrClients:
          [
            {
              'id': '',
              'value': 'Select a client'
            },
            {
              'id': '1',
              'value': 'Client Sally'
            },
            {
              'id': '2',
              'value': 'Client Jess'
            },
            {
              'id': '3',
              'value': 'Client Mel'
            }

          ]
    };
};
