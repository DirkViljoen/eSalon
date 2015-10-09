'use strict';

var LoginModel = require('../../models/login');
var passport = require('passport');

module.exports = function (router) {

// Navigation

    var model = new LoginModel();

    router.get('/', function (req, res) {
         //Include any error messages that come from the login process.
        //model.messages = req.flash('error');
        res.render('login/login', model);
    });

    // router.post('/', function (req, res) {
    //     console.log(req.body);
    //     passport.authenticate("local", {
    //         successRedirect: "/booking",
    //         failureRedirect: "/login"
    //     })(req, res);

    // });

    router.post('/', passport.authenticate('local'), function(req, res) {
        console.log('user - ' + JSON.stringify(req.user));
        res.redirect('/booking');
    });

    router.get('/logout', function (req, res) {
        req.logout();
        res.redirect('/login');
    });
};
