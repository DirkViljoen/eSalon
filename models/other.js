'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function OtherModel() {

    function audit(obj) {
        console.log('Module - Other - Auditing');

        var deferred = q.defer();

        db.audit(obj)
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
        log: audit
    };
};
