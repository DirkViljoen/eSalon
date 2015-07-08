'use strict';


var ServiceModel = require('../../models/service');


module.exports = function (router) {

    var model = new ServiceModel();


    router.get('/', function (req, res) {
        res.render('service', model)
    });

    router.get('/add', function (req, res) {
        res.render('service-add', model)
    });

    router.get('/view', function (req, res) {
        res.render('service-view', model)
    });

    router.get('/update', function (req, res) {
        res.render('service-update', model)
    });

};
