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
        var u = {};

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('reports/audit', {"user": u, "id": req.params.id})
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
        var u = {};

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('reports/stocklevel', {"user": u, "id": req.params.id})
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
        var u = {};

        auth.grantAccess(req.session.passport, 9, 5, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('reports/expense', {"user": u, "id": req.params.id})
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
