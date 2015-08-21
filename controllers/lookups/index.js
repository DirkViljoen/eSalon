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

};
