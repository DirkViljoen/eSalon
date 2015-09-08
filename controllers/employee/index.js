'use strict';


var EmployeeModel = require('../../models/employee');


module.exports = function (router) {

    var model = new EmployeeModel();
    var lastPage = ''


    router.get('/', function (req, res) {
        res.render('employees/employee', {})
    });

    router.get('/add', function (req, res) {
        res.render('employees/employee-add', {})
    });

    router.get('/view/:id', function (req, res) {
        res.render('employees/employee-view', req.params)
    });

    router.get('/update/:id', function (req, res) {
        res.render('employees/employee-update', req.params)
    });

    router.get('/change-password/:id', function (req, res) {
        res.render('employees/employee-change-password', req.params)
    });

    router.get('/schedule', function (req, res) {
        res.render('employees/employee-schedule', model)
    });

    router.get('/schedule-edit', function (req, res) {
        res.render('employees/employee-schedule-edit', model)
    });


};
