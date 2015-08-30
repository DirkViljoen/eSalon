'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function ServiceModel() {

    function get(id) {
        console.log('Module - Service - Get');

        var deferred = q.defer();

        if (id) {
            db.query('CALL spService_Read_ID(' + id + ');')
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

    function search(sname) {
        console.log('Module - Service - Search');

        var deferred = q.defer();

        db.query('CALL spService_Search("%' + sname + '%");')
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

    function add(obj) {
        console.log('Module - Service - Create');

        // var deferred = q.defer();

        // if (obj) {
        //     console.log('Creating Booking.');
        //     db.execute('CALL spBooking_Create (' +
        //         obj.datetime + ',' +
        //         obj.duration + ',' +
        //         obj.completed + ',' +
        //         obj.active + ',' +
        //         obj.reference + ',' +
        //         obj.cid + ',' +
        //         obj.eid + ',' +
        //         obj.iid + ')'
        //     )
        //         .then(
        //             function (result){
        //                 console.log('Booking created.');
        //                 deferred.resolve(result);
        //             },
        //             function (err){
        //                 console.error(new Error('Unable to create Booking.'))
        //                 deferred.reject(err);
        //             }
        //         );

        // }
        // else {
        //     console.error(new Error('Unable to create booking. No object provided.'))
        //     deferred.reject(err);
        // }

        // return deferred.promise;
    };

    function update(obj) {
        console.log('Module - Service - Update');

        // var deferred = q.defer();

        // if (obj.bid) {
        //     console.log('Updating Booking.');
        //     db.execute('CALL spBooking_Update (' +
        //         obj.bid + ',' +
        //         obj.datetime + ',' +
        //         obj.duration + ',' +
        //         obj.completed + ',' +
        //         obj.active + ',' +
        //         obj.reference + ',' +
        //         obj.eid + ',' +
        //         obj.iid + ')'
        //     )
        //         .then(
        //             function (result){
        //                 console.log('Booking updated.');
        //                 deferred.resolve(result);
        //             },
        //             function (err){
        //                 console.error(new Error('Unable to update Booking.'))
        //                 deferred.reject(err);
        //             }
        //         );

        // }
        // else {
        //     console.error(new Error('Unable to update Booking. No ID provided.'))
        //     deferred.reject(err);
        // }

        // return deferred.promise;
    };

    function disable(obj) {
        console.log('Module - Service - Delete');

        // var deferred = q.defer();

        // if (obj.bid) {
        //     console.log('Deleteing Client.');
        //     db.execute('CALL spBooking_Delete (' +
        //             obj.bid + ')'
        //         )
        //         .then(
        //             function (result){
        //                 console.log('Booking deleted.');
        //                 deferred.resolve(result);
        //             },
        //             function (err){
        //                 console.error(new Error('Unable to delete Booking.'))
        //                 deferred.reject(err);
        //             }
        //         );
        // };

        // return deferred.promise;
    };

    function hairlengthservices() {
        console.log('Module - hairlengthservices - Search');

        var deferred = q.defer();

        db.query('CALL spHairLengthService_Search();')
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

    function historicservices(sname) {
        console.log('Module - HistoricService - Search');

        var deferred = q.defer();

        db.query('CALL spHistoricService_Search("%' + sname + '%");')
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
        index: get,
        find: search,
        create: add,
        update: update,
        remove: disable,
        hairlengthservices: hairlengthservices,
        historicservices: historicservices
    };
};
