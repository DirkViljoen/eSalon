'use strict';

var
    AddressModel    = require('../../models/address'),
    BookingModel    = require('../../models/booking'),
    ClientModel     = require('../../models/client'),
    EmployeeModel   = require('../../models/employee'),
    ExpenseModel    = require('../../models/expenses'),
    InvoiceModel    = require('../../models/invoice'),
    OrdersModel     = require('../../models/orders'),
    ServiceModel    = require('../../models/service'),
    StockModel      = require('../../models/stock'),
    SupplierModel   = require('../../models/supplier'),
    UserModel       = require('../../models/user'),
    VouchersModel   = require('../../models/vouchers'),
    LookupsModel    = require('../../models/lookups'),
    ReportsModel    = require('../../models/reports');

var
    moment          = require('moment'),
    q               = require('q'),
    sms             = require('../../libs/sms'),
    Flow            = require('flow'),
    multer          = require('multer'),
    upload          = multer({ dest: './uploads/'}),
    fs              = require('fs'),
    parse           = require('csv-parse'),
    nodemailer      = require('nodemailer');

module.exports = function (router) {

    var models = {};

    models.address  = new AddressModel();
    models.booking  = new BookingModel();
    models.client   = new ClientModel();
    models.employee = new EmployeeModel();
    models.expenses = new ExpenseModel();
    models.invoice  = new InvoiceModel();
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
            obj.datetime = '"' + moment(req.body.datetime).format('YYYY-MM-DD HH:mm:ss') + '"';
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

        var myError = {};
        myError.code = null;
        myError.message = '';

        if (req.body) {

            //booking
            var booking = {};
            booking.bid = (req.body.booking.bid ? req.body.booking.bid : req.params.id);
            booking.datetime = '"' + moment(req.body.datetime).format('YYYY-MM-DD HH:mm:ss') + '"';

            booking.duration = (req.body.booking.duration ? '"' + req.body.booking.duration + '"' : 60);
            booking.completed = 1;
            booking.active = (req.body.booking.active ? '"' + req.body.booking.active + '"' : 1);
            booking.reference = (req.body.booking.reference ? '"' + req.body.booking.reference + '"' : null);
            booking.cid = (req.body.booking.cid ? req.body.booking.cid : null);
            booking.eid = (req.body.booking.eid ? req.body.booking.eid : null);
            booking.iid = (req.body.booking.iid ? req.body.booking.iid : null);

            booking.services = [];
            console.log(req.body.invoice.services);
            if (req.body.invoice.services) {
                var serv = {};
                for (var i = 0; i < req.body.invoice.services.length; i++) {
                    serv = {};
                    serv.hlsid = req.body.invoice.services[i].hlsid;
                    serv.bid = booking.bid;
                    serv.spid = (req.body.invoice.services[i].spid ? req.body.invoice.services[i].spid : null);
                    serv.price = req.body.invoice.services[i].price;
                    serv.quantity = 1;
                    serv.iid = null;
                    booking.services.push(serv);
                };
            };

            booking.stock = [];
            console.log(req.body.invoice.stock);
            if (req.body.invoice.stock) {
                var stk = {};
                for (var i = 0; i < req.body.invoice.stock.length; i++) {
                    stk = {};
                    stk.sid = req.body.invoice.stock[i].sid;
                    stk.spid = (req.body.invoice.stock[i].spid ? req.body.invoice.stock[i].spid : null);
                    stk.price = req.body.invoice.stock[i].price;
                    stk.quantity = req.body.invoice.stock[i].quantity;;
                    stk.iid = null;
                    booking.stock.push(stk);
                };
            };

            console.log(booking.services);

            models.booking.update(booking)
                .then(
                    function (result){
                        if (req.body.invoice) {
                            var invoice = {};

                            console.log('test0');
                            invoice.id = null;
                            invoice.datetime = '"' + moment().format('YYYY-MM-DD HH:mm:ss') + '"';
                            invoice.discount = (req.body.invoice.discount ? req.body.invoice.discount : 0);
                            invoice.percentage = (req.body.invoice.percentage ? req.body.invoice.percentage : false);
                            invoice.total = req.body.invoice.total;
                            invoice.paymentMethod = (req.body.invoice.paymentMethod ? req.body.invoice.paymentMethod : 1);
                            invoice.cid = (req.body.booking.cid ? req.body.booking.cid : null);
                            invoice.eid = (req.body.booking.eid ? req.body.booking.eid : null);
                            invoice.bid = booking.bid;

                            console.log('test1');

                            invoice.services = booking.services;
                            invoice.stock = booking.stock;
                            console.log('test2');
                            var servHistMap = [];
                            var productHistMap = [];

                            models.invoice.create(invoice)
                                .then(
                                    function (result){
                                        console.log(result);
                                        console.log('creating invoice services with invoice id:' + result.SQLstats.insertId);
                                        invoice.iid = result.SQLstats.insertId;
                                        models.invoice.getServiceHistory()
                                            .then(
                                                function (result){
                                                    servHistMap = result.rows;
                                                    for (var s = 0; s < invoice.services.length; s++){
                                                        for (var m = 0; m < servHistMap.length; m++){
                                                            if (invoice.services[s].hlsid == servHistMap[m].hlsid){
                                                                invoice.services[s].shid = servHistMap[m].shid;
                                                                invoice.services[s].iid = invoice.iid;
                                                            }
                                                        }
                                                    }
                                                    console.log(invoice.services);

                                                    console.log('1');
                                                    for (var inv = 0; inv < invoice.services.length; inv++){
                                                        models.invoice.serviceCreate(invoice.services[inv])
                                                        .then(
                                                            function(result){
                                                                console.log('loop:' + inv);
                                                                if (invoice.services.length == inv){
                                                                    console.log('2');
                                                                    return ({'done':'done'});
                                                                }
                                                            },
                                                            function(err){
                                                                return ({'err':'Could not create services'});
                                                            }
                                                        );
                                                    }

                                                },
                                                function (err){
                                                    console.error(err);
                                                }
                                            )
                                    },
                                    function (err){
                                        res.send({'err': "Could not create invoice"});
                                    }

                                )
                                .then(
                                    function (result){
                                        console.log(result);
                                        console.log('creating invoice products with invoice id:' + invoice.iid);
                                        models.invoice.getProductHistory()
                                            .then(
                                                function (result){
                                                    productHistMap = result.rows;
                                                    for (var s = 0; s < invoice.stock.length; s++){
                                                        for (var m = 0; m < productHistMap.length; m++){
                                                            if (invoice.stock[s].sid == productHistMap[m].sid){
                                                                invoice.stock[s].shid = productHistMap[m].shid;
                                                                invoice.stock[s].iid = invoice.iid;
                                                            }
                                                        }
                                                    }
                                                    console.log(invoice.stock);

                                                    console.log('1');
                                                    for (var inv = 0; inv < invoice.stock.length; inv++){
                                                        models.invoice.stockCreate(invoice.stock[inv])
                                                        .then(
                                                            function(result){
                                                                console.log('loop:' + inv);
                                                                if (invoice.services.length == inv){
                                                                    console.log('2');
                                                                    return ({'done':'done'});
                                                                }
                                                            },
                                                            function(err){
                                                                return ({'err':'Could not create services'});
                                                            }
                                                        );
                                                    }

                                                },
                                                function (err){
                                                    console.error(err);
                                                }
                                            )
                                    },
                                    function (err){
                                        res.send({'err': "Could not create invoice"});
                                    }

                                )
                                .then(
                                    function (result){
                                        res.send(result);
                                    },
                                    function (err){
                                        res.send({'err': "An error occured during the invoice create process."});
                                    }
                                );

                        }
                        else
                        {
                            var err = 'No req.body.invoice on invoice POST';
                            console.error(new Error(err));
                            res.send({'err': err});
                        }
                    },
                    function (err){
                        console.error(err);
                        myError.err = err;
                        myError.message = 'Unable to update booking';
                        myError.code = 1;
                        res.send({'err': err});
                    }
                );
        }
        else
        {
            var err = 'No req.body on invoice POST';
            console.error(new Error(err));
            res.send({'err': err});
        }
    });

    router.post('/makesale', function (req, res) {
        console.log('Bookings MakeSale POST.')
        console.log(' - Parameters: ' + JSON.stringify(req.params));
        console.log(' - Body: ' + JSON.stringify(req.body));

        var myError = {};
        myError.code = null;
        myError.message = '';

        if (req.body) {

            //booking
            var booking = {};

            booking.stock = [];
            console.log(req.body.invoice.stock);
            if (req.body.invoice.stock) {
                var stk = {};
                for (var i = 0; i < req.body.invoice.stock.length; i++) {
                    stk = {};
                    stk.sid = req.body.invoice.stock[i].sid;
                    stk.spid = (req.body.invoice.stock[i].spid ? req.body.invoice.stock[i].spid : null);
                    stk.price = req.body.invoice.stock[i].price;
                    stk.quantity = req.body.invoice.stock[i].quantity;;
                    stk.iid = null;
                    booking.stock.push(stk);
                };
            };


            if (req.body.invoice) {
                var invoice = {};

                console.log('test0');
                invoice.id = null;
                invoice.datetime = '"' + moment().format('YYYY-MM-DD HH:mm:ss') + '"';
                invoice.discount = (req.body.invoice.discount ? req.body.invoice.discount : 0);
                invoice.percentage = (req.body.invoice.percentage ? req.body.invoice.percentage : false);
                invoice.total = req.body.invoice.total;
                invoice.paymentMethod = (req.body.invoice.paymentMethod ? req.body.invoice.paymentMethod : 1);
                invoice.cid = null;
                invoice.eid = null;
                invoice.bid = null;

                console.log('test1');

                invoice.stock = booking.stock;
                console.log('test2');
                var productHistMap = [];

                models.invoice.create(invoice)
                    .then(
                        function (result){
                            console.log(result);
                            invoice.iid = result.SQLstats.insertId;
                            console.log('creating invoice products with invoice id:' + invoice.iid);
                            models.invoice.getProductHistory()
                                .then(
                                    function (result){
                                        console.log('test3');
                                        productHistMap = result.rows;
                                        console.log(invoice.stock);
                                        console.log(productHistMap);
                                        for (var s = 0; s < invoice.stock.length; s++){
                                            for (var m = 0; m < productHistMap.length; m++){
                                                if (invoice.stock[s].sid == productHistMap[m].sid){
                                                    invoice.stock[s].shid = productHistMap[m].shid;
                                                    invoice.stock[s].iid = invoice.iid;
                                                }
                                            }
                                        }
                                        console.log(invoice.stock);

                                        console.log('1');
                                        for (var inv = 0; inv < invoice.stock.length; inv++){
                                            models.invoice.stockCreate(invoice.stock[inv])
                                            .then(
                                                function(result){
                                                    console.log('loop:' + inv);
                                                    if (invoice.services.length == inv){
                                                        console.log('2');
                                                        return ({'done':'done'});
                                                    }
                                                },
                                                function(err){
                                                    return ({'err':'Could not create services'});
                                                }
                                            );
                                        }

                                    },
                                    function (err){
                                        console.error(err);
                                    }
                                )
                        },
                        function (err){
                            res.send({'err': "Could not create invoice"});
                        }

                    )
                    .then(
                        function (result){
                            res.send(result);
                        },
                        function (err){
                            res.send({'err': "An error occured during the invoice create process."});
                        }
                    );
            };
        }
        else
        {
            var err = 'No req.body on makesale POST';
            console.error(new Error(err));
            res.send({'err': err});
        };
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
            var transporter = nodemailer.createTransport({
                service: 'Gmail',
                auth: {
                    user: 'dirkcharl.viljoen@gmail.com', // Your email id
                    pass: 'gmLu@xeiri1' // Your password
                }
            });

            var mailOptions = {
                from: 'dirkcharl.viljoen@gmail.com', // sender address
                to: req.body.email, // list of receivers
                subject: (req.body.subject ? req.body.subject : 'Notification from Salon Redesign'), // Subject line
                text: req.body.message //, // plaintext body
                // html: '<b>Hello world ✔</b>' // You can choose to send an HTML body instead
            };

            transporter.sendMail(mailOptions, function(error, info){
                if(error){
                    console.log(error);
                    res.json({yo: 'error'});
                }else{
                    console.log('Message sent: ' + info.response);
                    res.json({yo: info.response});
                };
            });
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

        var sname = 0;
        var dateTo = "";
        var dateFrom = "";

        sname = (req.query.sid ? req.query.sid : 1);
        dateTo = (req.query.dateTo ? req.query.dateTo : "2030-01-01");
        dateFrom = (req.query.dateFrom ? req.query.dateFrom : "1990-01-01");

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
           obj.datePlaced = (req.body.DatePlaced ? '"' + req.body.DatePlaced + '"' : null);
           obj.dateReceived = '"2020-01-01"';
           obj.supplierID = (req.body.Supplier_ID ? req.body.Supplier_ID : null);
           obj.stock = req.body.Stock;
           console.log(obj.stock);

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
          obj.orderLID = (req.body.orderLID ? req.body.orderLID : null);
          obj.dateTo = (req.body.dateTo ? '"' + req.body.dateTo + '"' : null);
          obj.quantity = (req.body.quantity ? req.body.quantity : null);

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

    router.get('/reports/stocklevel', function (req, res) {
        console.log('Stocklevel GET. Parameters: ' + JSON.stringify(req.query));

        models.reports.stocklevel()
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
// Uploading files

    router.post('/uploadImage',function(req,res){
        console.log('uploading - file:');
        console.log('body: ' + JSON.stringify(req.body));
        upload(req,res,function(err) {
            if(err) {
                return res.send("Error uploading file.");
            }
            res.send("File is uploaded");
        });
    });

    router.post('/uploadCSV',function(req,res){
        console.log('uploading - file:');
        console.log('body: ' + JSON.stringify(req.files));


        fs.readFile(req.files.displayImage.path, function (err, data) {

            var newPath = process.cwd() + "/uploads/" + req.files.displayImage.name;
            fs.writeFile(newPath, data, function (err) {
                console.log('Reading CSV file');

                function validate(data) {
                    console.log('validating file');
                    var e = {};
                    e.err = false;
                    e.message = "";

                    var tmpErr = false;
                    var k;
                    var start = 0;
                    if (data[0][0] == "Category"){
                        start = 1;
                    };

                    for (k = start; k < data.length; k++){
                        if (tmpErr == false){
                            if (data[k][0].length > 50){
                                tmpErr = true;
                                e.message = e.message + "Some of the Categories in the 1st column are longer than 50 characters, only 50 characters can be stored. Anything more will be cut off. ";
                                e.err = true;
                            }
                        }
                    }

                    tmpErr = false;

                    for (k = start; k < data.length; k++){
                        if (tmpErr == false){
                            if (data[k][1].length > 50){
                                tmpErr = true;
                                e.message = e.message + "Some of the Expense names in the 2nd column are longer than 50 characters, only 50 characters can be stored. Anything more will be cut off. ";
                                e.err = true;
                            }
                        }
                    }

                    tmpErr = false;

                    for (k = start; k < data.length; k++){
                        if (tmpErr == false){
                            console.log(data[k][2]);
                            if (data[k][2].length != 10){
                                tmpErr = true;
                                e.message = e.message + "Some of the expense dates in the 3rd column are not in the format ‘yyyy/mm/dd’ please correct this before completing the import process. ";
                                e.err = true;
                            }
                        }
                    }

                    console.log(e.message);
                    return e;

                };

                function writeLine(dataline) {
                    console.log('writing clean line to db');
                    var expense = {};
                    expense.category = null;
                    expense.name = '"' + dataline[1] + '"';
                    expense.quantity = dataline[3];
                    expense.date = '"' + moment(new Date(dataline[2])).format("YYYY-MM-DD") + '"';
                    expense.price = dataline[4];
                    expense.paymentMethod = null;

                    models.expenses.create(expense);
                };

                var parser = parse({delimiter: ';'}, function(err, data){
                    console.log('we are reading everyting!!!');

                    var e = validate(data);
                    if (e.err){
                        res.send({err: e.message});
                    }
                    else{
                        for (var k = 1; k < data.length; k++){
                            writeLine(data[k]);
                            if (k+1==data.length){
                                res.redirect('/booking');
                            }
                        }
                    }
                });

                if (req.files.displayImage.name){
                    fs.createReadStream(process.cwd() + '/uploads/' + req.files.displayImage.name).pipe(parser);
                }
                else
                {
                    res.redirect('/booking');;
                }
            });
        });


    });

// Reading files

    router.get('/readCSV/:name', function (req, res){
        console.log('Reading CSV file');

        function validate(data) {
            console.log('validating file');
            var e = {};
            e.err = false;
            e.message = "";

            var tmpErr = false;
            var k;
            var start = 0;
            if (data[0][0] == "Category"){
                start = 1;
            };

            for (k = start; k < data.length; k++){
                if (tmpErr == false){
                    if (data[k][0].length > 50){
                        tmpErr = true;
                        e.message = e.message + "Some of the Categories in the 1st column are longer than 50 characters, only 50 characters can be stored. Anything more will be cut off. ";
                        e.err = true;
                    }
                }
            }

            tmpErr = false;

            for (k = start; k < data.length; k++){
                if (tmpErr == false){
                    if (data[k][1].length > 50){
                        tmpErr = true;
                        e.message = e.message + "Some of the Expense names in the 2nd column are longer than 50 characters, only 50 characters can be stored. Anything more will be cut off. ";
                        e.err = true;
                    }
                }
            }

            tmpErr = false;

            for (k = start; k < data.length; k++){
                if (tmpErr == false){
                    console.log(data[k][2]);
                    if (data[k][2].length != 10){
                        tmpErr = true;
                        e.message = e.message + "Some of the expense dates in the 3rd column are not in the format ‘yyyy/mm/dd’ please correct this before completing the import process. ";
                        e.err = true;
                    }
                }
            }

            console.log(e.message);
            return e;

        };

        function writeLine(dataline) {
            console.log('writing clean line to db');
            var expense = {};
            expense.category = null;
            expense.name = '"' + dataline[1] + '"';
            expense.quantity = dataline[3];
            expense.date = '"' + moment(new Date(dataline[2])).format("YYYY-MM-DD") + '"';
            expense.price = dataline[4];
            expense.paymentMethod = null;

            models.expenses.create(expense);
        };

        var parser = parse({delimiter: ';'}, function(err, data){
            console.log('we are reading everyting!!!');

            var e = validate(data);
            if (e.err){
                res.send({err: e.message});
            }
            else{
                for (var k = 1; k < data.length; k++){
                    writeLine(data[k]);
                    if (k+1==data.length){
                        res.send("done");
                    }
                }
            }
        });

        if (req.params.name){
            fs.createReadStream(process.cwd() + '/uploads/' + req.params.name + '.csv').pipe(parser);
        }
        else
        {
            res.send({err: 'No file to read'});
        }
    });
};
