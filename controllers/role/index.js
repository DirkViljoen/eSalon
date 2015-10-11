'use strict';


var RoleModel = require('../../models/role');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new RoleModel();


    router.get('/add', function (req, res) {
        // res.render('role/role-add', model);
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#roles-add';

        auth.grantAccess(req.session.passport, 8, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('role/role-add', m)
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

    router.get('/maintain', function (req, res) {
        // res.render('role/role-maintain', model);
        var m = {};
        m.p = req.params;
        m.q = req.query;
        m.user = {};
        m.h = '/help#roles-edit';

        auth.grantAccess(req.session.passport, 8, 5, req.header('Referer'))
        .then(function (result){
            m.user = result.user;

            if (result.granted){
                res.render('role/role-maintain', m)
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
