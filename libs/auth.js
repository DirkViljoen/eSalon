/**
 * Module that will handle our authentication tasks
 */
'use strict';

var User = require('../models/user'),
    LocalStrategy = require('passport-local').Strategy,
    q = require('q');

var usermodel = new User();

exports.config = function (settings) {

};

exports.localStrategy = function () {

    return new LocalStrategy(function (username, password, done) {
        console.log('auth.js - localStrategy');
        console.log(username);
        console.log(password);
        var user = {};

        usermodel.findOne(username, password)
            .then(
                function (result){
                    if (result.rows.length > 0) {
                        console.log('username/password combination found')
                        user.uid = result.rows[0].uid;
                        user.rid = result.rows[0].rid;
                        user.name = result.rows[0].uname;
                        return done(null, user);
                    }
                    else
                    {
                        console.log('wrong username/password')
                        return done(null, false, { message: 'Incorrect Username/Password combination' });
                    }
                },
                function (err){
                    console.log(err);
                    return done(err);
                }
            );


        // if (username == 'Admin'){
        //     console.log('correct username')
        //     if (password == 'test'){
        //         console.log('correct password')

        //     }
        //     else
        //     {
        //         console.log('wrong password')
        //         return done(null, false, { message: 'Incorrect Password' });
        //     }
        // }
        // else
        // {
        //     console.log('wrong username')
        //     return done(null, false, { message: 'Login not found' });
        // }
        // //Retrieve the user from the database by login
        // User.findOne({login: username}, function (err, user) {

        //     //If something weird happens, abort.
        //     if (err) {
        //         return done(err);
        //     }

        //     //If we couldn't find a matching user, flash a message explaining what happened
        //     if (!user) {
        //         return done(null, false, { message: 'Login not found' });
        //     }

        //     //Make sure that the provided password matches what's in the DB.
        //     if (!user.passwordMatches(password)) {
        //         return done(null, false, { message: 'Incorrect Password' });
        //     }

        //     //If everything passes, return the retrieved user object.
        //     done(null, user);

        // });
    });
}

exports.isAuthenticated = function (role) {


    return function (req, res, next) {
        console.log('auth.js - isAuthenticated');
        if (!req.isAuthenticated()) {

            //If the user is not authorized, save the location that was being accessed so we can redirect afterwards.
            req.session.goingTo = req.url;
            res.redirect('/login');
            return;
        }

        //If a role was specified, make sure that the user has it.
        if (role && req.user.role !== role) {
            res.status(401);
            res.render('errors/401');
        }

        next();
    }
}

exports.injectUser = function (req, res, next) {
    console.log('auth.js - injectUser');

    if (req.isAuthenticated()) {
        res.locals.user = req.user;
    }
    next();
}
