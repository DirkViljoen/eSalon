'use strict';


var ServiceModel = require('../../models/service');


module.exports = function (router) {

    var model = new ServiceModel();


    router.get('/', function (req, res) {
        res.render('services/service', {})
    });

    router.get('/add', function (req, res) {
        res.render('services/service-add', {})
    });

    router.get('/view/:id', function (req, res) {
        res.render('services/service-view', req.params)
    });

    router.get('/update/:id', function (req, res) {
        res.render('services/service-update', req.params)
    });

};
