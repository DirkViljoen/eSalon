'use strict';

var
    AddressModel    = require('../../models/address'),
    BookingModel    = require('../../models/booking'),
    ClientModel     = require('../../models/client'),
    EmployeeModel   = require('../../models/employee'),
    OrdersModel     = require('../../models/orders'),
    ServiceModel    = require('../../models/service'),
    StockModel      = require('../../models/stock'),
    SupplierModel   = require('../../models/supplier'),
    UserModel       = require('../../models/user'),
    VouchersModel   = require('../../models/vouchers'),
    LookupsModel    = require('../../models/lookups'),
    ReportsModel    = require('../../models/reports');

var moment          = require('moment');
var q               = require('q');
var sms             = require('../../libs/sms');

module.exports = function (router) {

    var models = {};

    models.address  = new AddressModel();
    models.booking  = new BookingModel();
    models.client   = new ClientModel();
    models.employee = new EmployeeModel();
    models.lookup   = new LookupsModel();
    models.orders   = new OrdersModel();
    models.reports  = new ReportsModel();
    models.services = new ServiceModel();
    // models.specials = new SpecialsModel();
    models.stock    = new StockModel();
    models.supplier = new SupplierModel();
    models.vouchers = new VouchersModel();
    models.user     = new UserModel();

// Address

    router.get('/address/:id', function (req, res) {
        console.log('Employee address GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.address.index(req.params.id)
            .then(
                function (result){
                    if (result) {
                        res.send(result);
                    }
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
     });

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

    router.get('/bookings/:id/services', function (req, res) {
        console.log('Bookings services GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.booking.services(req.params.id)
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

            obj.services = [];

            if (req.body.services) {
                for (var i = 0; i < req.body.services.length; i++) {
                    obj.services.push({hlsid: req.body.services[i].hlsid, bid: req.body.services[i].bid});
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
        console.log('Bookings DELETE. Parameters: ' + JSON.stringify(req.params));

        if (JSON.stringify(req.params) != '{}') {
            var obj = {};
            //booking
            obj.bid = (req.params.id ? '"' + req.params.id + '"' : null);

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
                    if (result) {
                        console.log(result);
                        res.send(result);
                    }
                },
                function (err){
                    console.log(err);
                    res.send(err);
                }
            );
     });

    router.post('/employees', function (req, res) {
       console.log('Employees POST. Parameters: ' + JSON.stringify(req.body));

       if (req.body){
            var obj = req.body;

            //Format attributes correctly for SQL stored procedure
            obj.address.line1 = (obj.address.line1 ? '"' + obj.address.line1 + '"' : null);
            obj.address.line2 = (obj.address.line2 ? '"' + obj.address.line2 + '"' : null);
            obj.address.suburbId = (obj.address.suburbId ? obj.address.suburbId : null);

            obj.employee.cfname = (obj.employee.cfname ? '"' + obj.employee.cfname + '"' : null);
            obj.employee.clname = (obj.employee.clname ? '"' + obj.employee.clname + '"' : null);
            obj.employee.cnumber = (obj.employee.cnumber ? '"' + obj.employee.cnumber + '"' : null);
            obj.employee.cemail = (obj.employee.cemail ? '"' + obj.employee.cemail + '"' : null);
            obj.employee.image = (obj.employee.image ? obj.employee.image : null);
            obj.employee.salary = (obj.employee.salary ? obj.employee.salary : null);
            obj.employee.addressId = null;

            obj.user.name = (obj.user.name ? '"' + obj.user.name + '"' : null);
            obj.user.password = (obj.user.password ? '"' + obj.user.password + '"' : null);
            obj.user.roleId = (obj.user.roleId ? obj.user.roleId : null);

            var deferred = q.defer();
            var promise;
            if ((obj.address.suburbId != null) || (obj.address.line2 != null) || (obj.address.line2 != null)) {
                console.log('Creating address');
                promise = models.address.create(obj.address);
            } else {
                console.log('No address to create');
                deferred.resolve({err: 'No Address'});
                promise = deferred.promise;
            }

            promise.then(
                    function (result){
                        if (result.err == 'No Address'){
                            obj.employee.addressId = null;
                            console.log('address create: No address information for new create');
                        }
                        else {
                            obj.employee.addressId = result.SQLstats.insertId;
                            console.log('address create: Success');
                        }
                    },
                    function (err){
                        obj.employee.addressId = null;
                        console.log('address create: Fail');
                    }
                )
                .then(
                    function (result){
                        console.log('Creating Employee');
                        return models.employee.create(obj.employee)
                            .then(
                                function (result){
                                    obj.user.employeeId = result.SQLstats.insertId;
                                    console.log('employee create: Success');
                                },
                                function (err){
                                    obj.user.employeeId = null;
                                    console.log('employee create: Fail');
                                }
                            )
                    }
                )
                .then(
                    function (result) {
                        if (obj.user.employeeId == null){
                            console.log('Critical error. Cannot create user.');
                            res.send({err: 'Unable to create user details. The employee has not been created'});
                        }
                        else {
                            console.log('Creating User');

                            models.user.create(obj.user)
                                .then(
                                    function (result){
                                        console.log('user create: Success');
                                        res.send(result);
                                    },
                                    function (err){
                                        console.log('user create: Fail');
                                        res.send({err: 'Unable to create user details. The employee has been created'});
                                    }
                                )
                        }

                    }
                );

         }
         else
         {
             var err = 'No req.body on employee POST';
             console.error(new Error(err));
             res.send({'err': err});
         }


     });

    router.put('/employees/:id', function (req, res) {
       console.log('Employees PUT. Parameters: ' + JSON.stringify(req.body));

       if (req.body){
            var obj = req.body;

            //Format attributes correctly for SQL stored procedure
            obj.address.addressId = (obj.employee.addressId ? obj.employee.addressId : null);
            obj.address.line1 = (obj.address.line1 ? '"' + obj.address.line1 + '"' : null);
            obj.address.line2 = (obj.address.line2 ? '"' + obj.address.line2 + '"' : null);
            obj.address.suburbId = (obj.address.suburbId ? obj.address.suburbId : null);

            obj.employee.cfname = (obj.employee.cfname ? '"' + obj.employee.cfname + '"' : null);
            obj.employee.clname = (obj.employee.clname ? '"' + obj.employee.clname + '"' : null);
            obj.employee.cnumber = (obj.employee.cnumber ? '"' + obj.employee.cnumber + '"' : null);
            obj.employee.cemail = (obj.employee.cemail ? '"' + obj.employee.cemail + '"' : null);
            obj.employee.image = (obj.employee.image ? obj.employee.image : null);
            obj.employee.salary = (obj.employee.salary ? obj.employee.salary : null);
            obj.employee.addressId = (obj.employee.addressId ? obj.employee.addressId : null);

            obj.user.name = (obj.user.name ? '"' + obj.user.name + '"' : null);
            obj.user.password = (obj.user.password ? '"' + obj.user.password + '"' : null);
            obj.user.roleId = (obj.user.roleId ? obj.user.roleId : null);
            obj.user.employeeId = (obj.employee.employeeId ? obj.employee.employeeId : null);
            obj.user.userId = (obj.user.userId ? obj.user.userId : null);

            var deferred = q.defer();
            var promise;

            if (obj.employee.addressId){
                console.log('Updating address');
                promise = models.address.update(obj.address);
            }
            else{
                if ((obj.address.suburbId != null) || (obj.address.line2 != null) || (obj.address.line2 != null)) {
                    console.log('Creating address');
                    promise = models.address.create(obj.address);
                } else {
                    console.log('No address to create or update');
                    deferred.resolve({err: 'No Info'});
                    promise = deferred.promise;
                }
            }

            promise.then(
                    function (result){
                        if (result.err == 'No Info'){
                            obj.employee.addressId = null;
                            console.log('address create: No address information for create');
                        }
                        else {
                            obj.employee.addressId = (result.SQLstats.insertId ? result.SQLstats.insertId : obj.employee.addressId);
                            console.log('address update/create: Success');
                        }
                    },
                    function (err){
                        obj.employee.addressId = (obj.employee.addressId ? obj.employee.addressId : null);
                        console.log('address update/create: Fail');
                    }
                )
                .then(
                    function (result){
                        console.log('Updating Employee');
                        return models.employee.update(obj.employee)
                            .then(
                                function (result){
                                    console.log('employee update: Success');
                                },
                                function (err){
                                    console.log('employee update: Fail');
                                }
                            )
                    }
                )
                .then(
                    function (result) {
                        if (obj.user.employeeId == null){
                            console.log('Critical error. Cannot create/update user.');
                            res.send({err: 'Unable to create/update user details. An employee has not been specified.'});
                        }
                        else {
                            if (obj.user.userId){
                                models.user.update(obj.user)
                                    .then(
                                        function (result){
                                            console.log('user update: Success');
                                            res.send(result);
                                        },
                                        function (err){
                                            console.log('user update: Fail');
                                            res.send({err: 'Unable to update user details. The employee has been created/updated'});
                                        }
                                    )
                            }
                            else {
                                console.log('Creating User');

                                models.user.create(obj.user)
                                    .then(
                                        function (result){
                                            console.log('user create: Success');
                                            res.send(result);
                                        },
                                        function (err){
                                            console.log('user create: Fail');
                                            res.send({err: 'Unable to create user details. The employee has been created/updated'});
                                        }
                                    )
                            }
                        }

                    }
                );

         }
         else
         {
             var err = 'No req.body on employee POST';
             console.error(new Error(err));
             res.send({'err': err});
         }


     });

    router.delete('/employees/:id', function (req, res) {
       console.log('Employees DELETE. Body: ' + JSON.stringify(req.params));

       if (req.params){
            var obj = {};

            //Format attributes correctly for SQL stored procedure
            obj.employeeId = (req.params.id ? req.params.id : null);

            models.employee.remove(obj)
                .then(
                    function (result){
                        console.log('employee delete: Success');
                        res.send({result: 'Success'})
                    },
                    function (err){
                        console.log('employee delete: Fail');
                        res.error(err)
                    }
                )

         }
         else
         {
             var err = 'No req.body on employee POST';
             console.error(new Error(err));
             res.send({'err': err});
         }


     });

// Employee_Leave


    router.get('/employees/:id/leave', function (req, res) {
        console.log('Employees leave GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.employee.leave(req.params.id)
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

// Finalize invoice, make sale

    router.post('/bookings/:id/invoice', function (req, res) {
        console.log('Bookings Invoice POST.')
        console.log(' - Parameters: ' + JSON.stringify(req.params));
        console.log(' - Body: ' + JSON.stringify(req.body));

        if (req.body) {
            var obj = {};

            obj.booking = {};

            //booking
            obj.booking.bid = (req.body.booking.bid ? '"' + req.body.booking.bid + '"' : null);

            obj.booking.datetime = (req.body.booking.datetime ? '"' + req.body.booking.datetime + '"' : null);
            obj.booking.datetime = obj.booking.datetime.replace("T"," ");
            obj.booking.datetime = obj.booking.datetime.replace("Z","");

            obj.booking.duration = (req.body.booking.duration ? '"' + req.body.booking.duration + '"' : 30);
            obj.booking.completed = 1;
            obj.booking.active = (req.body.booking.active ? '"' + req.body.booking.active + '"' : 1);
            obj.booking.reference = (req.body.booking.reference ? '"' + req.body.booking.reference + '"' : null);
            obj.booking.cid = (req.body.booking.cid ? req.body.booking.cid : null);
            obj.booking.eid = (req.body.booking.eid ? req.body.booking.eid : null);
            obj.booking.iid = (req.body.booking.iid ? req.body.booking.iid : null);
            obj.booking.services = [];

            if (req.body.booking.services) {
                for (var i = 0; i < req.body.booking.services.length; i++) {
                    obj.booking.services.push({hlsid: req.body.booking.services[i].hlsid});
                };
            };

            models.booking.update(obj.booking)
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

            obj.invoice = {};
        }
        else
        {
            var err = 'No req.body on booking POST';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.post('/makesale', function (req, res) {
        console.log('Bookings Invoice POST.')
        console.log(' - Parameters: ' + JSON.stringify(req.params));
        console.log(' - Body: ' + JSON.stringify(req.body));

        if (req.body) {
            var obj = {};

            obj.invoice = {};
            res.send({done: 'fake'});
        }
        else
        {
            var err = 'No req.body on booking POST';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

// Notifications
    router.post('/sms', function (req, res) {
        console.log('sms POST.')
        console.log(' - Body: ' + JSON.stringify(req.body));

        if (req.body.message && req.body.number) {
            sms.send(req.body.number, req.body.message)
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
            var err = 'Invalid number or message. Unable to send sms.';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.post('/email', function (req, res) {
        console.log('email POST.')
        console.log(' - Body: ' + JSON.stringify(req.body));

        if (req.body.message && req.body.email) {
            // sms.send(req.body.number, req.body.message);
        }
        else
        {
            var err = 'Invalid email or message. Unable to send email.';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

// orders
    router.get('/order', function (req, res) {
        console.log('order GET. Parameters: ' + JSON.stringify(req.query))

        var sname = "";
        var dateTo = "";
        var dateFrom = "";

        sname = (req.query.sname ? req.query.sname : "");
        dateTo = (req.query.dateTo ? '"' + req.query.dateTo + '"' : "2030-01-01");
        dateFrom = (req.query.dateFrom ? '"' + req.query.dateFrom + '"' : "1990-01-01");

        models.orders.find(sname, dateTo, dateFrom)
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

    router.get('/order/:id', function (req, res) {
        console.log('order GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.orders.index(req.params.id)
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

    router.post('/order', function (req, res) {
       console.log('order POST. Parameters: ' + JSON.stringify(req.body));

       if (JSON.stringify(req.body) != '{}') {
           var obj = {};
           //post
           obj.dateTo = (req.body.dateTo ? '"' + req.body.dateTo + '"' : null);
           obj.dateFrom = (req.body.dateFrom ? '"' + req.body.dateFrom + '"' : null);
           obj.sname = (req.body.sname ? req.body.sname : null);

            models.orders.create(obj)
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
             var err = 'No req.body on order POST';
             console.error(new Error(err));
             res.send({'err': err});
         }
     });

    router.put('/order', function (req, res) {
      console.log('order PUT. Parameters: ' + JSON.stringify(req.body));

      if (JSON.stringify(req.body) != '{}') {
          var obj = {};
          //post
          obj.orderID = (req.body.orderID ? req.body.orderID : null);
          obj.dateTo = (req.body.dateTo ? '"' + req.body.dateTo + '"' : null);
          obj.dateFrom = (req.body.dateFrom ? '"' + req.body.dateFrom + '"' : null);
          obj.sname = (req.body.sname ? req.body.sname : null);

           models.orders.update(obj)
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
            var err = 'No req.body on order PUT';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.delete('/order/:id', function (req, res) {
            console.log('order DELETE. Parameters: ' + JSON.stringify(req.body));

            if (JSON.stringify(req.params) != '{}') {
                var obj = {};
                //client
                obj.orderID = req.params.id;

                 models.orders.remove(obj)
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
                var err = 'No req.params on order DELETE';
                console.error(new Error(err));
                res.send({'err': err});
            }
    });

// order Line
    router.get('/orderLine/:id', function (req, res) {
        console.log('Order_Line GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.orders.readLine(req.params)
              .then(
                  function (result){
                      if (result.err)
			  {
				console.log(result.err)
			  }
			  else
			  {
				res.send(result);
			  }
                  },
                  function (err){
                      console.log(err);
                      res.send(err);
                  }
              );
            });

    router.post('/orderLine', function (req, res) {
       console.log('Order_Line POST. Parameters: ' + JSON.stringify(req.body));

       if (JSON.stringify(req.body) != '{}') {
           var obj = {};
           //post
           obj.quantity = (req.body.quantity ?  req.body.quantity : 0);
           obj.stockID = (req.body.stockID ? req.body.stockID : null);
           obj.orderID = (req.body.orderID ? req.body.orderID : null);

            models.orders.addLine(obj)
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
             var err = 'No req.body on orderLine POST';
             console.error(new Error(err));
             res.send({'err': err});
         }
     });

    router.put('/orderLine', function (req, res) {
      console.log('Order_Line PUT. Parameters: ' + JSON.stringify(req.body));

      if (JSON.stringify(req.body) != '{}') {
          var obj = {};
          //post
          obj.orderLineID = (req.body.orderLineID ? req.body.orderLineID : null);
          obj.quantity = (req.body.quantity ?  req.body.quantity : 0);
          obj.stockID = (req.body.stockID ? req.body.stockID  : null);
          obj.orderID = (req.body.orderID ? req.body.orderID : null);

           models.orders.updateLine(obj)
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
            var err = 'No req.body on orderLine PUT';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

// Services

    router.get('/services', function (req, res) {
        console.log('Services GET. Query: ' + JSON.stringify(req.query))

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

    router.get('/services/:id', function (req, res) {
        console.log('Services GET. Parameters: ' + JSON.stringify(req.params))

        models.services.index(req.params.id)
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

    router.get('/services/:id/duration', function (req, res) {
        console.log('Services GET. Parameters: ' + JSON.stringify(req.params))

        var len = 1

        if (req.query.len) {
            switch(req.query.len) {
                case 's':
                    len = 3;
                    break;
                case 'm':
                    len = 2;
                    break;
                default:
                    len = 1
            }
        }

        models.services.duration(req.params.id, len)
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

    router.post('/services', function (req, res) {
       console.log('Services POST. Parameters: ' + JSON.stringify(req.body));
       var err = '';

       if (req.body){
            var obj = {};
            obj.duration = {};

            //Format attributes correctly for SQL stored procedure
            if (req.body.name !== undefined){
                obj.name = '"' + req.body.name + '"';
            }
            else {
                err = 'No name for service';
                console.error(new Error(err));
                res.send({'err': err});
            }

            obj.info = (req.body.info ? '"' + req.body.info + '"' : null);

            if (req.body.price !== undefined){
                obj.price = req.body.price;
            }
            else {
                err = 'No price for service';
                console.error(new Error(err));
                res.send({'err': err});
            }

            if (req.body.duration.long !== undefined){
                obj.duration.long = req.body.duration.long;
            }
            else {
                err = 'No long hair duration specified';
                console.error(new Error(err));
                res.send({'err': err});
            }

            obj.duration.medium = (req.body.duration.medium ? req.body.duration.medium : req.body.duration.long);
            obj.duration.short = (req.body.duration.short ? req.body.duration.short : req.body.duration.long);

            // Call model post function
            models.services.create(obj)
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
             var err = 'No req.body on service POST';
             console.error(new Error(err));
             res.send({'err': err});
         }


     });

    router.put('/services/:id', function (req, res) {
       console.log('Services PUT. Parameters: ' + JSON.stringify(req.params) + ', Body: ' + JSON.stringify(req.body));
       var err = '';

       if (req.body){
            var obj = {};
            obj.duration = {};

            //Format attributes correctly for SQL stored procedure
            if (req.body.serviceId !== undefined){
                obj.serviceId = req.body.serviceId;
            }
            else {
                obj.serviceId = req.params.serviceId;
            }

            if (req.body.name !== undefined){
                obj.name = '"' + req.body.name + '"';
            }
            else {
                err = 'No name for service';
                console.error(new Error(err));
                res.send({'err': err});
            }

            obj.info = (req.body.info ? '"' + req.body.info + '"' : null);

            if (req.body.price !== undefined){
                obj.price = req.body.price;
            }
            else {
                err = 'No price for service';
                console.error(new Error(err));
                res.send({'err': err});
            }

            if (req.body.duration.long !== undefined){
                obj.duration.long = req.body.duration.long;
            }
            else {
                err = 'No long hair duration specified';
                console.error(new Error(err));
                res.send({'err': err});
            }

            obj.duration.medium = (req.body.duration.medium ? req.body.duration.medium : req.body.duration.long);
            obj.duration.short = (req.body.duration.short ? req.body.duration.short : req.body.duration.long);

            // Call model post function
            models.services.update(obj)
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
             var err = 'No req.body on service PUT';
             console.error(new Error(err));
             res.send({'err': err});
         }


     });

    router.delete('/services/:id', function (req, res) {
       console.log('Services DELETE. Parameters: ' + JSON.stringify(req.params) + ', Body: ' + JSON.stringify(req.body));
       var err = '';

       if (req.body){
            var obj = {};

            //Format attributes correctly for SQL stored procedure

            obj.serviceId = req.params.id;

            // Call model post function
            models.services.remove(obj)
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
             var err = 'No req.body on service PUT';
             console.error(new Error(err));
             res.send({'err': err});
         }


     });

    router.get('/historicservices', function (req, res) {
        console.log('Historic services GET. Parameters: ' + JSON.stringify(req.query))

        var sname = (req.query.service ? req.query.service : "");

        models.historicservices.find(sname)
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

    router.get('/hairlengthservices', function (req, res) {
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

// Specials

// stock

    router.get('/stock', function (req, res) {
        console.log('Stock GET. Parameters: ' + JSON.stringify(req.query))

        var sname = "";
        var bname = "";
        var pname = "";

        sname = (req.query.sname ? req.query.sname : "");
        pname = (req.query.pname ? req.query.pname : "");
        bname = (req.query.bname ? req.query.bname : "");

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

    router.get('/stock/:id', function (req, res) {
        console.log('Stock GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.stock.index(req.params.id)
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

    router.post('/stock', function (req, res) {
       console.log('Stock POST. Parameters: ' + JSON.stringify(req.body));

       if (JSON.stringify(req.body) != '{}') {
           var obj = {};
           //post
           obj.brandName = (req.body.brandName ? '"' + req.body.brandName + '"' : null);
           obj.productName = (req.body.productName ? '"' + req.body.productName + '"' : null);
           obj.price = (req.body.price ? req.body.price : null);
           obj._size = (req.body._size ? req.body._size : null);
           obj.quantity = (req.body.quantity ? req.body.quantity : null);
           obj.barcode = (req.body.barcode ?  '"' + req.body.barcode + '"' : null);
           obj.supplierID = (req.body.supplierID ? req.body.supplierID : null);

            models.stock.create(obj)
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
             var err = 'No req.body on stock POST';
             console.error(new Error(err));
             res.send({'err': err});
         }
     });

    router.put('/stock', function (req, res) {
      console.log('Stock PUT. Parameters: ' + JSON.stringify(req.body));

      if (JSON.stringify(req.body) != '{}') {
          var obj = {};
          //post
          obj.stockID = (req.body.stockID ? req.body.stockID : null);
          obj.brandName = (req.body.brandName ? '"' + req.body.brandName + '"' : null);
          obj.productName = (req.body.productName ? '"' + req.body.productName + '"' : null);
          obj.price = (req.body.price ? req.body.price : null);
          obj._size = (req.body._size ? req.body._size : null);
          obj.quantity = (req.body.quantity ? req.body.quantity : null);
          obj.barcode = (req.body.barcode ?  '"' + req.body.barcode + '"' : null);
          obj.supplierID = (req.body.supplierID ? req.body.supplierID : null);

           models.stock.update(obj)
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
            var err = 'No req.body on stock PUT';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.delete('/stock/:id', function (req, res) {
            console.log('Stock DELETE. Parameters: ' + JSON.stringify(req.body));

            if (JSON.stringify(req.params) != '{}') {
                var obj = {};
                //client
                obj.stockID = req.params.id;

                 models.stock.remove(obj)
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
                var err = 'No req.params on stock DELETE';
                console.error(new Error(err));
                res.send({'err': err});
            }
    });

// sp_Insert_Stock_History

    router.get('/stockHistory/:id', function (req, res) {
        console.log('Stock_History GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.stock.index(req.params.id)
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

// suppliers
    router.get('/supplier', function (req, res) {
        console.log('supplier GET. Parameters: ' + JSON.stringify(req.query))

       var sname = "";

        sname = (req.query.sname ? req.query.sname : "");

        models.supplier.find(sname)
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

    router.get('/supplier/:id', function (req, res) {
        console.log('supplier GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.supplier.index(req.params.id)
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

    router.post('/supplier', function (req, res) {
       console.log('Supplier POST. Parameters: ' + JSON.stringify(req.body));

       if (JSON.stringify(req.body) != '{}') {
           var obj = {};
           //post
           obj.name = (req.body.name ? '"' + req.body.name + '"' : null);
           obj.contact = (req.body.contactNumber ? '"' + req.body.contactNumber + '"' : null);
           obj.email = (req.body.contactEmail ? '"' + req.body.contactEmail + '"' : null);
           obj.active = 1;

            models.supplier.create(obj)
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
             var err = 'No req.body on supplier POST';
             console.error(new Error(err));
             res.send({'err': err});
         }
     });

    router.put('/supplier/:id', function (req, res) {
      console.log('supplier PUT. Parameters: ' + JSON.stringify(req.body));

      if (JSON.stringify(req.body) != '{}') {
          var obj = {};
          //post
          obj.supplierid = (req.body.supplierid ? req.body.supplierid : req.params.id);
          obj.name = (req.body.name ? '"' + req.body.name + '"' : null);
          obj.contactNumber = (req.body.contactNumber ? '"' + req.body.contactNumber + '"' : null);
          obj.contactEmail = (req.body.contactEmail ? '"' + req.body.contactEmail + '"' : null);

           models.supplier.update(obj)
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
            var err = 'No req.body on supplier PUT';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.delete('/supplier/:id', function (req, res) {
            console.log('supplier DELETE. Parameters: ' + JSON.stringify(req.body));

            if (JSON.stringify(req.params) != '{}') {
                var obj = {};
                //client
                obj.supplierID = req.params.id;

                 models.supplier.remove(obj)
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
                var err = 'No req.params on supplier DELETE';
                console.error(new Error(err));
                res.send({'err': err});
            }
    });

// Users

    router.get('/users/:id', function (req, res) {
        console.log('user GET w/ ID. Parameters: ' + JSON.stringify(req.params))

        models.user.index(req.params.id)
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

    router.put('/users/:id/password', function (req, res) {
        console.log('user GET w/ ID. Body: ' + JSON.stringify(req.body))

        var obj = req.body;

        obj.password = '"' + obj.password + '"';

        models.user.changePassword(obj)
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
    });

// Vouchers

    router.get('/vouchers/:id', function (req, res) {
        console.log('Vouchers GET. Parameters: ' + JSON.stringify(req.params))

        var obj = {};

        obj.vid = (req.params.id ? req.params.id : null);

        models.vouchers.find(obj)
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

    router.post('/vouchers', function (req, res) {
        console.log('Vouchers POST. Body: ' + JSON.stringify(req.body))

        var obj = {};

        obj.amount = (req.body.amount ? req.body.amount : 0);
        obj.barcode = (req.body.barcode ? req.body.barcode : null);

        models.vouchers.create(obj)
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

    router.put('/vouchers/:id', function (req, res) {
        console.log('Vouchers PUT. Body: ' + JSON.stringify(req.body))

        var obj = {};

        obj.vid = (req.body.vid ? req.body.vid : null);
        obj.amount = (req.body.amount ? req.body.amount : 0);

        models.vouchers.update(obj)
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

    router.get('/lookups/roles', function (req, res) {
        console.log('get roles');
        console.log('request body: ' + JSON.stringify(req.params));

        models.lookup.roles()
            .then(
                function (result){
                    console.log(result);
                    res.send(result);
                }
            );
    });

// Reports
    router.get('/reports/audit', function (req, res) {
        console.log(req);
        console.log('Audit GET. Parameters: ' + JSON.stringify(req.query))

        var obj = {};

        obj.action = (req.query.action ? '"%' + req.query.action + '%"': '"%"');
        obj.uname = (req.query.uname ? '"%' + req.query.uname + '%"': '"%"');
        obj.date = (req.query.date ? moment(req.query.date).format("YYYY-MM-DD") : moment());

        models.reports.audit(obj)
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
};
