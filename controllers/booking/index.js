'use strict';


var BookingModel = require('../../models/booking');


module.exports = function (router) {

    var model = new BookingModel();


    router.get('/', function (req, res) {
        res.render('bookings/booking', {})
    });

    router.get('/add', function (req, res) {
        console.log('Booking add with query: ' + JSON.stringify(req.query))
        res.render('bookings/booking-add', req.query);
    });

    router.get('/update/:id', function (req, res) {
        res.render('bookings/booking-update', req.params);
    });

    router.get('/makesale', function (req, res) {
        res.render('invoice/makesale', {});
    });

    router.get('/finalise/:id', function (req, res) {
        res.render('invoice/finalisebooking', req.params);
    });

};
