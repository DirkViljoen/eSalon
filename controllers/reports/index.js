'use strict';

var ReportsModel = require('../../models/reports');


module.exports = function (router) {

    var model = new ReportsModel();

    router.get('/', function (req, res) {
        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
    });

    router.get('/audit', function (req, res) {
        res.render('reports/audit', {})
    });

    router.get('/stocklevel', function (req, res) {
        res.render('reports/stocklevel', {})
    });

    router.get('/expense', function (req, res) {
        res.render('reports/expense', {})
    });

    router.get('/employee', function (req, res) {
        res.render('reports/employee', {})
    });

    router.get('/income', function (req, res) {
        res.render('reports/income', {})
    });

    router.get('/stocktrend', function (req, res) {
        res.render('reports/stocktrend', {})
    });

    router.get('/client', function (req, res) {
        res.render('reports/client', {})
    });
};
