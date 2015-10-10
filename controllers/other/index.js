'use strict';

var OtherModel = require('../../models/other');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new OtherModel();

    router.get('/expenses', function (req, res) {

        // res.render("Import/ImportExpenses", {});
        var u = {};

        auth.grantAccess(req.session.passport, 8, 5, req.header('Referer'))
        .then(function (result){
            u = result.user;

            if (result.granted){
                res.render('Import/ImportExpenses', {"user": u, "id": req.params.id})
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
