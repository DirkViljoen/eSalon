'use strict';
var uploadLocation = './uploads/';

var express     = require('express'),
    kraken      = require('kraken-js'),
    db          = require('./libs/db'),
    sms         = require('./libs/sms'),
    passport    = require('passport'),
    auth        = require('./libs/auth'),
    User        = require('./models/user'),
    usermodel   = new User();

var options, app;

/*
 * Create and configure application. Also exports application instance for use by tests.
 * See https://github.com/krakenjs/kraken-js#options for additional configuration options.
 */

app = module.exports = express();

    //Tell passport to use our newly created local strategy for authentication
    passport.use(auth.localStrategy());

    //Give passport a way to serialize and deserialize a user. In this case, by the user's id.
    passport.serializeUser(function (user, done) {
        console.log('Serializing user');
        done(null, user.uid);
    });

    passport.deserializeUser(function (id, done) {
        console.log('Deserializing user');
        // console.log('id');

        // user = {};

        // usermodel.getOne(id)
        // .then(
        //     function (result){
        //         if (result.rows.length > 0) {
        //             console.log('user found')
        //             user.uid = result.rows[0].uid;
        //             user.rid = result.rows[0].rid;
        //             user.name = result.rows[0].uname;
        //             return done(null, user);
        //         }
        //         else
        //         {
        //             return done(null, false);
        //         }
        //     },
        //     function (err){
        //         console.log(err);
        //         return done(err);
        //     }
        // );
    });


options = {
    onconfig: function (config, next) {
        /*
         * Add any additional config setup or overrides here. `config` is an initialized
         * `confit` (https://github.com/krakenjs/confit/) configuration object.
         */
         db.config(config.get('db'));
         sms.config(config.get('sms'));

         // Host most stuff in the public folder

        next(null, config);
    }
};

    app.use(kraken(options));
    app.use(passport.initialize());
    app.use(passport.session());

app.on('start', function () {
    console.log('Application ready to serve requests.');
    console.log('Environment: %s', app.kraken.get('env:env'));
});
