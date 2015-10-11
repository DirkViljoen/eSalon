'use strict';


var BookingModel    = require('../../models/booking');
var moment          = require('moment');
var auth            = require('../../libs/auth.js');
var jMerge          = require('../../libs/JSONMerge.js');

module.exports = function (router) {

    var model = new BookingModel();

    router.get('/', function (req, res) {
        console.log('Bookings - query: ' + JSON.stringify(req.query));
        console.log(' - session.passport: ' + JSON.stringify(req.session.passport));
        console.log(' - user: ' + JSON.stringify(req.user));
        var obj = {};
        obj.eid = (req.query.eid ? req.query.eid : 1);
        obj.view = (req.query.view ? req.query.view : 'month');
        obj.date = (req.query.date ? req.query.date : moment());

        var m = {};
        m.p = req.params;
        m.q = obj;
        m.user = {};
        m.h = '/help#bookings';

        console.log(m);

        auth.grantAccess(req.session.passport, 2, 1, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('bookings/booking', m)
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
        console.log('Booking add with query: ' + JSON.stringify(req.query))
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#bookings-add';

        console.log(m);

        auth.grantAccess(req.session.passport, 2, 1, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('bookings/booking-add', m)
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
        console.log('Booking add with params: ' + JSON.stringify(req.params))
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#bookings-edit/Cancel';

        auth.grantAccess(req.session.passport, 2, 2, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('bookings/booking-update', m)
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

    router.get('/makesale', function (req, res) {
        var u = {};

        auth.grantAccess(req.session.passport, 3, 1, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('invoice/makesale', {"user": u, "id": req.params.id, "h":"bookings-View/Search Booking"})
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

    router.get('/finalise/:id', function (req, res) {
        // res.render('invoice/finalisebooking', req.params);
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#bookings-finalise';

        auth.grantAccess(req.session.passport, 3, 1, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('invoice/finalisebooking', m)
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
