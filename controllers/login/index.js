'use strict';

var passport = require('passport');

module.exports = function (router) {

// Navigation

    router.get('/', function (req, res) {
        res.render('login/login', {})
    });

    router.post('/', function (req, res) {
            console.log(req.body);

            if (req.body.password == "Admin") {
              res.redirect('/booking')
            } else {
              res.redirect('/login')
            }
        ;
    });
};
