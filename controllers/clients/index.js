'use strict';

var auth = require('../../libs/auth');

module.exports = function (router) {

// Navigation

    router.get('/', function (req, res) {
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#clients';

        auth.grantAccess(req.session.passport, 1, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('clients/client', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#clients-Add';

        auth.grantAccess(req.session.passport, 1, 1, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('clients/client-add', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#clients';

        auth.grantAccess(req.session.passport, 1, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('clients/client-view', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#clients-Update';

        auth.grantAccess(req.session.passport, 1, 2, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('clients/client-update', m)
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
