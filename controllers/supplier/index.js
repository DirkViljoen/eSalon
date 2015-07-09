'use strict';


var supplierModel = require('../../models/supplier');


module.exports = function (router) {

    var model = new supplierModel();


    router.get('/', function (req, res) {

        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');

    });

};
