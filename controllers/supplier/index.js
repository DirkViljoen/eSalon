'use strict';


var supplierModel = require('../../models/supplier');


module.exports = function (router) {

    var model = new supplierModel();


    router.get('/', function (req, res) {
        res.render('supplier', model)
    });

    router.get('/view', function (req, res) {
        res.render('supplier-view', model)
    });

    router.get('/add', function (req, res) {
        res.render('supplier-add', model)
    });

    router.get('/update', function (req, res) {
        res.render('supplier-update', model)
    });
};
