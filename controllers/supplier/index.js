'use strict';

module.exports = function (router) {

    router.get('/', function (req, res) {
        res.render('suppliers/supplier', {})
    });

    router.get('/view/:id', function (req, res) {
        res.render('suppliers/supplier-view', req.params)
    });

    router.get('/add', function (req, res) {
        res.render('suppliers/supplier-add', {})
    });

    router.get('/update/:id', function (req, res) {
        res.render('suppliers/supplier-update', req.params)
    });
};
