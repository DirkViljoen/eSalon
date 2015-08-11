'use strict';

module.exports = function SubLettersModel() {
    var result = {};

    function show(id) {
        var mysql = require('mysql');

        var connection = mysql.createConnection({
            host     : 'localhost',
            user     : 'root',
            // password : '',
            database : 'eSalon'
        });

        connection.connect();

        connection.query('USE eSalon');

        if (id) {
            connection.query('CALL Sub_Letter_Search(' + id + ');', function(err, rows, fields) {
                if (!err){
                    console.log('SL search id complete');
                }
                else{
                    console.log('Error while performing SL search id.', err);
                }
                result = JSON.parse('{results: ' + JSON.stringify(rows) + '}' );
            });
        }
        else{
            connection.query('CALL Sub_Letter_All();', function(err, rows, fields) {
                if (!err){
                    console.log('SL search all complete');
                }
                else{
                    console.log('Error while performing SL search all.', err);
                }
                //res.json({"Error" : false, "Message" : "Success", "Users" : rows});
                result = JSON.parse('{"rows": ' + JSON.stringify(rows[0]) + ',' + '"SQLstats": ' + JSON.stringify(rows[1]) + '}');

            });
        }
        console.log(result);
        return result;
        connection.end();
    };

    function help() {
        return "hello world";
    };

    function add(sub_letter) {
        var db = require('../database/dbConnection');
        var connection = db.newConnection();
        db.opencon(connection);

        //var mysql = require('mysql');

        //var connection = mysql.createConnection({
        //    host     : 'localhost',
        //    user     : 'root',
            // password : '',
        //    database : 'eSalon'
        //});

        //connection.connect();

        //connection.query('USE eSalon');

        var query = connection.query('CALL Sub_Letter_Add ("'+sub_letter.body.slStartDate+'",'+sub_letter.body.slRent+',"'+sub_letter.body.slCEmail+'","'+sub_letter.body.slCNumber+'")', function(err, result) {
                if (!err){
                    console.log('SL search all complete');
                }
                else{
                    console.log('Error while performing SL search all.', err);
                }
            });
        console.log(query.sql);

        //connection.end();

        db.closecon(connection);
    };

    function testdata() {
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
        slSearch: show,
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
    }

    return {
        index: show,
        help: help,
        create: add,
        test: testdata
    };
};
