'use strict';


var EmployeeModel = require('../../models/employee');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new EmployeeModel();
    var lastPage = ''


    router.get('/', function (req, res) {
        var u = {};

        auth.grantAccess(req.session.passport, 6, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;
            console.log(result);

            if (result.granted){
                res.render('employees/employee', {"user": u})
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

        auth.grantAccess(req.session.passport, 6, 1, req.header('Referer'))
        .then(function (result){
            u = result.user;
            console.log(result);

            if (result.granted){
                res.render('employees/employee-add', {"user": u})
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
        var u = {};

        auth.grantAccess(req.session.passport, 6, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('employees/employee-view', {"user": u, "id": req.params.id})
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
        var u = {};

        auth.grantAccess(req.session.passport, 6, 2, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('employees/employee-update', {"user": u, "id": req.params.id})
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
        var u = {};

        auth.grantAccess(req.session.passport, 6, 4, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('employees/employee-schedule', {"user": u, "id": req.params.id})
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
        var u = {};

        auth.grantAccess(req.session.passport, 6, 2, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('employees/employee-schedule-edit', {"user": u, "id": req.params.id})
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
