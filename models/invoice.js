'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function InvoiceModel() {

    function add(obj) {
        console.log('Module - Invoice - Create');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Invoice.');
            db.execute('CALL sp_Insert_Invoice (' +
                obj.datetime + ',' +
                obj.discount + ',' +
                obj.percentage + ',' +
                obj.total + ',' +
                obj.paymentMethod + ',' +
                obj.cid + ',' +
                obj.eid + ',' +
                obj.bid + ')'
            )
                .then(
                    function (result){
                        console.log('Invoice created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Invoice.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to create service. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function addService(obj) {
        console.log('Module - Invoice - Service Create');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Invoice Service Line.');
            db.execute('CALL sp_Insert_Invoice_Service (' +
                obj.price + ',' +
                obj.quantity + ',' +
                obj.shid + ',' +
                obj.spid + ',' +
                obj.iid + ')'
            )
                .then(
                    function (result){
                        console.log('Invoice service line created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Invoice service line.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to create invoice service line. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function addStock(obj) {
        console.log('Module - Invoice - Stock Create');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Invoice Stock Line.');
            db.execute('CALL sp_Insert_Invoice_Stock (' +
                obj.price + ',' +
                obj.quantity + ',' +
                obj.shid + ',' +
                obj.spid + ',' +
                obj.iid + ')'
            )
                .then(
                    function (result){
                        console.log('Invoice stock line created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Invoice stock line.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to create invoice stock line. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function serviceHistory() {
        console.log('Module - Invoice - serviceHistory');

        var deferred = q.defer();

        console.log("GET Service history ID's");
        db.query("Call spServiceMap();")
            .then(
                function (result){
                    deferred.resolve(result);
                },
                function (err){
                    deferred.reject(new Error(err));
                }
            );

        return deferred.promise;
    };

    function productHistory() {
        console.log('Module - Invoice - productHistory');

        var deferred = q.defer();

        console.log("GET Product history ID's");
        db.query("Call spProductMap();")
            .then(
                function (result){
                    deferred.resolve(result);
                },
                function (err){
                    deferred.reject(new Error(err));
                }
            );

        return deferred.promise;
    };

    return {
        create: add,
        serviceCreate: addService,
        stockCreate: addStock,
        getServiceHistory: serviceHistory,
        getProductHistory: productHistory
    };
};
