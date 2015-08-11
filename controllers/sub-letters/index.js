'use strict';


var SubLettersModel = require('../../models/sub-letters');
var app = require('express')();
var bodyParser = require('body-parser');


module.exports = function (router) {

    var model = new SubLettersModel();

    router.get('/', function (req, res) {
        console.log(req.params);
        //model = new SubLettersModel();
        res.render('subletters/subletter', model.find());
        //res.send('<code><pre>' + JSON.stringify(model.index()) + '</pre></code>')
        //res.render('subletters/subletter', model.index());
    });

    router.post('/', function (req, res) {
        console.log(req.body);
        //model = new SubLettersModel();
        //var temp = model.index(req.body.sBusinessName))
        res.render('subletters/subletter', model.find(req.body.sBusinessName));
        //res.send('<code><pre>' + JSON.stringify(model.index(req.body.sBusinessName)) + '</pre></code>')
        //res.render('subletters/subletter', model.index());
    });

    router.get('/add', function (req, res) {
        res.render('subletters/subletter-add', model)
    });

    router.post('/add/', function (req, res) {
        console.log(req.params);
        var index = model.create(req.body);
        res.render('subletters')
    });

    router.get('/update', function (req, res) {
        res.render('subletters/subletter-update', model.index(req.body.sID))
    });

    router.get('/view', function (req, res) {
        res.render('subletters/subletter-view', model)
    });

    router.get('/payment', function (req, res) {
        res.render('subletters/subletter-payment', model)
    });
};
