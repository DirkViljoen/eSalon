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

String.prototype.hexDecode = function(){
    var j;
    var hexes = this.match(/.{1,4}/g) || [];
    var back = "";
    for(j = 0; j<hexes.length; j++) {
        back += String.fromCharCode(parseInt(hexes[j], 16));
    }

    return back;
};

exports.localStrategy = function () {

    return new LocalStrategy(function (username, password, done) {
        console.log('auth.js - localStrategy');
        console.log(username);
        console.log(password.hexEncode());
        var user = {};

        usermodel.findOne(username, password.hexEncode())
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

function deserialize(uid) {
    console.log('Deserializing user');
    console.log('uid:' + uid);

    var user = {};
    var deferred = q.defer();

    usermodel.getOne(uid)
    .then(
        function (result){
            if (result.rows.length > 0) {
                console.log('user found')
                user.uid = result.rows[0].uid;
                user.rid = result.rows[0].rid;
                user.name = result.rows[0].uname;
                user.image = result.rows[0].image;
                deferred.resolve(user);
            }
            else
            {
                console.log('user not found');
                deferred.resolve({});
            }
        },
        function (err){
            console.error(err);
            deferred.reject(err);
        }
    );
    return deferred.promise;
}

function determineAccess(rid, major, minor){
    console.log('Get access permissions');

    var deferred = q.defer();
    var r = {};
    r.access = false;

    usermodel.getaccess(rid, major, minor)
    .then(
        function (result){

            if (result.rows.length > 0) {
                // console.log('have access');
                r.access = true
                deferred.resolve(r);
            }
            else
            {
                // console.log('do not have access');
                deferred.resolve(r);
            }
        },
        function (err){
            deferred.reject(err);
        }
    );

    return deferred.promise;
}

exports.grantAccess = function(user, major, minor, source){
    var r = {};
    r.granted = false;
    r.user = {};
    r.message = "";
    r.button = "";
    r.destination = "";
    r.header = "";


    var deferred = q.defer();

    if (user != undefined){
        deserialize(user.user)
            .then(function (result){
                r.user = result;
                if (r.user != {}){
                    determineAccess(r.user.rid, major, minor)
                    .then(
                        function (result){
                            console.log(result);
                            if (result.access == true){
                                r.granted = true;
                                deferred.resolve(r);
                            }
                            else {
                                r.header = "Access denied";
                                r.message = "You do not have permission to access this page";
                                r.button = "Back";
                                r.destination = source;
                                deferred.resolve(r);
                            }
                        },
                        function (err){
                            r.header = "Access denied";
                            r.message = "Could not determine if you have access. Please try again.";
                            r.button = "Back";
                            r.destination = source;
                            deferred.resolve(r);
                        }
                    )
                }
                else
                {
                    r.header = "Session expired";
                    r.message = "Your session has expired. please log in again.";
                    r.button = "Login";
                    r.destination = "/login/";
                    deferred.resolve(r);
                }
            },
            function (err){
                r.header = "Authentication failed";
                r.message = "We could not retrieve you authentication details. Please log in again.";
                r.button = "Login";
                r.destination = "/login/";
                deferred.resolve(r);
            })
    }
    else{
        r.header = "You are not logged in";
        r.message = "Please log in to gain access to this page.";
        r.button = "Login";
        r.destination = "/login/";
        deferred.resolve(r);
    };

    return deferred.promise;

}
