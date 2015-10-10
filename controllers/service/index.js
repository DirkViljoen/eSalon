'use strict';


var ServiceModel = require('../../models/service');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new ServiceModel();


    router.get('/', function (req, res) {
        // res.render('services/service', {})
        var u = {};

        auth.grantAccess(req.session.passport, 4, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('services/service', {"user": u, "id": req.params.id})
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

    router.get('/add', function (req, res) {
        // res.render('services/service-add', {})
        var u = {};

        auth.grantAccess(req.session.passport, 4, 1, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('services/service-add', {"user": u, "id": req.params.id})
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

    router.get('/view/:id', function (req, res) {
        // res.render('services/service-view', req.params)
        var u = {};

        auth.grantAccess(req.session.passport, 4, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('services/service-view', {"user": u, "id": req.params.id})
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

    router.get('/update/:id', function (req, res) {
        // res.render('services/service-update', req.params)
        var u = {};

        auth.grantAccess(req.session.passport, 4, 2, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('services/service-update', {"user": u, "id": req.params.id})
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
