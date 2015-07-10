'use strict';


module.exports = function supplierModel() {
    return {
        supName: 'Biokenetics4u',
        supContactNum: '+27 83 123 1234',
        supEmail: 'john@bk4u.com',
        supProducts:
          [
            {
              'id': '1',
              'value': 'Shampoo'
            }
          ],
        arrSearchProducts:
          [
            {
              'id': '',
              'value': 'Search based on ordered products'
            },
            {
              'id': '1',
              'value': 'Shampoo'
            },
            {
              'id': '2',
              'value': 'Gel'
            },
            {
              'id': '3',
              'value': 'Conditioner'
            }

          ],
        supOrders:
          [
            {
              'date': '12 June 2015',
              'product': 'Shampoo',
              'quantity': '5',
              'price': '50'
            },
            {
              'date': '26 June 2015',
              'product': 'Conditioner',
              'quantity': '5',
              'price': '55'
            }
          ]
    };
};
