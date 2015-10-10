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

    function expenseReport(obj) {
        console.log('Module - Reports - Expense');

        var deferred = q.defer();

        db.query('CALL spExpenseReport(' + obj.dateFrom + ','
                                          + obj.dateTo
                                          + ');')
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

    function employeeReport(obj) {
        console.log('Module - Reports - Employee');

        var deferred = q.defer();

        db.query('CALL spEployeeIncome(' + obj.name + ',' + obj.surname + ');')
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

    function stockReport(obj) {
        console.log('Module - Reports - invoice Stock');

        var deferred = q.defer();

        db.query('CALL spIncomeReport_stock(' + obj.dateFrom + ','
                                          + obj.dateTo
                                          + ');')
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

    function serviceReport(obj) {
        console.log('Module - Reports - Service');

        var deferred = q.defer();

        db.query('CALL spIncomeReport_services(' + obj.dateFrom + ','
                                          + obj.dateTo
                                          + ');')
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

    function istockReport(obj) {
        console.log('Module - Reports - invoice Stock');

        var deferred = q.defer();

        db.query('CALL spIncomeReport_Istock(' + obj.dateFrom + ','
                                          + obj.dateTo
                                          + ');')
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

    function iserviceReport(obj) {
        console.log('Module - Reports - invoice Services');

        var deferred = q.defer();

        db.query('CALL spIncomeReport_Iservices(' + obj.dateFrom + ','
                                          + obj.dateTo
                                          + ');')
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

    function subletterReport(obj) {
        console.log('Module - Reports - invoiceSubletter');

        var deferred = q.defer();

        db.query('CALL spSubletterPayment(' + obj.dateFrom + ','
                                          + obj.dateTo
                                          + ');')
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

    function stockBoughtReport(obj) {
        console.log('Module - Reports - stockBought');

        var deferred = q.defer();

        db.query('CALL spStockTrends_bought(' + obj.dateFrom + ','
                                          + obj.dateTo + ','
                                          + obj.name
                                          + ');')
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

    function stockSoldReport(obj) {
        console.log('Module - Reports - stockSold');

        var deferred = q.defer();

        db.query('CALL spStockTrends_sold(' + obj.dateFrom + ','
                                          + obj.dateTo + ','
                                          + obj.name
                                          + ');')
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

    function clientReport(obj) {
        console.log('Module - Reports - client');

        var deferred = q.defer();

        db.query('CALL spClient();')
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
        stocklevel: stocklevel,
        expense: expenseReport,
        employee: employeeReport,
        invoiceStock: stockReport,
        invoiceService: serviceReport,
        invoiceIStock: istockReport,
        invoiceIService: iserviceReport,
        invoiceSubletter: subletterReport,
        stockSold: stockSoldReport,
        stockBought: stockBoughtReport,
        client: clientReport
    };
};
