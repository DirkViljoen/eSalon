'use strict';


module.exports = function ServiceModel() {
    return {
        serName: 'standard cut',
        serAddInfo: 'none',
        serDuration: '30 minutes',
        serPrice: '100',
        serCategory: '1',
        serHairLength: '3',
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

          ],
        arrCategory:
          [
            {
              'id': '',
              'value': 'Choose a category'
            },
            {
              'id': '1',
              'value': 'Cutting'
            },
            {
              'id': '2',
              'value': 'Colouring'
            },
            {
              'id': '3',
              'value': 'Shampoos'
            },
            {
              'id': '4',
              'value': 'Other'
            }

          ]
    };
};
