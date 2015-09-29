'use strict';


var BookingModel = require('../../models/booking');


module.exports = function (router) {

    var model = new BookingModel();


    router.get('/', function (req, res) {
        console.log('Bookings - query: ' + JSON.stringify(req.query));
        var obj = {};
        obj.stylist = (req.query.stylist ? req.query.stylist : 1);
        obj.view = (req.query.view ? req.query.view : 'month');
        res.render('bookings/booking', obj)
    });

    router.get('/add', function (req, res) {
        console.log('Booking add with params: ' + JSON.stringify(req.params))
        res.render('bookings/booking-add', req.query);
    });

    router.get('/update/:id', function (req, res) {
        console.log('Booking add with params: ' + JSON.stringify(req.params))
        res.render('bookings/booking-update', req.params);
    });

    router.get('/makesale', function (req, res) {
        res.render('invoice/makesale', {});
    });

    router.get('/finalise/:id', function (req, res) {
        res.render('invoice/finalisebooking', req.params);
    });

};
