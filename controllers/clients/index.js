'use strict';

var auth = require('../../libs/auth');

module.exports = function (router) {

// Navigation

    router.get('/', function (req, res) {
        var u = {};

        auth.grantAccess(req.session.passport, 1, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('clients/client', {"user": u})
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
        var u = {};

        auth.grantAccess(req.session.passport, 1, 1, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('clients/client-add', {"user": u})
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
        console.log('Client View Get')
        var u = {};

        auth.grantAccess(req.session.passport, 1, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('clients/client-view', {"user": u, "id": req.params.id})
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
        console.log('Client Update Get');
        var u = {};

        auth.grantAccess(req.session.passport, 1, 2, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('clients/client-update', {"user": u, "id": req.params.id})
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
