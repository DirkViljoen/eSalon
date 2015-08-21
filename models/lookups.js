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

    return {
        paymentMethods: paymentMethods
    };
};
