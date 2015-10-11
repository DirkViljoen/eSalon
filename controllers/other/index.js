'use strict';

var OtherModel = require('../../models/other');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new OtherModel();

    router.get('/expenses', function (req, res) {

        // res.render("Import/ImportExpenses", {});
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#expenses-import';

        auth.grantAccess(req.session.passport, 8, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('Import/ImportExpenses', m)
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

};
