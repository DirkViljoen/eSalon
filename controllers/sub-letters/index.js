'use strict';


var SubLettersModel = require('../../models/sub-letters');


module.exports = function (router) {

    var model = new SubLettersModel();


    router.get('/', function (req, res) {
        console.log('call subletter')
        res.render('subletters/subletter', model)
    });

    router.get('/add', function (req, res) {
        res.render('subletters/subletter-add', model)
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
