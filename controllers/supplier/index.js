'use strict';

var auth = require('../../libs/auth.js');

module.exports = function (router) {

    router.get('/', function (req, res) {
        // res.render('suppliers/supplier', {})
        var u = {};

        auth.grantAccess(req.session.passport, 5, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('suppliers/supplier', {"user": u, "id": req.params.id})
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
        // res.render('suppliers/supplier-view', req.params)
        var u = {};

        auth.grantAccess(req.session.passport, 5, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('suppliers/supplier-view', {"user": u, "id": req.params.id})
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
        // res.render('suppliers/supplier-add', {})
        var u = {};

        auth.grantAccess(req.session.passport, 5, 1, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('suppliers/supplier-add', {"user": u, "id": req.params.id})
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
        // res.render('suppliers/supplier-update', req.params)
        var u = {};

        auth.grantAccess(req.session.passport, 5, 2, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('suppliers/supplier-update', {"user": u, "id": req.params.id})
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
