'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function RoleModel() {

    function getPermissions(obj) {
        console.log('Module - Other - Audit');

        var deferred = q.defer();

        db.query('CALL sp_getPermissions();')
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
        permissions: getPermissions
    };
};
