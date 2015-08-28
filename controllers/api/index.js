'use strict';

var BookingModel = require('../../models/booking');
var ClientModel = require('../../models/client');
var EmployeeModel = require('../../models/employee');
var OrdersModel = require('../../models/orders');
var ServiceModel = require('../../models/service');
var StockModel = require('../../models/stock');
var LookupsModel = require('../../models/lookups');

var moment = require('moment');

module.exports = function (router) {

    var models = {};
    models.client = new ClientModel();
    models.employee = new EmployeeModel();
    models.lookup = new LookupsModel();
    models.orders = new OrdersModel();
    models.services = new ServiceModel();
    models.stock = new StockModel();
    models.booking = new BookingModel();

// Bookings

    router.get('/bookings', function (req, res) {
        console.log('Bookings GET. Parameters: ' + JSON.stringify(req.query))

        var fname = "";
        var lname = "";
        var ref = "";

        if (req.query.search) {
            if (req.query.search.indexOf(" ") >= 0) {
                fname = req.query.search.substring(0, req.query.search.indexOf(" "));
                lname = req.query.search.substring(req.query.search.indexOf(" ") + 1, req.query.search.length);
            }
            else {
                fname = req.query.search;
                ref = req.query.search;
            };
        };

        models.booking.find(fname, lname, ref)
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

    router.get('/employees/:eid/bookings', function (req, res) {
        console.log('Bookings for employee GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.employee.bookings(req.params.eid)
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

    router.get('/bookings/:id', function (req, res) {
        console.log('Bookings GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.booking.index(req.params.id)
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

    router.post('/bookings', function (req, res) {
        console.log('Bookings POST. Parameters: ' + JSON.stringify(req.body));

        if (JSON.stringify(req.body) != '{}') {
            var obj = {};
            //booking
            obj.datetime = (req.body.datetime ? '"' + req.body.datetime + '"' : null);
            obj.datetime = obj.datetime.replace("T"," ");
            obj.datetime = obj.datetime.replace("Z","");
            obj.duration = (req.body.duration ? '"' + req.body.duration + '"' : 30);
            obj.completed = (req.body.completed ? '"' + req.body.completed + '"' : 0);
            obj.active = (req.body.active ? '"' + req.body.active + '"' : 1);
            obj.reference = (req.body.reference ? '"' + req.body.reference + '"' : null);
            obj.cid = (req.body.cid ? req.body.cid : null);
            obj.eid = (req.body.eid ? req.body.eid : null);
            obj.iid = (req.body.iid ? req.body.iid : null);
            obj.services = [];

            if (req.body.services) {
                for (var i = 0; i < req.body.services.length; i++) {
                    obj.services.push({hlsid: req.body.services[i].hlsid});
                };
            };

            models.booking.create(obj)
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
            var err = 'No req.body on booking POST';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.put('/bookings/:id', function (req, res) {
        console.log('Bookings PUT. Parameters: ' + JSON.stringify(req.body));

        if (JSON.stringify(req.body) != '{}') {
            var obj = {};
            //booking
            obj.bid = (req.body.bid ? req.body.bid : req.params.id);

            obj.datetime = '"' + moment(req.body.datetime).format('YYYY-MM-DD HH:mm:ss') + '"';

            obj.duration = (req.body.duration ? '"' + req.body.duration + '"' : 60);
            obj.completed = (req.body.completed ? '"' + req.body.completed + '"' : 0);
            obj.active = (req.body.active ? '"' + req.body.active + '"' : 1);
            obj.reference = (req.body.reference ? '"' + req.body.reference + '"' : null);
            obj.eid = (req.body.eid ? req.body.eid : null);
            obj.iid = (req.body.iid ? req.body.iid : null);

            if (req.body.services) {
                for (i = 0; i < req.body.services.length; i++) {
                    t = {
                        bid: obj.bid,
                        hlsid: req.body.services[i].hlsid
                    };

                    obj.services.push(t);
                };
            };

             models.booking.update(obj)
                .then(
                    function (result){
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
            var err = 'No req.body on booking PUT';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.delete('/bookings/:id', function (req, res) {
        console.log('Bookings DELETE. Parameters: ' + JSON.stringify(req.body));

        if (JSON.stringify(req.body) != '{}') {
            var obj = {};
            //booking
            obj.bid = (req.body.bid ? '"' + req.body.bid + '"' : null);

             models.booking.remove(obj)
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
            var err = 'No req.body on booking DELETE';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

// Clients

    router.get('/clients/', function (req, res) {
        console.log('Clients GET. Parameters: ' + JSON.stringify(req.query))
        var fname = "";
        var lname = "";

        if (req.query.fname) {fname = req.query.fname};
        if (req.query.lname) {lname = req.query.lname};

        models.client.find(fname, lname)
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

        models.client.index(req.params.id)
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

        models.client.address(req.params.id)
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

        models.client.services(req.params.id)
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

        models.client.products(req.params.id)
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

             models.client.create(obj)
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

             models.client.update(obj)
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

             models.client.remove(obj)
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

// Employees

    router.get('/employees/', function (req, res) {
        console.log('Employees GET. Parameters: ' + JSON.stringify(req.query))
        var fname = "";
        var lname = "";
        var role = "";

        if (req.query.fname) {fname = req.query.fname};
        if (req.query.lname) {lname = req.query.lname};
        if (req.query.role) {role = req.query.role};

        models.employee.find(fname, lname, role)
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

    router.get('/employees/:id', function (req, res) {
        console.log('Employees GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.employee.index(req.params.id)
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

// Services

    router.get('/services', function (req, res) {
        console.log('Services GET. Parameters: ' + JSON.stringify(req.query))

        var sname = (req.query.service ? req.query.service : "");

        models.services.find(sname)
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

    router.get('/services/hairlengthservices', function (req, res) {
        console.log('hair length services GET. Parameters: ' + JSON.stringify(req.params))

        models.services.hairlengthservices()
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

// Stock

    router.get('/stock', function (req, res) {
        console.log('Stock GET. Parameters: ' + JSON.stringify(req.query))

        var sname = "";
        var bname = "";
        var pname = "";

        sname = (req.query.sname ? '"' + req.query.sname + '"' : "");
        pname = (req.query.pname ? '"' + req.query.pname + '"' : "");
        bname = (req.query.bname ? '"' + req.query.bname + '"' : "");

        models.stock.find(sname, pname, bname)
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

// Lookups
    router.get('/lookups/paymentMethods', function (req, res) {
        console.log('get payment methods');
        console.log('request body: ' + JSON.stringify(req.params));

        models.lookup.paymentMethods()
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

        models.lookup.provinces()
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

        models.lookup.cities(req.params.provinceID)
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

        models.lookup.suburbs(req.params.cityID)
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

        models.lookup.notificationMethods(req.params.cityID)
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });


    router.get('/lookups/hairlength', function (req, res) {
        console.log('get hairlengths');
        console.log('request body: ' + JSON.stringify(req.params));

        models.lookup.hairlengths()
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

};
