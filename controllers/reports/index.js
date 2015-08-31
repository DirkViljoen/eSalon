'use strict';

var ReportsModel = require('../../models/reports');


module.exports = function (router) {

    var model = new ReportsModel();

    router.get('/', function (req, res) {
        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
    });

    router.get('/audit', function (req, res) {
        res.render('reports/audit', {})
    });

};
