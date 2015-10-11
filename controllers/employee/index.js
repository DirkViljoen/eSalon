'use strict';


var EmployeeModel = require('../../models/employee');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new EmployeeModel();
    var lastPage = ''


    router.get('/', function (req, res) {
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#employees';

        auth.grantAccess(req.session.passport, 6, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;
            console.log(result);

            if (result.granted){
                res.render('employees/employee', m)
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
        m.h = '/help#employees-add';

        auth.grantAccess(req.session.passport, 6, 1, req.header('Referer'))
        .then(function (result){
            u = result.user;
            console.log(result);

            if (result.granted){
                res.render('employees/employee-add', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#employees-view';

        auth.grantAccess(req.session.passport, 6, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('employees/employee-view', m)
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
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#employees-update';

        auth.grantAccess(req.session.passport, 6, 2, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('employees/employee-update', m)
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

    router.get('/change-password/:id', function (req, res) {
        res.render('employees/employee-change-password', req.params)
    });

    router.get('/schedule', function (req, res) {
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#employees-schedule';

        auth.grantAccess(req.session.passport, 6, 4, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('employees/employee-schedule', m)
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

    router.get('/schedule-edit', function (req, res) {
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#employees-schedule-edit';

        auth.grantAccess(req.session.passport, 6, 2, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('employees/employee-schedule-edit', m)
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
