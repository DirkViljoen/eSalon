'use strict';


module.exports = function EmployeeModel() {
    return {
        empFName: 'John',
        empLName: 'Doe',
        empCNumber: '082 453 4532',
        empSalary: '10 000',

        empUsername: 'johdoe',
        empPassword: 'asdfg',

        empEmail: 'johdoe@esalon.co.za',
        empALine1: '43 Oaktree Avenue',
        empALine2: '',
        empSuburb: '1',
        empCity: '2',
        empProvince: '1',

        empRole: '1',

        empImage: 'https://www.google.co.za/images/nav_logo195.png',
        lastpage: '',

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
        userRoles:  [
                        {
                            'id': '',
                            'value': 'Choose a role'
                        },
                        {
                            'id': '1',
                            'value': 'Manager'
                        },
                        {
                            'id': '2',
                            'value': 'Stylist'
                        }
                    ]
    };
};
