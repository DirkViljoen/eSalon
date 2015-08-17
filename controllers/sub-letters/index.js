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
                if (req.body.sSub_Letter_id) {
                    res.redirect('/sub-letters/view/' + req.body.sSub_Letter_id);
                }
                else
                {
                    return null;
                }
                break;
            case 'Update':
                if (req.body.sSub_Letter_id) {
                    res.redirect('/sub-letters/update/' + req.body.sSub_Letter_id)
                }
                else
                {
                    return null;
                }
                break;

            case 'Delete':
                if (req.body.sSub_Letter_id) {
                    res.redirect('/sub-letters/delete/' + req.body.sSub_Letter_id)
                }
                else
                {
                    return null;
                }
                break;

            case 'Capture payment':
                if (req.body.sSub_Letter_id) {
                    res.render('subletters/subletter-payment', model)
                }
                else
                {
                    return null;
                }
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

    router.get('/payment', function (req, res) {
        res.render('subletters/subletter-payment', model)
    });
};
