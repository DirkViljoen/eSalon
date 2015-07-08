'use strict';


var EmployeeModel = require('../../models/employee');


module.exports = function (router) {

    var model = new EmployeeModel();


    router.get('/', function (req, res) {
        //You can find me at /employee
        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
    });

    router.get('/add', function (req, res) {
      console.log('test add');
        //You can find me at /employee/add
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-add', model)
    });

    router.get('/add-image', function (req, res) {
      console.log('test add');
        //You can find me at /employee/add-image
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('employee-add-image', model)
    });
};
