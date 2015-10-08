'use strict';

var q = require('q');
var db = require('../libs/db');
var moment = require('moment');

module.exports = function ReportsModel() {

    function auditReport(obj) {
        console.log('Module - Reports - Audit');

        var deferred = q.defer();

        db.query('CALL sp_audit_Search(' + obj.uname + ',' + obj.action + ',"' + moment(obj.date).format("YYYY-MM-DD") + '");')
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

    function stocklevel(obj) {
        console.log('Module - Reports - stocklevel');

        var deferred = q.defer();

        db.query('CALL spStockLevel();')
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
        audit: auditReport,
        stocklevel: stocklevel
    };
};
