'use strict';
var uploadLocation = './uploads/';

var express     = require('express'),
    kraken      = require('kraken-js'),
    db          = require('./libs/db'),
    sms         = require('./libs/sms'),
    multer      = require('multer'),
    upload      = multer({ dest: uploadLocation});

var options, app;

/*
 * Create and configure application. Also exports application instance for use by tests.
 * See https://github.com/krakenjs/kraken-js#options for additional configuration options.
 */

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

app = module.exports = express();
app.use(kraken(options));

// Getting multer file upload to work
app.use(multer({ dest: uploadLocation,
    rename: function (fieldname, filename) {
        return filename+Date.now();
    },
    onFileUploadStart: function (file) {
        console.log(file.originalname + ' is starting ...');
    },
    onFileUploadComplete: function (file) {
        console.log(file.fieldname + ' uploaded to  ' + file.path)
    }
}));

app.on('start', function () {
    console.log('Application ready to serve requests.');
    console.log('Environment: %s', app.kraken.get('env:env'));
});
