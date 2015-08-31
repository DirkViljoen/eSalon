'use strict';


var SubLettersModel = require('../../models/sub-letters');
var app = require('express')();
var bodyParser = require('body-parser');

module.exports = function (router) {

    var model = new SubLettersModel();

    router.get('/', function (req, res) {
        console.log('Sub-Letter Search Get');
        console.log('request body: ' + JSON.stringify(req.params));

        model.find()
            .then(
                function (result){
                    res.render('subletters/subletter', result);
                }
            );
    });

    router.post('/', function (req, res) {
        console.log('Sub-Letter Search Post');
        console.log('request body: ' + JSON.stringify(req.body));



        switch(req.body.action) {
            case 'View':
                if (req.body.Sub_Letter_id) {
                    res.redirect('/sub-letters/view/' + req.body.Sub_Letter_id);
                }
                else
                {
                    return null;
                }
                break;
            case 'Update':
                if (req.body.Sub_Letter_id) {
                    res.redirect('/sub-letters/update/' + req.body.Sub_Letter_id)
                }
                else
                {
                    return null;
                }
                break;

            case 'Delete':
                if (req.body.Sub_Letter_id) {
                    res.redirect('/sub-letters/delete/' + req.body.Sub_Letter_id)
                }
                else
                {
                    return null;
                }
                break;

            case 'Capture payment':
                if (req.body.Sub_Letter_id) {
                    res.redirect('/sub-letters/RecievePayment/' + req.body.Sub_Letter_id)
                }
                else
                {
                    return null;
                }
                break;

            default:
                {
                    model.find(req.body.BusinessName)
                        .then(
                            function (result){
                                res.render('subletters/subletter', result);
                            }
                        );
                }
        }
    });

    router.get('/get/:id', function (req, res) {
        console.log('get subLetter by id')
        model.index(req.params.id)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                }
            );
    });

    router.get('/view/:id', function (req, res) {
        console.log('Sub-Letter View Get');
        model.index(req.params.id)
            .then(
                function (result){
                    if (result.rows[0])
                        res.render('subletters/subletter-view', result.rows[0]);
                }
            );
    });

    router.get('/update/:id', function (req, res) {
        console.log('Sub-Letter Update Get');
        model.index(req.params.id)
            .then(
                function (result){
                    if (result.rows[0])
                        res.render('subletters/subletter-update', result.rows[0]);
                }
            );
    });

    router.post('/update', function (req, res) {
        console.log('Sub-Letter Update Post');
        console.log('request body: ' + JSON.stringify(req.body));

        if (req.body) {
            var index = model.update(req.body);
            res.redirect('/sub-letters');
        }
        else
        {
            console.log('empty body on sub-letter update');
            res.redirect('/sub-letters');
        }
    })

    router.get('/add', function (req, res) {
        console.log('Sub-Letter Add Get');
        console.log('request body: ' + JSON.stringify(req.params));

        model.index('0')
            .then(
                function (result){
                    res.render('subletters/subletter-add', result);
                }
            );
    });

    router.post('/add', function (req, res) {
        console.log('Sub-Letter Add Post');
        console.log('request body: ' + JSON.stringify(req.body));

        if (req.body) {
            var index = model.create(req.body);
            res.redirect('/sub-letters');
        }
        else
        {
            console.log('empty body on add');
            res.render('subletters/subletter-add', model)
        }
    });

    router.get('/delete/:id', function (req, res) {
        console.log('request param: ' + JSON.stringify(req.params));
        if (req.params) {
            var index = model.deactivate(req.params);
            res.redirect('/sub-letters');
        }
        else
        {
            console.log('empty params on sub-letter delete');
            res.redirect('/sub-letters');
        }
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
            var index = model.capturePayment(req.body);
            res.redirect('/sub-letters');
        }
        else
        {
            console.log('empty body on capture sub-letter payment sub-letter');
            res.redirect('/sub-letters');
        }
    })

    router.get('/PaymentMethods', function (req, res) {
        console.log('Sub-Letter Recieve Payment');
        model.index(req.params.id)
            .then(
                function (result){
                    if (result.rows[0])
                        console.log(result.rows);
                        console.log(JSON.parse('{"id":1,"content":"hello angular"}'));
                        res.send(JSON.parse('{"id":1,"content":"hello angular"}'));
                        //res.render('subletters/subletter-payment', result.rows[0]);
                }
            );
    });
};
