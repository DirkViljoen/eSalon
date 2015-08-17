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
        var deferred = q.defer();

        if (sub_letter) {
            console.log('Sub-Letter Create');
            db.execute('CALL Sub_Letter_Add ("' +
                sub_letter.sBusinessName + '","' +
                sub_letter.sContactFName + '","' +
                sub_letter.sContactLName + '","' +
                sub_letter.sContactNumber + '","' +
                sub_letter.sContactEmail + '","2015-04-01",' +
                sub_letter.sAmount + ')'
            )
                .then(
                    function (result){
                        deferred.resolve(results);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else {
            console.log('No data to add');
            deferred.reject('No data');
        }

        return deferred.promise;
    };

    function update(sub_letter) {
        var deferred = q.defer();

        if (sub_letter) {
            console.log('Sub-Letter Update');
            db.execute('CALL Sub_Letter_Update (' +
                sub_letter.sSub_Letter_id + ',"' +
                sub_letter.sBusinessName + '","' +
                sub_letter.sContactFName + '","' +
                sub_letter.sContactLName + '","' +
                sub_letter.sContactNumber + '","' +
                sub_letter.sContactEmail + '","2015-04-01",' +
                sub_letter.sAmount + ')'
            )
                .then(
                    function (result){
                        deferred.resolve(results);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else {
            console.log('No data to update');
            deferred.reject('No data');
        }

        return deferred.promise;
    };

    function deactivate(sub_letter) {
        var deferred = q.defer();

        if (sub_letter) {
            console.log('Sub-Letter Delete');
            db.execute('CALL Sub_Letter_Delete (' +
                sub_letter.id + ')'
            )
                .then(
                    function (result){
                        deferred.resolve(results);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else {
            console.log('No data to delete');
            deferred.reject('No data');
        }

        return deferred.promise;
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
    };

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
        deactivate: deactivate,
        update: update,
        buffer: null,
        test: testdata,
        help: testcode
    };
};
