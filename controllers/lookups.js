'use strict';

var LookupsModel = require('../models/lookups');


module.exports = function (router) {

    var model = new LookupsModel();

    router.get('/', function (req, res) {
        
        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        
    });

};
