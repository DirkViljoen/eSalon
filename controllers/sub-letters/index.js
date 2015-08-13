'use strict';


var SubLettersModel = require('../../models/sub-letters');
var app = require('express')();
var bodyParser = require('body-parser');


module.exports = function (router) {

    var model = new SubLettersModel();

    router.get('/', function (req, res) {
        console.log('request body: ' + JSON.stringify(req.body));
        model.find()
            .then(
                function (result){
                    res.render('subletters/subletter', result);
                }
            );
    });

    router.post('/', function (req, res) {
        console.log('request body: ' + JSON.stringify(req.body));

        switch(req.body.action) {
            case 'View':
                //res.render('subletters/subletter-view', model)
                {
                    model.index(req.body.subletter)
                        .then(
                            function (result){
                                if (result.rows[0])
                                    res.render('subletters/subletter-view', result.rows[0]);

                            }
                        );
                }
                break;
            case 'Update':
                res.render('subletters/subletter-update', model.find(req.body.subletter))
                break;

            case 'Delete':
                {
                    model.find(req.body.sBusinessName)
                        .then(
                            function (result){

                                res.render('subletters/subletter', result);
                            }
                        );
                }
                break;

            case 'Capture payment':
                res.render('subletters/subletter-payment', model)
                break;

            default:
                {
                    model.find(req.body.sBusinessName)
                        .then(
                            function (result){
                                res.render('subletters/subletter', result);
                            }
                        );
                }
        }
    });

    router.get('/add', function (req, res) {
        res.render('subletters/subletter-add', model)
    });

    router.post('/add/', function (req, res) {
        console.log('request body: ' + JSON.stringify(req.body));
        if (req.body) {
            var index = model.create(req.body);
            res.render('subletters');
        }
        else
        {
            console.log('empty body on add');
            res.render('subletters/subletter-add', model)
        }
    });

    router.post('/edit', function (req, res) {
        console.log(req.body);

        res.render('subletters/subletter-add', model);
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
