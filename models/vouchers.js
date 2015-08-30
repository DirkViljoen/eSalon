'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function VouchersModel() {

    function get(obj) {
        console.log('Module - Vouchers - Get');

        var deferred = q.defer();

        if (obj.vid) {
            db.query('CALL spVoucher_Read(' + obj.vid + ');')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else
        {
            deferred.reject({err: 'No barcode to search for voucher'});
        }

        return deferred.promise;
    };

    function post(obj) {
        console.log('Module - Vouchers - Post');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL spVoucher_Create(' + obj.amount + ',"' + obj.barcode + '");')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else
        {
            deferred.reject({err: 'No voucher object to add'});
        }

        return deferred.promise;
    };

    function put(obj) {
        console.log('Module - Vouchers - Put');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL spVoucher_Update(' + obj.vid + ',' + obj.amount + ');')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else
        {
            deferred.reject({err: 'No voucher object to update'});
        }

        return deferred.promise;
    };

    return {
        find: get,
        create: post,
        update: put
    };
};
