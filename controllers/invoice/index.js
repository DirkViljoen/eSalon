'use strict';


var InvoiceModel = require('../../models/invoice');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new InvoiceModel();

    // router.get('/', function (req, res) {
    //     res.render('invoice-add', model)
    // });

    router.get('/makesale', function (req, res) {
        // res.render('invoice/makesale', model)
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#makesale';

        auth.grantAccess(req.session.passport, 3, 1, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('invoice/makesale', m)
            }
            else
            {
                res.render('login/accessDenied', result);
            }
        },
        function (err) {
            console.log('An error occurred while trying to find the user');
            res.redirect('/login');
        });
    });

    // router.get('/finalisebooking', function (req, res) {
    //     res.render('invoice-add', model)
    // });

    // router.get('/invoice', function (req, res) {
    //     res.render('login', model);
    // });

};
