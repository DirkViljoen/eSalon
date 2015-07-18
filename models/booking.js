'use strict';


module.exports = function BookingModel() {
    return {
        name: 'booking',
        bkgClient: '2',
        bkgStylist: 'Cherise',
        dateBookingStart: '12/08/2015',
        bkgService: '2',
        bkgHairLength: '3',
        bkgSerLen: "00:30",
        timeBookingStart: "2:30 PM",
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

          ],
        arrServices:
          [
            {
              'id': '',
              'value': 'Choose a service'
            },
            {
              'id': '1',
              'value': 'Haircut'
            },
            {
              'id': '2',
              'value': 'Colour'
            },
            {
              'id': '3',
              'value': 'Roots'
            }

          ],
        arrHairLength:
          [
            {
              'id': '',
              'value': 'Choose a hair length'
            },
            {
              'id': '1',
              'value': 'Short'
            },
            {
              'id': '2',
              'value': 'Medium'
            },
            {
              'id': '3',
              'value': 'Long'
            }

          ]
    };
};
