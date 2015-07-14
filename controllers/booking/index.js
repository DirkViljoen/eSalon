'use strict';


var BookingModel = require('../../models/booking');


module.exports = function (router) {

    var model = new BookingModel();


    router.get('/', function (req, res) {
        res.render('booking', model);
    });

    router.get('/invoice', function (req, res) {
        res.render('invoice-add', model);
    });

};
