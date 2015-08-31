'use strict';

var app = require('./index');
var http = require('http');
var passport = require('./config/passport')


var server;

/*
 * Create and start HTTP server.
 */

// require('./config/passport')(passport); // pass passport for configuration

server = http.createServer(app);
server.listen(process.env.PORT || 8000);
server.on('listening', function () {
    console.log('Server listening on http://localhost:%d', this.address().port);
});
