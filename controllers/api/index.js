'use strict';

var ClientModel = require('../../models/client');
var LookupsModel = require('../../models/lookups');

module.exports = function (router) {

    var model = {};
    model.client = new ClientModel();
    model.lookup = new LookupsModel();

    console.log(model);

// Clients
    router.get('/clients/', function (req, res) {
        console.log('Clients GET. Parameters: ' + JSON.stringify(req.query))
        var fname = "";
        var lname = "";

        if (req.query.fname) {fname = req.query.fname};
        if (req.query.lname) {lname = req.query.lname};

        model.client.find(fname, lname)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    });

    router.get('/clients/:id', function (req, res) {
        console.log('Clients GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        model.client.index(req.params.id)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    });

    router.get('/clients/:id/address', function (req, res) {
        console.log('Clients address GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        model.client.address(req.params.id)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    });

    router.get('/clients/:id/services', function (req, res) {
        console.log('Clients services GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        model.client.services(req.params.id)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    });

    router.get('/clients/:id/products', function (req, res) {
        console.log('Clients products GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        model.client.products(req.params.id)
            .then(
                function (result){
                    if (result)
                        res.send(result);
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
    });

    router.post('/clients', function (req, res) {
        console.log('Clients POST. Parameters: ' + JSON.stringify(req.body));

        if (JSON.stringify(req.body) != '{}') {
            var obj = {};
            //client
            obj.contactTitle = (req.body.title ? '"' + req.body.title + '"' : null);
            obj.contactFName = (req.body.contactFName ? '"' + req.body.contactFName + '"' : null);
            obj.contactLName = (req.body.contactLName ? '"' + req.body.contactLName + '"' : null);
            obj.contactNumber = (req.body.contactNumber ? '"' + req.body.contactNumber + '"' : null);
            obj.contactEmail = (req.body.contactEmail ? '"' + req.body.contactEmail + '"' : null);
            obj.dateOfBirth = (req.body.dateOfBirth ? '"' + req.body.dateOfBirth + '"' : null);
            obj.reminders = (req.body.reminders ? req.body.reminders : true);
            obj.notifications = (req.body.notifications ? req.body.notifications : false);
            obj.notificationMethod = (req.body.notificationMethod ? req.body.notificationMethod : 1);
            //address
            obj.line1 = (req.body.line1 ? '"' + req.body.line1 + '"' : null);
            obj.line2 = (req.body.line2 ? '"' + req.body.line2 + '"' : null);
            obj.suburbId = (req.body.suburbId ? req.body.suburbId : null);

             model.client.create(obj)
                .then(
                    function (result){
                        console.log(result);
                        res.send(result);
                    },
                    function (err){
                        // console.error(err);
                        res.send({'err': err});
                    }
                );
        }
        else
        {
            var err = 'No req.body on clients POST';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.put('/clients/:id', function (req, res) {
        console.log('Clients PUT. Parameters: ' + JSON.stringify(req.body));

        if (JSON.stringify(req.body) != '{}') {
            var obj = {};
            //client
            obj.clientId = (req.body.clientid ? '"' + req.body.clientid + '"' : null);
            obj.contactTitle = (req.body.title ? '"' + req.body.title + '"' : null);
            obj.contactFName = (req.body.contactFName ? '"' + req.body.contactFName + '"' : null);
            obj.contactLName = (req.body.contactLName ? '"' + req.body.contactLName + '"' : null);
            obj.contactNumber = (req.body.contactNumber ? '"' + req.body.contactNumber + '"' : null);
            obj.contactEmail = (req.body.contactEmail ? '"' + req.body.contactEmail + '"' : null);
            obj.dateOfBirth = (req.body.dateOfBirth ? '"' + req.body.dateOfBirth + '"' : null);
            obj.reminders = (req.body.reminders ? req.body.reminders : true);
            obj.notifications = (req.body.notifications ? req.body.notifications : false);
            obj.notificationMethod = (req.body.notificationMethod ? req.body.notificationMethod : 1);
            //address
            obj.addressId = (req.body.addressid ? req.body.addressid : null);
            obj.line1 = (req.body.line1 ? '"' + req.body.line1 + '"' : null);
            obj.line2 = (req.body.line2 ? '"' + req.body.line2 + '"' : null);
            obj.suburbId = (req.body.suburbId ? req.body.suburbId : null);

             model.client.update(obj)
                .then(
                    function (result){
                        console.log(result);
                        res.send(result);
                    },
                    function (err){
                        // console.error(err);
                        res.send({'err': err});
                    }
                );
        }
        else
        {
            var err = 'No req.body on clients PUT';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.delete('/clients/:id', function (req, res) {
        console.log('Clients DELETE. Parameters: ' + JSON.stringify(req.body));

        if (JSON.stringify(req.params) != '{}') {
            var obj = {};
            //client
            obj.clientId = req.params.id;

             model.client.remove(obj)
                .then(
                    function (result){
                        console.log(result);
                        res.send(result);
                    },
                    function (err){
                        // console.error(err);
                        res.send({'err': err});
                    }
                );
        }
        else
        {
            var err = 'No req.params on clients DELETE';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

// Lookups
    router.get('/lookups/paymentMethods', function (req, res) {
        console.log('get payment methods');
        console.log('request body: ' + JSON.stringify(req.params));

        model.lookup.paymentMethods()
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

    router.get('/lookups/provinces', function (req, res) {
        console.log('get provinces');
        console.log('request body: ' + JSON.stringify(req.params));

        model.lookup.provinces()
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

    router.get('/lookups/cities/:provinceID', function (req, res) {
        console.log('get cities based on provinceID');
        console.log('request body: ' + JSON.stringify(req.params));

        model.lookup.cities(req.params.provinceID)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

    router.get('/lookups/suburbs/:cityID', function (req, res) {
        console.log('get suburbs based on cityID');
        console.log('request body: ' + JSON.stringify(req.params));

        model.lookup.suburbs(req.params.cityID)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

    router.get('/lookups/notificationMethods', function (req, res) {
        console.log('get notification methods');
        console.log('request body: ' + JSON.stringify(req.params));

        model.lookup.notificationMethods(req.params.cityID)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });


};
