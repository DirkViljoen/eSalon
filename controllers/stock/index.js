'use strict';

var StockModel = require('../../models/stock');


module.exports = function (router) {

    var model = new StockModel();

    router.get('/', function (req, res) {

        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');

    });

};
