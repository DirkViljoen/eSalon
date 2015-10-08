'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function ServiceModel() {

    function get(id) {
        console.log('Module - Service - Get');

        var deferred = q.defer();

        if (id) {
            db.query('CALL spService_Read(' + id + ');')
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

    function search(service) {
        console.log('Module - Service - Search');

        var deferred = q.defer();

        db.query('CALL spService_Search("%' + service + '%");')
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

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Service.');
            db.execute('CALL sp_Insert_Service (' +
                obj.name + ',' +
                obj.info + ',' +
                obj.price + ',' +
                obj.duration.short + ',' +
                obj.duration.medium + ',' +
                obj.duration.long + ')'
            )
                .then(
                    function (result){
                        console.log('Service created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Service.'))
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

    function update(obj) {
        console.log('Module - Service - Update');

        var deferred = q.defer();

        if (obj) {
            console.log('Updating Service.');
            db.execute('CALL sp_Update_Service (' +
                obj.serviceId + ',' +
                obj.name + ',' +
                obj.info + ',' +
                obj.price + ',' +
                obj.duration.short + ',' +
                obj.duration.medium + ',' +
                obj.duration.long + ')'
            )
                .then(
                    function (result){
                        console.log('Service updated.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to update Service.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to update service. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function disable(obj) {
        console.log('Module - Service - Delete');

        var deferred = q.defer();

        console.log(obj);

        if (obj.serviceId) {
            console.log('Deleteing Service.');
            db.execute('CALL spService_Delete (' +
                    obj.serviceId + ')'
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

    function serviceDuration(serviceId, hairLenId) {
        console.log('Module - Service - Duration');

        var deferred = q.defer();

        db.query('CALL spService_Read_Duration(' + serviceId + ',' + hairLenId + ');')
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
        historicservices: historicservices,
        duration: serviceDuration
    };
};
