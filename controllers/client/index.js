'use strict';


var ClientModel = require('../../models/client');


module.exports = function (router) {

    var model = new ClientModel();


    router.get('/', function (req, res) {
        res.render('client', model)
    });

    router.get('/add', function (req, res) {
        res.render('client-add', model)
    });

    router.get('/update', function (req, res) {
        res.render('client-update', model)
    });

    router.get('/view', function (req, res) {
        res.render('client-view', model)
    });
};
