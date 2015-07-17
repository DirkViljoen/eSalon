'use strict';


module.exports = function ClientModel() {
    return {
        cliTitle: 'Ms',
        cliFName: 'Angelique',
        cliLName: 'Burger',
        cliCNumber: '086 567 1234',
        cliEmailAddress: 'angbur@google.co.za',
        cliDOB: '12 January 1979',
        cliALine1: '23 Oukraal Appartments',
        cliALine2: 'January Masilela Street',
        cliASuburb: '1',
        cliACity: '2',
        cliAProvince: '1',
        cliNotification: '2',
        cliRecieveNotifications: '1',
        addressSuburbs:  [
                        {
                            'id': '',
                            'value': 'Choose a suburb'
                        },
                        {
                            'id': '1',
                            'value': 'Brooklynn'
                        },
                        {
                            'id': '2',
                            'value': 'Hatfield'
                        }
                    ],
        addressCitys:  [
                        {
                            'id': '',
                            'value': 'Choose a city'
                        },
                        {
                            'id': '1',
                            'value': 'Johannesburg'
                        },
                        {
                            'id': '2',
                            'value': 'Pretoria'
                        }
                    ],
        addressProvinces:  [
                        {
                            'id': '',
                            'value': 'Choose a province'
                        },
                        {
                            'id': '1',
                            'value': 'Gauteng'
                        },
                        {
                            'id': '2',
                            'value': 'Western Cape'
                        }
                    ],
        cliServiceHistory:  [
                        {
                            'date': '01 June 2015',
                            'service': 'Hair colour - full',
                            'stylist': 'Monique'
                        },
                        {
                            'date': '01 July 2015',
                            'service': 'Hair colour - roots',
                            'stylist': 'Monique'
                        },
                        {
                            'date': '01 August 2015',
                            'service': 'Hair colour - full',
                            'stylist': 'Monique'
                        }
                    ],
        cliProductHistory:  [
                        {
                            'date': '02 July 2015',
                            'product': 'Shampoo - Tresume',
                            'stylist': 'Chante'
                        },
                        {
                            'date': '02 July 2015',
                            'product': 'Conditioner - Pantene',
                            'stylist': 'Chante'
                        }
                    ],
        cliNotificationMethods:  [
                        {
                            'id':'',
                            'value': 'Select a notification method'
                        },
                        {
                            'id':'1',
                            'value': 'SMS'
                        },
                        {
                            'id':'2',
                            'value': 'email'
                        }
                    ]
    };
};
