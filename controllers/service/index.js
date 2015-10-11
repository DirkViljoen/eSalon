'use strict';


var ServiceModel = require('../../models/service');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new ServiceModel();


    router.get('/', function (req, res) {
        // res.render('services/service', {})
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#services';

        auth.grantAccess(req.session.passport, 4, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('services/service', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#services-add';

        auth.grantAccess(req.session.passport, 4, 1, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('services/service-add', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#services-view';

        auth.grantAccess(req.session.passport, 4, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('services/service-view', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#services-update';

        auth.grantAccess(req.session.passport, 4, 2, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('services/service-update', m)
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
