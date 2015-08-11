'use strict';


var SubLettersModel = require('../../models/sub-letters');


module.exports = function (router) {

    var model = new SubLettersModel();


    router.get('/', function (req, res) {
        console.log(req.body);
        //model = new SubLettersModel();
        res.render('subletters/subletter', model.index());
        //res.send('<code><pre>' + JSON.stringify(model.index()) + '</pre></code>')
        //res.render('subletters/subletter', model.index());
    });

    router.post('/', function (req, res) {
        console.log(req.body);
        //model = new SubLettersModel();
        //res.render('subletters', model.hello());
        res.send('<code><pre>' + JSON.stringify(model.index(req.body.id)) + '</pre></code>')
        //res.render('subletters/subletter', model.index());
    });

    router.get('/add', function (req, res) {
        res.render('subletters/subletter-add', model)
    });

    router.post('/add/', function (req, res) {
        var index = model.create(req);
        res.render('subletters')
    });

    router.get('/update', function (req, res) {
        res.render('subletters/subletter-update', model)
    });

    router.get('/view', function (req, res) {
        res.render('subletters/subletter-view', model)
    });

    router.get('/payment', function (req, res) {
        res.render('subletters/subletter-payment', model)
    });
};
