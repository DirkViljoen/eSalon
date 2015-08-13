'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function SubLettersModel() {

    function search(bName) {
        var deferred = q.defer();

        console.log('Search');
        // ** test code

        if (bName) {
            console.log('sub-letter name search');
            db.query('CALL Sub_Letter_Search("%' + bName + '%");')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else{
            console.log('sub-letter full search');

            db.query('CALL Sub_Letter_All();')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }

        return deferred.promise;
    };

    function add(sub_letter) {
        var mysql = require('mysql');

        var connection = mysql.createConnection({
            host     : 'localhost',
            user     : 'root',
            // password : '',
            database : 'eSalon'
        });

        connection.connect();

        connection.query('USE eSalon');

        var query = connection.query('CALL Sub_Letter_Add ("'+sub_letter.slBName+'","'+sub_letter.slCFName+'","'+sub_letter.slCLName+'","'+sub_letter.slCNumber+'","'+sub_letter.slCEmail+'","2015-04-01",'+sub_letter.slRent+')', function(err, result) {
                if (!err){
                    console.log('SL create complete');
                }
                else{
                    console.log('Error while performing SL create.', err);
                }
            });
        console.log(query.sql);

        connection.end();
    };

    function view(sID) {
        var deferred = q.defer();

        console.log('View');
        // ** test code

        if (sID) {
            console.log('sub-letter get details');
            db.query('CALL Sub_Letter_get(' + sID + ');')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else{
            console.log('ERROR - no ID for sub-letter get');
            deferred.reject(new Error('No ID'));
        };

        return deferred.promise;
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

    function testcode() {
        var deferred = q.defer();

        console.log('module defer testing');

        db.query('CALL Sub_Letter_All();')
            .then(
                function (result){
                    console.log(result);
                    deferred.resolve(result);
                },
                function (err){
                    deferred.reject(new Error(err));
                }
            );

        console.log('Module end');
        return deferred.promise;
    };

    return {
        find: search,
        index: view,
        create: add,
        test: testdata,
        help: testcode
    };
};
