'use strict';


var BookingModel = require('../../models/booking');


module.exports = function (router) {

    var model = new BookingModel();


    router.get('/', function (req, res) {
        
        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        
    });

};
