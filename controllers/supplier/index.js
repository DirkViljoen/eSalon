'use strict';

var auth = require('../../libs/auth.js');

module.exports = function (router) {

    router.get('/', function (req, res) {
        // res.render('suppliers/supplier', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#suppliers';

        auth.grantAccess(req.session.passport, 5, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('suppliers/supplier', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#suppliers-view';

        auth.grantAccess(req.session.passport, 5, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('suppliers/supplier-view', m
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#suppliers-add';

        auth.grantAccess(req.session.passport, 5, 1, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('suppliers/supplier-add', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#suppliers-update';

        auth.grantAccess(req.session.passport, 5, 2, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('suppliers/supplier-update', m)
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
