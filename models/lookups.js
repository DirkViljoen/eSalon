'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function LookupsModel() {

    function paymentMethods() {
        var deferred = q.defer();

        db.query('CALL spPayment_Method_All();')
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

    function provinces() {
        var deferred = q.defer();

        db.query('CALL spProvinces_Read();')
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

    function cities(id) {
        var deferred = q.defer();

        db.query('CALL spCities_Filtered(' + id + ');')
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

    function hairlength() {
        var deferred = q.defer();

        db.query('CALL spHairLength_Read();')
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

    function suburbs(id) {
        var deferred = q.defer();

        db.query('CALL spSuburbs_Filtered(' + id + ');')
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

    function notificationMethods() {
        var deferred = q.defer();

        db.query('CALL spNotificationMethods_Read();')
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

    function roles() {
        var deferred = q.defer();

        db.query('CALL spRoles_Lookup();')
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
        cities: cities,
        hairlengths: hairlength,
        notificationMethods: notificationMethods,
        paymentMethods: paymentMethods,
        provinces: provinces,
        suburbs: suburbs,
        roles: roles
    };
};
