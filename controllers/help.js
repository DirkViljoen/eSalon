'use strict';

var HelpModel = require('../models/help');


module.exports = function (router) {

    var model = new HelpModel();

    router.get('/', function (req, res) {

        res.render('help/content', {});

    });

    router.get('/2', function (req, res) {

        res.render('help/content2', {});

    });

};
