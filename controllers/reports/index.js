'use strict';

var ReportsModel = require('../../models/reports');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new ReportsModel();

    router.get('/', function (req, res) {
        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
    });

    router.get('/audit', function (req, res) {
        // res.render('reports/audit', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#Reports';

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('reports/audit', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });

    router.get('/stocklevel', function (req, res) {
        // res.render('reports/stocklevel', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#Reports';

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('reports/stocklevel', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });

    router.get('/expense', function (req, res) {
        // res.render('reports/expense', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#Reports-Expense Report';

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('reports/expense', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });

    router.get('/employee', function (req, res) {
        // res.render('reports/employee', {});
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#Reports-Generate Employee Income';

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('reports/employee', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });

    router.get('/income', function (req, res) {
        // res.render('reports/income', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#reports';

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('reports/income', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });

    router.get('/stocktrend', function (req, res) {
        // res.render('reports/stocktrend', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#Reports-Stock Trend';

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('reports/stocktrend', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });

    router.get('/client', function (req, res) {
        // res.render('reports/client', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#Reports-Client Report';

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('reports/client', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });
};
