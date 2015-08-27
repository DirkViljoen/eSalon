'use strict';

var OrdersModel = require('../../models/orders');


module.exports = function (router) {

    var model = new OrdersModel();

    router.get('/', function (req, res) {

        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');

    });

};
