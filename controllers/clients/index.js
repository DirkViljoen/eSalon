'use strict';

var auth = require('../../libs/auth');

module.exports = function (router) {

// Navigation

    router.get('/', auth.isAuthenticated(), function (req, res) {
        res.render('clients/client', {})
    });

    router.get('/add', function (req, res) {
        res.render('clients/client-add', {"test":null})
    });

    router.get('/view/:id', function (req, res) {
        console.log('Client View Get')
        res.render('clients/client-view', req.params)
    });

    router.get('/update/:id', function (req, res) {
        console.log('Client Update Get');
        console.log(req.params);
        res.render('clients/client-update', req.params)
    });
};
