'use strict';


var LoginModel = require('../../models/login');


module.exports = function (router) {

    var model = new LoginModel();

    router.get('/', function (req, res) {
        res.render('login/login',model)
    });

    router.get('/login', function (req, res) {
        res.send('<code><pre>test</pre></code>');
    });

};
