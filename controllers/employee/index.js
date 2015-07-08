'use strict';


var EmployeeModel = require('../../models/employee');


module.exports = function (router) {

    var model = new EmployeeModel();


    router.get('/', function (req, res) {
        //You can find me at /employee
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee', model)
    });

    router.get('/add', function (req, res) {
        //You can find me at /employee/add
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-add', model)
    });

    router.get('/add-image', function (req, res) {
        //You can find me at /employee/add-image
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-add-image', model)
    });

    router.get('/view', function (req, res) {
        //You can find me at /employee/add-image
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-view', model)
    });

    router.get('/view-image', function (req, res) {
        //You can find me at /employee/add-image
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-view-image', model)
    });

    router.get('/update', function (req, res) {
        //You can find me at /employee/add-image
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-update', model)
    });

    router.get('/update-image', function (req, res) {
        //You can find me at /employee/add-image
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-update-image', model)
    });

};
