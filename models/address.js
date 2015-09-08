'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function AddressModel() {
    function get(id) {
        console.log('Module - Address - Get');

        var deferred = q.defer();

        if (id) {
            console.log('Client get address');
            db.query('CALL spAddress_Read(' + id + ');')
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
            deferred.reject(new Error('No ID'));
        }

        return deferred.promise;
    };

    function add(obj) {
        console.log('Module - Address - Add');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL spAddress_Create (' +
                    obj.line1 + ',' +
                    obj.line2 + ',' +
                    obj.suburbId + ')'
                )
                .then(
                    function (result){
                        console.log('Address created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Address.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to create Address.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function update(obj) {
        console.log('Module - Address - Update');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL spAddress_Update (' +
                    obj.addressId + ',' +
                    obj.line1 + ',' +
                    obj.line2 + ',' +
                    obj.suburbId + ')'
                )
                .then(
                    function (result){
                        console.log('Address Updated.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to update Address.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to update Address.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function disable(obj) {
        // console.log('Module - Address - Delete');

        var deferred = q.defer();

        // if (obj.clientId != null) {
        //     console.log('Deleteing Client.');
        //     db.execute('CALL spClient_Delete (' +
        //             obj.clientId + ')'
        //         )
        //         .then(
        //             function (result){
        //                 console.log('Client deleted.');
        //                 deferred.resolve(result);
        //             },
        //             function (err){
        //                 console.error(new Error('Unable to delete Client.'))
        //                 deferred.reject(err);
        //             }
        //         );
        // };

        // return deferred.promise;
        return deferred.reject({err: 'unimplemented'});
    };

    return {
        index: get,
        create: add,
        update: update,
        remove: disable
    };
};
