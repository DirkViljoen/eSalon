'use strict';

var IndexModel = require('../models/login');

module.exports = function (router) {
    var model = new IndexModel();

    router.get('/', function (req, res) {
        res.render('login/login', model);
    });
};
