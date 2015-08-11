'use strict';

module.exports = function SubLettersModel() {
    var result = {};

    function search(bName) {
        var mysql = require('mysql');

        var connection = mysql.createConnection({
            host     : 'localhost',
            user     : 'root',
            // password : '',
            database : 'eSalon'
        });

        connection.connect();

        connection.query('USE eSalon');

        if (bName) {
            connection.query('CALL Sub_Letter_Search("%' + bName + '%");', function(err, rows, fields) {
                if (!err){
                    console.log('SL search by Name complete');
                    result = JSON.parse('{"rows": ' + JSON.stringify(rows[0]) + ',' + '"SQLstats": ' + JSON.stringify(rows[1]) + '}');
                }
                else{
                    console.log('Error while performing SL search by Name.', err);
                    console.log(connection.query.sql);
                    result = JSON.parse('{}');
                }

            });
        }
        else{
            connection.query('CALL Sub_Letter_All();', function(err, rows, fields) {
                if (!err){
                    console.log('SL search all complete');
                    result = JSON.parse('{"rows": ' + JSON.stringify(rows[0]) + ',' + '"SQLstats": ' + JSON.stringify(rows[1]) + '}');
                }
                else{
                    console.log('Error while performing SL search all.', err);
                    result = JSON.parse('{}');
                }
                //res.json({"Error" : false, "Message" : "Success", "Users" : rows});

            });
        }
        console.log(result);
        return result;
        connection.end();
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

    function view(sID) {
        var mysql = require('mysql');

        var connection = mysql.createConnection({
            host     : 'localhost',
            user     : 'root',
            // password : '',
            database : 'eSalon'
        });

        connection.connect();

        connection.query('USE eSalon');

        if (sID) {
            connection.query('CALL Sub_Letter_View("' + sID + '");', function(err, rows, fields) {
                if (!err){
                    console.log('SL search by Name complete');
                    result = JSON.parse('{"rows": ' + JSON.stringify(rows[0]) + ',' + '"SQLstats": ' + JSON.stringify(rows[1]) + '}');
                }
                else{
                    console.log('Error while performing SL search by Name.', err);
                    console.log(connection.query.sql);
                    result = JSON.parse('{}');
                }

            });
        }
        else{
            result = JSON.parse('{}');
        }

        console.log(result);
        return result;
        connection.end();
    }

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
        find: show,
        index: view,
        create: add,
        test: testdata
    };
};
