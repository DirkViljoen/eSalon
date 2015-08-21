'use strict';


var SubLettersModel = require('../../models/sub-letters');
var app = require('express')();
var bodyParser = require('body-parser');

module.exports = function (router) {

    var model = new SubLettersModel();

    router.get('/', function (req, res) {
        console.log('get all subletters')
        model.find()
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    });

    router.get('/:id', function (req, res) {
        console.log('get subLetter by id')
        model.index(req.params.id)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    });

    router.post('/', function (req, res) {
        console.log('Sub-Letter Create');
        console.log('request body: ' + JSON.stringify(req.body));

        if (req.body) {
             model.create(req.body)
                .then(
                    function (result){
                        console.log(result);
                        res.send(result);
                    }
                );
        }
        else
        {
            console.log('empty body on sub-letter add');
            res.send(err('No body'));
        }
    });

    router.put('/:id', function (req, res) {
        console.log('Sub-Letter Update Post');
        console.log('request body: ' + JSON.stringify(req.body));

        if (req.body) {
             model.update(req.body)
                .then(
                    function (result){
                        console.log(result);
                        res.send(result);
                    }
                );
        }
        else
        {
            console.log('empty body on sub-letter update');
        }
    })

    router.delete('/:id', function (req, res) {
        console.log('Sub-Letter Delete');
        console.log('request params: ' + JSON.stringify(req.params));
        model.deactivate(req.params.id)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    })

    router.get('/manage/sl', function (req, res) {
        console.log('Sub-Letter Search Get');
        console.log('request body: ' + JSON.stringify(req.params));
        res.render('subletters/subletter', req.params);
    });

    router.get('/add/new', function (req, res) {
        console.log('Sub-Letter Add Get');
        console.log('request body: ' + JSON.stringify(req.params));
        res.render('subletters/subletter-add', req.params);
    });

    router.get('/view/:id', function (req, res) {
        console.log('Sub-Letter View Get');
        console.log(req.params);
        res.render('subletters/subletter-view', req.params);
    });

    router.get('/update/:id', function (req, res) {
        console.log('Sub-Letter Update Get');
        console.log(req.params);
        res.render('subletters/subletter-update', req.params);
    });

    router.get('/RecievePayment/:id', function (req, res) {
        console.log('Router - Sub-Letter - RecievePayment - ' + req.params.id);
        console.log(req.params);
        res.render('subletters/subletter-payment', req.params);
    });

    router.post('/payment', function (req, res) {
        console.log('Sub-Letter RecievePayment Post');
        console.log('request body: ' + JSON.stringify(req.body));

        if (req.body) {
             model.capturePayment(req.body)
                .then(
                    function (result){
                        console.log(result);
                        res.send(result);
                    }
                );
        }
        else
        {
            console.log('empty body on capture sub-letter payment sub-letter');
        }
    })

    router.post('/search', function (req, res) {
        console.log('search subletters')
        model.find(req.body.businessName)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    })

    router.get('/:id/payments', function (req, res) {
        console.log('Sub-Letter Recieve Payment');
        console.log(req.params);

        model.previousPayments(req.params.id)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });
};
