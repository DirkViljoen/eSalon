'use strict';


var BookingModel = require('../../models/booking');


module.exports = function (router) {

    var model = new BookingModel();


    router.get('/', function (req, res) {
        res.render('bookings/booking', model);
    });

    router.get('/add', function (req, res) {
        res.render('bookings/booking-add', model);
    });

    router.get('/update', function (req, res) {
        res.render('bookings/booking-update', model);
    });

    router.get('/makesale', function (req, res) {
        res.render('invoice/makesale', model);
    });

    router.get('/finalise', function (req, res) {
        res.render('invoice/finalisebooking', model);
    });

};
