'use strict';


var SpecialModel = require('../../models/special');
var auth = require('../../libs/auth.js');


module.exports = function (router) {

    var model = new SpecialModel();


    router.get('/', function (req, res) {
        res.render('bookings/booking', model)
    });

    router.get('/add', function (req, res) {
        res.render('specials/special-add', model)
    });

    router.get('/birthday', function (req, res) {
        res.render('specials/birthday', model)
    });
};
