'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function ClientModel() {
    function get(id) {
        console.log('Module - Client - Get');

        var deferred = q.defer();

        if (id) {
            console.log('Client get');
            db.query('CALL spClient_Read_ID(' + id + ');')
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

    function address(id) {
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

    function search(fname, lname) {
        console.log('Module - Client - Search');

        var deferred = q.defer();

        console.log('sub-letter name search');
        db.query('CALL spClient_Read_Search("%' + fname + '%","%' + lname + '%");')
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
        console.log('Module - Client - Add');

        var deferred = q.defer();

        if (((obj.line1 == null) && (obj.line2 == null) && (obj.suburbId == null)) == false) {
            console.log('Creating Address.');
            db.execute('CALL spAddress_Create (' +
                    obj.line1 + ',' +
                    obj.line2 + ',' +
                    obj.suburbId + ')'
                )
                .then(
                    function (result){
                        console.log('Address created, creating new Client.');
                        db.execute('CALL spClient_Create (' +
                            obj.contactTitle + ',' +
                            obj.contactFName + ',' +
                            obj.contactLName + ',' +
                            obj.contactNumber + ',' +
                            obj.contactEmail + ',' +
                            obj.dateOfBirth + ',' +
                            obj.reminders + ',' +
                            obj.notifications + ',' +
                            obj.notificationMethod + ',' +
                            result.SQLstats.insertId + ')'
                        )
                        .then(
                            function (result){
                                console.log('New Client created.');
                                deferred.resolve(result);
                            },
                            function (err){
                                console.error(new Error('Unable to create new Client.'))
                                deferred.reject(err);
                            }
                        );
                    },
                    function (err){
                        console.error(new Error('Unable to create new Address.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            db.execute('CALL spClient_Create (' +
                obj.contactTitle + ',' +
                obj.contactFName + ',' +
                obj.contactLName + ',' +
                obj.contactNumber + ',' +
                obj.contactEmail + ',' +
                obj.dateOfBirth + ',' +
                obj.reminders + ',' +
                obj.notifications + ',' +
                obj.notificationMethod + ', null)'
            )
            .then(
                function (result){
                    console.log('New Client created.');
                    deferred.resolve(result);
                },
                function (err){
                    console.error(new Error('Unable to create new Client.'))
                    deferred.reject(err);
                }
            );
        }

        return deferred.promise;
    };

    function update(obj) {
        console.log('Module - Client - Update');

        var deferred = q.defer();

        if ((obj.addressId != null) && (((obj.line1 == null) && (obj.line2 == null) && (obj.suburbId == null)) == false)) {
            console.log('Existing Address. Updating Address.');
            db.execute('CALL spAddress_Update (' +
                    obj.addressId + ',' +
                    obj.line1 + ',' +
                    obj.line2 + ',' +
                    obj.suburbId + ')'
                )
                .then(
                    function (result){
                        console.log('Address updated, updating Client.');
                        db.execute('CALL spClient_Update (' +
                            obj.clientId + ',' +
                            obj.contactTitle + ',' +
                            obj.contactFName + ',' +
                            obj.contactLName + ',' +
                            obj.contactNumber + ',' +
                            obj.contactEmail + ',' +
                            obj.dateOfBirth + ',' +
                            obj.reminders + ',' +
                            obj.notifications + ',' +
                            obj.notificationMethod + ',' +
                            obj.addressId + ')'
                        )
                        .then(
                            function (result){
                                console.log('Client updated.');
                                deferred.resolve(result);
                            },
                            function (err){
                                console.error(new Error('Unable to update Client.'))
                                deferred.reject(err);
                            }
                        );
                    },
                    function (err){
                        console.error(new Error('Unable to update Address.'))
                        deferred.reject(err);
                    }
                );
        }
        else if ((obj.addressId == null) && (((obj.line1 == null) && (obj.line2 == null) && (obj.suburbId == null)) == false)) {
            console.log('New Address. Creating Address.');
            db.execute('CALL spAddress_Create (' +
                    obj.line1 + ',' +
                    obj.line2 + ',' +
                    obj.suburbId + ')'
                )
                .then(
                    function (result){
                        console.log('Address created, updating Client.');
                        db.execute('CALL spClient_Update (' +
                            obj.clientId + ',' +
                            obj.contactTitle + ',' +
                            obj.contactFName + ',' +
                            obj.contactLName + ',' +
                            obj.contactNumber + ',' +
                            obj.contactEmail + ',' +
                            obj.dateOfBirth + ',' +
                            obj.reminders + ',' +
                            obj.notifications + ',' +
                            obj.notificationMethod + ',' +
                            result.SQLstats.insertId + ')'
                        )
                        .then(
                            function (result){
                                console.log('Client updated.');
                                deferred.resolve(result);
                            },
                            function (err){
                                console.error(new Error('Unable to update Client.'))
                                deferred.reject(err);
                            }
                        );
                    },
                    function (err){
                        console.error(new Error('Unable to create Address.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.log('No Address information, Updating Client.');
            db.execute('CALL spClient_Update (' +
                obj.clientId + ',' +
                obj.contactTitle + ',' +
                obj.contactFName + ',' +
                obj.contactLName + ',' +
                obj.contactNumber + ',' +
                obj.contactEmail + ',' +
                obj.dateOfBirth + ',' +
                obj.reminders + ',' +
                obj.notifications + ',' +
                obj.notificationMethod + ', null)'
            )
            .then(
                function (result){
                    console.log('Client updated.');
                    deferred.resolve(result);
                },
                function (err){
                    console.error(new Error('Unable to update Client.'))
                    deferred.reject(err);
                }
            );
        }

        return deferred.promise;
    };

    function disable(obj) {
        console.log('Module - Client - Delete');

        var deferred = q.defer();

        if (obj.clientId != null) {
            console.log('Deleteing Client.');
            db.execute('CALL spClient_Delete (' +
                    obj.clientId + ')'
                )
                .then(
                    function (result){
                        console.log('Client deleted.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to delete Client.'))
                        deferred.reject(err);
                    }
                );
        };

        return deferred.promise;
    };

    function products(id) {
        console.log('Module - Products history - Get');

        var deferred = q.defer();

        if (id) {
            console.log('Client get products history');
            db.query('CALL spClient_Product_History(' + id + ');')
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

    function services(id) {
        console.log('Module - Service history - Get');

        var deferred = q.defer();

        if (id) {
            console.log('Client get service history');
            db.query('CALL spClient_Service_History(' + id + ');')
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
        address: address,
        products: products,
        services: services
    };
};
