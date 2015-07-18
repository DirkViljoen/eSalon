'use strict';


module.exports = function SubLettersModel() {
    return {
        slBName: 'Cat"s nails',
        slCFName: 'Cathrine',
        slCLName: 'Swart',
        slCNumber: '061 555 2356',
        slCEmail: 'cat@cats.co.za',
        slRent: '4000',
        slStartDate: '12 June, 2015',
        slPaymentAmount: '4000',
        slPaymentDate: '01 July, 2015',
        slPaymentMethod: '2',
        slSearch:  [
                        {
                            'id': '1',
                            'slBName': 'Cat"s nails',
                            'slCFName': 'Cathrine',
                            'slCLName': 'Swart',
                            'slCNumber': '061 555 2356',
                            'slCEmail': 'cat@cats.co.za',
                            'slRent': '4000',
                            'slStartDate': '12 June, 2015'
                        },
                        {
                            'id': '2',
                            'slBName': 'Sallywax',
                            'slCFName': 'Samantha',
                            'slCLName': 'Carter',
                            'slCNumber': '061 555 8765',
                            'slCEmail': 'sam@sillywax.co.za',
                            'slRent': '2600',
                            'slStartDate': '01 January, 2014'
                        }
                    ],
        slPaymentMethods: [
                        {
                            'id': '',
                            'value': 'Select payment method'
                        },
                        {
                            'id': '1',
                            'value': 'Cash'
                        },
                        {
                            'id': '2',
                            'value': 'Card'
                        },
                        {
                            'id': '3',
                            'value': 'Electronic'
                        },
                        {
                            'id': '1',
                            'value': 'Zapper'
                        }
        ]
    };
};
