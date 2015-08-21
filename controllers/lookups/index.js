'use strict';

var LookupsModel = require('../../models/lookups');


module.exports = function (router) {

    var model = new LookupsModel();

    router.get('/', function (req, res) {

        res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');

    });

    router.get('/paymentMethods', function (req, res) {
        console.log('get payment methods');
        console.log('request body: ' + JSON.stringify(req.params));

        model.paymentMethods()
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

    router.get('/provinces', function (req, res) {
        console.log('get payment methods');
        console.log('request body: ' + JSON.stringify(req.params));

        model.provinces()
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

    router.get('/cities/:provinceID', function (req, res) {
        console.log('get payment methods');
        console.log('request body: ' + JSON.stringify(req.params));

        model.cities(req.params.provinceID)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

    router.get('/suburbs/:cityID', function (req, res) {
        console.log('get payment methods');
        console.log('request body: ' + JSON.stringify(req.params));

        model.suburbs(req.params.cityID)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

};
