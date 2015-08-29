'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function BookingModel() {
    function get(id) {
        console.log('Module - Booking - Get');

        var deferred = q.defer();

        if (id) {
            console.log('Booking get');
            db.query('CALL spBooking_Read_ID(' + id + ');')
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

    function search(fname, lname, reference) {
        console.log('Module - Booking - Search');

        var deferred = q.defer();

        console.log('booking fname, sname & reference search');
        db.query('CALL spBooking_Read_Search("%' + fname + '%","%' + lname + '%","' + reference + '");')
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
        console.log('Module - Booking - Add');

        var deferred = q.defer();
        var services = 0;

        if (obj) {
            console.log('Creating Booking.');
            db.execute('CALL spBooking_Create (' +
                obj.datetime + ',' +
                obj.duration + ',' +
                obj.completed + ',' +
                obj.active + ',' +
                obj.reference + ',' +
                obj.cid + ',' +
                obj.eid + ')'
            )
                .then(
                    function (result){
                        console.log('Booking created. Creating ' + obj.services.length + ' service(s).');
                        for (var i = 0; i < obj.services.length; i++) {
                            db.execute('CALL spBookingServices_Create(' + result.SQLstats.insertId + ',' + obj.services[i].hlsid + ')')
                                .then(
                                    function (result){
                                        services = services + 1;
                                        if (services == obj.services.length) {
                                            console.log('All services created.');
                                            deferred.resolve(result);
                                        };
                                    },
                                    function (err){
                                        console.error(new Error('Unable to create Booking Services.'))
                                        deferred.reject(err);
                                    });
                        }
                    },
                    function (err){
                        console.error(new Error('Unable to create Booking.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to create Booking. no object provided'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function update(obj) {
        console.log('Module - Booking - Update');

        var deferred = q.defer();
        var services = 0;

        if (obj.bid) {
            console.log('Updating Booking.');
            db.execute('CALL spBooking_Update (' +
                obj.bid + ',' +
                obj.datetime + ',' +
                obj.duration + ',' +
                obj.completed + ',' +
                obj.active + ',' +
                obj.reference + ',' +
                obj.eid + ',' +
                obj.iid + ')'
            )
                .then(
                    function (result){
                        console.log('Booking updated. Updating services.');
                        db.execute('CALL spBookingServices_Delete()')
                            .then(
                                function (result){
                                    console.log('Booking services deleted. Creating new services');
                                    for (var i = 0; i < obj.services.length; i++) {
                                        db.execute('CALL spBookingServices_Create(' + obj.services[i].bid + ',' + obj.services[i].hlsid + ')')
                                            .then(
                                                function (result){
                                                    services = services + 1;
                                                    if (services == obj.services.length) {
                                                        console.log('All services created.');
                                                        deferred.resolve(result);
                                                    };
                                                },
                                                function (err){
                                                    console.error(new Error('Unable to create Booking Services.'))
                                                    deferred.reject(err);
                                                });
                                    }
                                },
                                function (err){
                                    console.error(new Error('Unable to delete Booking Services.'))
                                    deferred.reject(err);
                                }
                            )
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to update Booking.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to update Booking. No ID provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function disable(obj) {
        console.log('Module - Booking - Delete');

        var deferred = q.defer();

        if (obj.bid) {
            console.log('Deleteing Client.');
            db.execute('CALL spBooking_Delete (' +
                    obj.bid + ')'
                )
                .then(
                    function (result){
                        console.log('Booking deleted.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to delete Booking.'))
                        deferred.reject(err);
                    }
                );
        };

        return deferred.promise;
    };

    function products(id) {
        // console.log('Module - Products history - Get');

        // var deferred = q.defer();

        // if (id) {
        //     console.log('Client get products history');
        //     db.query('CALL spClient_Product_History(' + id + ');')
        //         .then(
        //             function (result){
        //                 deferred.resolve(result);
        //             },
        //             function (err){
        //                 deferred.reject(new Error(err));
        //             }
        //         );
        // }
        // else
        // {
        //     deferred.reject(new Error('No ID'));
        // }

        // return deferred.promise;
    };

    function services(id) {
        console.log('Module - Booking services - Get');

        var deferred = q.defer();

        if (id) {
            db.query('CALL spBooking_Services(' + id + ');')
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

    return {
        index: get,
        find: search,
        create: add,
        update: update,
        remove: disable,
        products: products,
        services: services
    };
};
