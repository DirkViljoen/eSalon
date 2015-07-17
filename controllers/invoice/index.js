'use strict';


var InvoiceModel = require('../../models/invoice');


module.exports = function (router) {

    var model = new InvoiceModel();

    router.get('/', function (req, res) {
        res.render('invoice-add', model)
    });

    router.get('/makesale', function (req, res) {
        res.render('invoice/makesale', model)
    });

    router.get('/finalisebooking', function (req, res) {
        res.render('invoice-add', model)
    });

    router.get('/invoice', function (req, res) {
        res.render('login', model);
    });

};
