var myModule = angular.module('app', ['smart-table', 'ui.calendar', 'angularMoment', 'ui.bootstrap', 'flow', 'chart.js']);

//Angular app
myModule.controller('SubLetterController', function($scope, $http, $window) {
    $scope.loading = true;
    $scope.error = '';

    $scope.subLetter = {};
    $scope.subLetters = [];
    $scope.subLetterPayments = [];
    $scope.paymentMethods = [];
    $scope.payment = {};
    $scope.searchCriteria = {};

    $scope.home = '/sub-letters/manage/sl';

    // Lookup tables
    $scope.getPaymentMethods = function() {
        $scope.loading = true;
        $http.get('/lookups/paymentMethods').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.paymentMethods = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    // Core controller tables
    $scope.getSubLetter = function(id) {
        $scope.loading = true;
        $http.get('/sub-letters/' + id).then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.subLetter.id = response.data.rows[0].Sub_Letter_id;
                $scope.subLetter.businessName = response.data.rows[0].BusinessName;
                $scope.subLetter.contactFName = response.data.rows[0].ContactFName;
                $scope.subLetter.contactLName = response.data.rows[0].ContactLName;
                $scope.subLetter.contactNumber = response.data.rows[0].ContactNumber;
                $scope.subLetter.contactEmail = response.data.rows[0].ContactEmail;
                $scope.subLetter.startDate = response.data.rows[0].DateTime.slice(0,10);
                $scope.subLetter.rent = response.data.rows[0].Amount;
                $scope.subLetter.active = response.data.rows[0].Active;

                $scope.payment.id = response.data.rows[0].Sub_Letter_id;
                $scope.payment.date = new Date().toJSON().slice(0,10)
                $scope.payment.amount = response.data.rows[0].Amount;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getSubLetters = function() {
        $scope.loading = true;
        $http.get('/sub-letters').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.subLetters = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getSubLetterPayments = function(id) {
        $scope.loading = true;
        $http.get('/sub-letters/' + id + '/payments').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.subLetterPayments = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    // Functionality
    $scope.postPayment = function() {
        if($("#sub-letterPayment").valid()){
            subletter_payment('capture', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.post('/sub-letters/payment', $scope.payment)
                            .then(function(response) {
                                success_Ok('Payment successfull', 'Payment captured successfully', function(res) {
                                    $window.location.href = $scope.home;
                                });
                            }, function(err) {
                                error_Ok('Capture payment error', 'The following error occured while capturing the payment: ' + err.data, function(res) {
                                    $window.location.href = $scope.home;
                                });
                                $scope.error = err.data;
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        $window.location.href = $scope.home;
                        break;
                    case 'none':
                        alert('Unknown user response from capture payment confirmation');
                        break;
                }
            })
        }
        else {
            error_Ok('Capture payment validation failed', 'Some fields have not passed validation, please correct before submitting.');
        }
    };

    $scope.postSubLetter = function() {
        if($("#sub-letterAdd").valid()){
            subletter_update('save', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.post('/sub-letters/', $scope.subLetter)
                            .then(function(response) {
                                success_Ok('Sub-letter successfully added', 'The details for ' + $scope.subLetter.businessName + ' has been saved successfully.', function(res) {
                                    $window.location.href = $scope.home;
                                });
                            }, function(err) {
                                error_Ok('Sub-letter add error', 'The following error occured while saving the new sub-letter details: ' + err.data, function(res) {
                                    $window.location.href = $scope.home;
                                });
                                $scope.error = err.data;
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        $window.location.href = $scope.home;
                        break;
                    case 'none':
                        alert('Unknown user response from add sub-letter confirmation');
                        break;
                }
            })
        }
        else {
            error_Ok('Capture payment validation failed', 'Some fields have not passed validation, please correct before submitting.');
        }
    };

    $scope.putSubLetter = function() {
        if($("#sub-letterUpdate").valid()){
            subletter_update('save', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.put('/sub-letters/' + $scope.subLetter.id, $scope.subLetter)
                            .then(function(response) {
                                success_Ok('Sub-letter updated successfully', 'The new details for ' + $scope.subLetter.businessName + ' has been saved successfully.', function(res) {
                                    $window.location.href = $scope.home;
                                });
                            }, function(err) {
                                error_Ok('Sub-letter update error', 'The following error occured while saving the new sub-letter details: ' + err.data, function(res) {
                                    $window.location.href = $scope.home;
                                });
                                $scope.error = err.data;
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        $window.location.href = $scope.home;
                        break;
                    case 'none':
                        alert('Unknown user response from update sub-letter confirmation');
                        break;
                }
            })
        }
        else {
            error_Ok('Capture payment validation failed', 'Some fields have not passed validation, please correct before submitting.');
        }
    };

    $scope.deleteSubLetter = function() {
        if($scope.subLetter.id){
            var temp = $scope.subLetter.businessName;
            subletter_delete('delete', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.delete('/sub-letters/' + $scope.subLetter.id, $scope.subLetter)
                            .then(function(response) {
                                success_Ok('Sub-letter deleted', 'The sub-letter has been deleted successfully.', function(res) {
                                    $window.location.href = $scope.home;
                                });
                            }, function(err) {
                                error_Ok('Sub-letter delete error', 'The following error occured while deleting the sub-letter: ' + err.data, function(res) {
                                    $window.location.href = $scope.home;
                                });
                                $scope.error = err.data;
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        break;
                    case 'none':
                        alert('Unknown user response from delete sub-letter confirmation');
                        break;
                }
            })
        }
        else {
            error_Ok('Delete sub letter failed', 'A sub-letter was not selected');
        }
    };

    $scope.searchSubLetter = function() {
        $scope.loading = true;
        $http.post('/sub-letters/search', $scope.searchCriteria).then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.subLetters = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.selectRow = function(id) {
        $scope.subLetter.id = id;
    };

    // Routing
    $scope.cancel = function() {
        $window.location.href = $scope.home;
    };

    $scope.viewSubLetter = function() {
        if($scope.subLetter.id){
            $window.location.href = '/sub-letters/view/' + $scope.subLetter.id;
        }
        else {
            error_Ok('Sub-letter not selected', 'You have not selected a sub-letter to view.');
        };
    }

    $scope.updateSubLetter = function() {
        if($scope.subLetter.id){
            $window.location.href = '/sub-letters/update/' + $scope.subLetter.id;
        }
        else {
            error_Ok('Sub-letter not selected', 'You have not selected a sub-letter to update.');
        };
    }

    $scope.capturePayment = function() {
        if($scope.subLetter.id){
            $window.location.href = '/sub-letters/RecievePayment/' + $scope.subLetter.id;
        }
        else {
            error_Ok('Sub-letter not selected', 'You have not selected a sub-letter to recieve a payment from.');
        };
    }

    //Initializing
    $scope.initPayment = function(subLetter) {
        $scope.getPaymentMethods();
        $scope.getSubLetter(subLetter);
    }

    $scope.initAdd = function() {

        $scope.subLetter.businessName = '';
        $scope.subLetter.contactFName = '';
        $scope.subLetter.contactLName = '';
        $scope.subLetter.contactNumber = '';
        $scope.subLetter.contactEmail = '';
        $scope.subLetter.startDate = '';
        $scope.subLetter.rent = '';
        $scope.subLetter.active = '';

        $scope.loading = false;
    }

    $scope.initView = function(subLetter) {
        $scope.getSubLetter(subLetter);
        $scope.getSubLetterPayments(subLetter);
    }

    $scope.initUpdate = function(subLetter) {
        $scope.getSubLetter(subLetter);
    }

    $scope.initManage = function(subLetter) {
        $scope.getSubLetters();
    }


  });

myModule.controller('ClientController', function($scope, $http, $window) {
    $scope.loading = true;
    $scope.error = '';

    $scope.client = {};
    $scope.clients = [];
    $scope.serviceHistory = [];
    $scope.productHistory = [];

    $scope.notificationMethods = [];
    $scope.provinces = [];
    $scope.cities = [];
    $scope.suburbs = [];
    $scope.searchCriteria = {};

    $scope.home = '/clients';

    // Lookup tables
    $scope.getNotificationMethods = function() {
        $scope.loading = true;
        $http.get('/api/lookups/notificationMethods').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.notificationMethods = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getProvinces = function() {
        $scope.loading = true;
        $http.get('/api/lookups/provinces').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.provinces = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getCities = function() {
        if ($scope.client.provinceId) {
            $scope.loading = true;
            $scope.suburbs = [];
            $http.get('/api/lookups/cities/' + $scope.client.provinceId).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.cities = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };
    };

    $scope.getSuburbs = function() {
        if ($scope.client.cityId) {
            $scope.loading = true;
            $http.get('/api/lookups/suburbs/' + $scope.client.cityId).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.suburbs = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };
    };

    // Core controller tables
    $scope.getClient = function(id) {
        $scope.loading = true;
        $http.get('/api/clients/' + id).then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.client.clientid = response.data.rows[0].Client_ID;
                $scope.client.title = response.data.rows[0].Title;
                $scope.client.contactFName = response.data.rows[0].Name;
                $scope.client.contactLName = response.data.rows[0].Surname;
                $scope.client.contactNumber = response.data.rows[0].ContactNumber;
                $scope.client.contactEmail = response.data.rows[0].email;
                $scope.client.dateOfBirth = (response.data.rows[0].DateOfBirth == null ? null : response.data.rows[0].DateOfBirth.slice(0,10));
                $scope.client.reminders = (response.data.rows[0].Reminders == 1 ? true : false);
                $scope.client.notifications = (response.data.rows[0].Notifications == 1 ? true : false);
                $scope.client.active = response.data.rows[0].Active;
                $scope.client.notificationMethod = response.data.rows[0].NoticationMethod_ID;
                $scope.client.addressid = response.data.rows[0].Address_ID;

                if (response.data.rows[0].Address_ID) {
                    $scope.getAddress($scope.client.addressid);
                };
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getAddress = function(id) {
        $scope.loading = true;
        $http.get('/api/clients/' + id + '/address').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.client.line1 = response.data.rows[0].Line1;
                $scope.client.line2 = response.data.rows[0].Line2;
                $scope.client.suburbId = response.data.rows[0].Surburb_id;
                $scope.client.cityId = response.data.rows[0].City_id;
                $scope.client.provinceId = response.data.rows[0].Province_id;

                if ($scope.client.provinceId) {
                    $scope.getCities();
                    $scope.getSuburbs();
                };
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getClients = function() {
        console.log('test');
        $scope.loading = true;
        $http.get('/api/clients').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.clients = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getServiceHistory = function(id) {
        $scope.loading = true;
        $http.get('/api/clients/' + id + '/services').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.serviceHistory = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.getProductHistory = function(id) {
        $scope.loading = true;
        $http.get('/api/clients/' + id + '/products').then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.productHistory = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };
    // Functionality

    $scope.postClient = function() {
        if($("#clientAdd").valid()){
            client_add('save', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.post('/api/clients', $scope.client)
                            .then(function(response) {
                                if (response.data.err) {
                                    error_Ok('Client add error', 'An error occured while saving the new client details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                    $scope.error = response.data.err;
                                }
                                else {
                                    success_Ok('Client successfully added', 'The details for ' + $scope.client.contactFName + ' ' + $scope.client.contactLName + ' has been saved successfully.', function(res) {
                                        $window.location.href = $scope.home;
                                    });
                                }
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        $window.location.href = $scope.home;
                        break;
                    default:
                        error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                        break;
                }
            })
        }
        else {
            error_Ok('Capture payment validation failed', 'Some fields have not passed validation, please correct before submitting.');
        }
    };

    $scope.putClient = function() {
        if($("#clientUpdate").valid()){
            client_update('update', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.put('/api/clients/' + $scope.client.clientid, $scope.client)
                            .then(function(response) {
                                if (response.data.err) {
                                    error_Ok('Client update error', 'An error occured while saving the clients new details. Please contact suport with the following information: ' + JSON.stringify(response.data.err), function() {return {}});
                                    $scope.error = response.data.err;
                                }
                                else {
                                    success_Ok('Client successfully updated', 'The details for ' + $scope.client.contactFName + ' ' + $scope.client.contactLName + ' has been updated successfully.', function(res) {
                                        $window.location.href = $scope.home;
                                    });
                                }
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        $window.location.href = $scope.home;
                        break;
                    default:
                        error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                        break;
                }
            })
        }
        else {
            error_Ok('Update client validation failed', 'Some fields have not passed validation, please correct before submitting.');
        }
    };

    $scope.deleteClient = function() {
        if($scope.client.clientid){
            client_delete('delete', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.delete('/api/clients/' + $scope.client.clientid, $scope.client)
                            .then(function(response) {
                                if (response.data.err) {
                                    error_Ok('Client delete error', 'An error occured while deleting the client. Please contact support with the following information: ' + JSON.stringify(response.data.err), function(res) {return {};});
                                    $scope.error = response.data.err;
                                }
                                else {
                                    success_Ok('Client deleted', 'The client has been deleted successfully.', function(res) {
                                        $window.location.href = $scope.home;
                                    });
                                }
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        break;
                    case 'none':
                        error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                        break;
                }
            })
        }
        else {
            error_Ok('Delete client failed', 'A client was not selected');
        }
    };

    $scope.searchClient = function() {
        $scope.loading = true;
        var criteria = '?fname=' + $scope.searchCriteria.contactFName + '&lname=' + $scope.searchCriteria.contactLName

        $http.get('/api/clients' + criteria).then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.clients = response.data.rows;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.searchClearClient = function() {
        $scope.searchCriteria.contactFName = '';
        $scope.searchCriteria.contactLName = '';
        $scope.searchClient();
    }

    $scope.selectRow = function(id) {
        $scope.client.clientid = id;
    };

    // Helper functions

    $scope.formatDateTime = function(inDate) {
        return (inDate.slice(0,10) + ' ' + indate.slice(11,15));
    }

    $scope.JSON2CSV = function(objArray) {
        var array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;

        var str = '';
        var line = '';

        if ($("#labels").is(':checked')) {
            var head = array[0];
            if ($("#quote").is(':checked')) {
                for (var index in array[0]) {
                    var value = index + "";
                    line += '"' + value.replace(/"/g, '""') + '",';
                }
            } else {
                for (var index in array[0]) {
                    line += index + ',';
                }
            }

            line = line.slice(0, -1);
            str += line + '\r\n';
        }

        for (var i = 0; i < array.length; i++) {
            var line = '';

            if ($("#quote").is(':checked')) {
                for (var index in array[i]) {
                    var value = array[i][index] + "";
                    line += '"' + value.replace(/"/g, '""') + '",';
                }
            } else {
                for (var index in array[i]) {
                    line += array[i][index] + ',';
                }
            }

            line = line.slice(0, -1);
            str += line + '\r\n';
        }
        return str;
    };

    $scope.downloadCSV = function(obj) {
        var json = $.parseJSON(obj);
        var csv = JSON2CSV(json);
        window.open("data:text/csv;charset=utf-8," + escape(csv));
    };


    // Routing
    $scope.cancel = function() {
        $window.location.href = $scope.home;
    };

    $scope.addClient = function() {
        $window.location.href = '/clients/add';
    };

    $scope.viewClient = function() {
        if($scope.client.clientid){
            $window.location.href = '/clients/view/' + $scope.client.clientid;
        }
        else {
            error_Ok('Client not selected', 'You have not selected a client to view.');
        };
    };

    $scope.updateClient = function() {
        if($scope.client.clientid){
            $window.location.href = '/clients/update/' + $scope.client.clientid;
        }
        else {
            error_Ok('Client not selected', 'You have not selected a client to update.');
        };
    };

    $scope.exportClients = function() {
        $scope.downloadCSV($scope.clients);
    };

    //Initializing
    $scope.initAdd = function() {
        $scope.getNotificationMethods();
        $scope.getProvinces();
    }

    $scope.initView = function(client) {
        $scope.getNotificationMethods();
        $scope.getProvinces();
        $scope.getClient(client);
        $scope.getServiceHistory(client);
        $scope.getProductHistory(client);
    }

    $scope.initUpdate = function(client) {
        $scope.getNotificationMethods();
        $scope.getProvinces();
        $scope.getClient(client);
    }

    $scope.initManage = function() {
        $scope.getClients();
        $scope.searchCriteria.contactFName = "";
        $scope.searchCriteria.contactLName = "";
    }

  });

myModule.controller('BookingController', function($scope, $modal, $http, $window, $compile,uiCalendarConfig, $interval) {
    $scope.alertMessage = {};
    $scope.settings = {};

    $scope.events = {};

    $scope.events.new = {};

    $scope.booking = {};
    $scope.bookings = [];
    $scope.employeeLeave = [];

    $scope.clients = [];
    $scope.employees = [];
    $scope.services = [];
    $scope.hairlengths = [];
    $scope.hairlengthservices = [];

    $scope.calendar = {};

    $scope.searchCriteria = '';
    $scope.searchResult = [];

    $scope.badclicks = 0;

    $scope.home = '/booking/'

    $scope.timertest = 0;

    var timer;

    // core controller tables

        $scope.getbooking = function(id) {
            $scope.loading = true;
            $http.get('/api/bookings/' + id).then(function(response) {
                $scope.loading = false;
                console.log('Booking details:');
                console.log(response.data);
                if (response.data.rows) {
                    $scope.booking.bid = response.data.rows[0].Booking_id;
                    $scope.booking.datetime = response.data.rows[0].DateTime;
                    $scope.booking.date = moment($scope.booking.datetime).format('dddd, MMMM Do YYYY');
                    $scope.booking.time = moment($scope.booking.datetime).format('HH:mm');

                    $scope.booking.duration = response.data.rows[0].Duration;
                    $scope.booking.completed = response.data.rows[0].Completed;
                    $scope.booking.active = response.data.rows[0].Active;
                    $scope.booking.reference = response.data.rows[0].ReferenceNumber;
                    $scope.booking.cid = response.data.rows[0].Client_id;
                    $scope.booking.cfullname = response.data.rows[0].clientTitle + ' ' + response.data.rows[0].clientFName + ' ' + response.data.rows[0].clientLName
                    $scope.booking.eid = response.data.rows[0].Employee_id;
                    $scope.booking.efullname = response.data.rows[0].employeeFName + ' ' + response.data.rows[0].employeeLName
                    $scope.booking.iid = response.data.rows[0].Invoice_ID;

                    $http.get('/api/bookings/' + id + '/services')
                        .then(
                            function(response) {
                                $scope.booking.services = response.data.rows;
                            });
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getbookings = function(id) {
            $scope.loading = true;
            $scope.eventSources = [];
            $scope.bookings = [];

            var path = (id ? '/api/employees/' + id + '/bookings': '/api/bookings');

            $http.get(path).then(function(response) {
                console.log(response.data);
                if (response.data.rows) {
                    $scope.bookings = response.data.rows;
                    $scope.colorize();
                    $scope.getbookinggservices();
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getleave = function(id) {
            $scope.loading = true;
            $scope.employeeLeave = [];

            if (id) {
                var path = '/api/employees/' + id + '/leave';

                $http.get(path).then(function(response) {
                    console.log(response.data);
                    if (response.data.rows) {
                        $scope.employeeLeave = response.data.rows;
                        $scope.colorizeleave();
                    }
                }, function(err) {
                    $scope.loading = false;
                    $scope.error = err.data;
                });
            }
        }

        $scope.getbookinggservices = function() {
            var temp = [];

            for (var i = 0; i < $scope.bookings.length; i++)
            {
                temp.push({bid: $scope.bookings[i].Booking_id, index: i})

                $http.get('/api/bookings/' + $scope.bookings[i].Booking_id + '/services')
                    .then(
                        function(response) {
                            for (var j = 0; j < temp.length; j++) {
                                if (response.data.rows.length > 0) {
                                    if (response.data.rows[0].Booking_id == temp[j].bid) {
                                        $scope.bookings[temp[j].index].services = response.data.rows;

                                    };
                                };
                            };
                        });
            }
            $scope.loading = false;

        };

    // lookup tables

        $scope.getclients = function() {
            $scope.loading = true;
            $http.get('/api/clients').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.clients = response.data.rows;
                    for (index = 0; index < $scope.clients.length; ++index) {
                        $scope.clients[index].fullname = $scope.clients[index].Title + ". " + $scope.clients[index].Name + " " + $scope.clients[index].Surname;
                    };

                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getemployees = function() {
            $scope.loading = true;
            $http.get('/api/employees').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.employees = response.data.rows;
                    for (index = 0; index < $scope.employees.length; ++index) {
                        $scope.employees[index].fullname = $scope.employees[index].Name + " " + $scope.employees[index].Surname;
                    };

                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getservices = function() {
            $scope.loading = true;
            $http.get('/api/services').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.services = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.gethairlengths = function() {
            $scope.loading = true;
            $http.get('/api/lookups/hairlength').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.hairlengths = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.gethairlengthservices = function() {
            $scope.loading = true;
            $http.get('/api/hairlengthservices').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.hairlengthservices = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

    // helper functions

        $scope.addminutes = function(start,minutes) {
            var d = new Date(start);
            d.setMinutes(d.getMinutes() + minutes);
            return d.toISOString();
        };

        $scope.colorize = function() {

            for (index = 0; index < $scope.bookings.length; ++index) {
                console.log($scope.bookings[index]);
                $scope.bookings[index].events = [];

                var color = $scope.getColor($scope.bookings[index].DateTime, $scope.bookings[index].Completed);

                $scope.bookings[index].events.push({
                    id: $scope.bookings[index].Booking_id,
                    title: $scope.bookings[index].clientFName,
                    start: $scope.bookings[index].DateTime,
                    end: $scope.addminutes($scope.bookings[index].DateTime, $scope.bookings[index].Duration),
                    allDay: false,
                    backgroundColor: color,
                    color: "#AAA",
                    textColor: "#000"
                  });
                $scope.eventSources.push($scope.bookings[index])
            };
            $scope.changeDate($scope.settings.date,'myCalendar');
            $scope.changeView($scope.settings.view, 'myCalendar');
        };

        $scope.colorizeleave = function() {

            for (index = 0; index < $scope.employeeLeave.length; ++index) {
                console.log($scope.employeeLeave[index]);
                $scope.employeeLeave[index].events = [];

                var color = '#888';

                $scope.employeeLeave[index].events.push({
                    id: $scope.employeeLeave[index].EmployeeLeave_ID,
                    title: 'Out of office',
                    start: moment($scope.employeeLeave[index].StartDate),
                    end: moment($scope.employeeLeave[index].EndDate),
                    allDay: false,
                    backgroundColor: color,
                    color: "#AAA",
                    textColor: "#FFF"
                  });
                $scope.eventSources.push($scope.employeeLeave[index]);
            };
        };

        $scope.generateReference = function() {
            $scope.booking.reference = $scope.booking.cid + moment($scope.booking.datetime).format('MMDD');
        };

        $scope.addService = function(){
            var service = {};

            $scope.booking.services.push(service);
        };

        $scope.updateService = function(index){
            if (($scope.booking.services[index].hlid) && ($scope.booking.services[index].sid)) {
                for (i = 0; i < $scope.hairlengthservices.length; ++i) {
                    if (($scope.hairlengthservices[i].Service_id == $scope.booking.services[index].sid) &&
                        ($scope.hairlengthservices[i].HairLength_id == $scope.booking.services[index].hlid)) {

                        $scope.booking.services[index].hlsid = $scope.hairlengthservices[i].HairLengthService_id;
                        $scope.booking.services[index].duration = $scope.hairlengthservices[i].Duration;
                    };
                };
            };

            $scope.booking.duration = 0;
            for (i = 0; i < $scope.booking.services.length; ++i) {
                if ($scope.booking.services[i].duration){
                    $scope.booking.duration = $scope.booking.duration + $scope.booking.services[i].duration;
                };
            }
        };

        $scope.removeService = function(index){
            $scope.booking.services.splice(index, 1);
        };

        $scope.timeFormatting = function(value){
            return (value-(value%60))/60 + ':' + (value%60)
        }

    // functionality

        $scope.searchBooking = function() {
            $scope.loading = true;
            var criteria = '?search=' + $scope.searchCriteria.booking;

            $http.get('/api/bookings' + criteria).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.searchResult = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };


        $scope.changeCalendar = function() {
            var locat = '/booking?eid=' + $scope.settings.eid;
            if ($scope.settings.view) {
                locat = locat + '&view=' + $scope.settings.view;
            }
            $window.location.href = locat;

        };

        notifyClient = function(cid, message, subject) {
            // console.log(cid);
            // console.log(message);
            for (i = 0; i < $scope.clients.length; i++) {
                if ($scope.clients[i].Client_ID == cid) {
                    // alert(JSON.stringify($scope.clients[i]));
                    if ($scope.clients[i].Notifications == 1) {
                        var obj = {};
                        obj.message = message;
                        obj.subject = subject;

                        if ($scope.clients[i].NoticationMethod_ID == 1) {
                            // sms
                            // alert('sms');
                            obj.number = $scope.clients[i].ContactNumber;
                            $http.post('/api/sms', obj)
                        }
                        else
                        {
                            // email
                            // alert('email');
                            obj.email = $scope.clients[i].email;
                            $http.post('/api/email', obj)
                        }
                    }
                }
            }
        };

        $scope.postBooking = function() {
            if($("#bookingAdd").valid()){
                if($scope.booking.services.length > 0){
                    booking_add('save', function(res) {
                        console.log(res);
                        switch (res){
                            case 'yes':
                                $http.post('/api/bookings', $scope.booking)
                                    .then(function(response) {
                                        if (response.data.err) {
                                            error_Ok('Booking add error', 'An error occured while saving the new booking details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                            $scope.error = response.data.err;
                                        }
                                        else {
                                            notifyClient($scope.booking.cid, 'Your booking has been confirmed for ' + $scope.booking.time + ' on ' + $scope.booking.date + ' at Salon Redesign. Your reference number is ' + $scope.booking.reference + '. ', 'Salon Redesign booking confirmation');
                                            success_Ok('Booking successfully added', 'The booking reference number is: ' + $scope.booking.reference, function(res) {
                                                $window.location.href = $scope.home;
                                            });
                                        }
                                    });
                                break;
                            case 'no':
                                break;
                            case 'cancel':
                                $window.location.href = $scope.home;
                                break;
                            default:
                                error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                                break;
                        }
                    })
                }
                else {
                    error_Ok('You have not selected any services.', 'Please select at least one service before saving the booking.');
                }

            }
            else {
                error_Ok('Create new booking failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        };

        $scope.putBooking = function() {
            if($("#bookingUpdate").valid()){
                if ($scope.booking.completed != 1){
                    if($scope.booking.services.length > 0){
                        booking_update('update', function(res) {
                            console.log(res);
                            switch (res){
                                case 'yes':
                                    $http.put('/api/bookings/' + $scope.booking.bid, $scope.booking)
                                        .then(function(response) {
                                            if (response.data.err) {
                                                error_Ok('Booking update error', 'An error occured while updating the booking details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                                $scope.error = response.data.err;
                                            }
                                            else {
                                                notifyClient($scope.booking.cid, 'Your booking slot has been updated for ' + $scope.booking.time + ' on ' + $scope.booking.date + ' at Salon Redesign. Your reference number is ' + $scope.booking.reference + '. ', 'Salon Redesign booking change confirmation');
                                                success_Ok('Booking successfully updated', 'The booking reference number is: ' + $scope.booking.reference, function(res) {
                                                    $window.location.href = $scope.home;
                                                });
                                            }
                                        });
                                    break;
                                case 'no':
                                    break;
                                case 'cancel':
                                    $window.location.href = $scope.home;
                                    break;
                                default:
                                    error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                                    break;
                            }
                        })
                    }
                    else {
                        error_Ok('You have not selected any services.', 'Please select at least one service before saving the booking.');
                    }
                }
                else
                {
                    error_Ok('Update booking error', 'You are not allowed to update a booking that have been finalized.');
                }
            }
            else {
                error_Ok('Updating booking failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        }

        $scope.deleteBooking = function() {
            if($scope.booking.bid){
                if ($scope.booking.completed != 1){
                    booking_delete('delete', function(res) {
                        console.log(res);
                        switch (res){
                            case 'yes':
                                $http.delete('/api/bookings/' + $scope.booking.bid, $scope.booking)
                                    .then(function(response) {
                                        if (response.data.err) {
                                            error_Ok('Booking cancel error', 'An error occured while cancelling the booking. Please contact support with the following information: ' + JSON.stringify(response.data.err), function(res) {return {};});
                                            $scope.error = response.data.err;
                                        }
                                        else {
                                            notifyClient($scope.booking.cid, 'Your booking has been CANCELLED for ' + $scope.booking.time + ' on ' + $scope.booking.date + ' at Salon Redesign. Your reference number is ' + $scope.booking.reference + '. ', 'Salon Redesign booking cancelation');
                                            success_Ok('Booking cancelled', 'The boooking has been cancelled successfully.', function(res) {
                                                $window.location.href = $scope.home;
                                            });
                                        }
                                    });
                                break;
                            case 'no':
                                break;
                            case 'cancel':
                                break;
                            case 'none':
                                error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                                break;
                        }
                    })
                }
                else
                {
                    error_Ok('Cancel booking error', 'You are not allowed to delete a booking that have been finalized.');
                }
            }
            else {
                error_Ok('Cancel booking failed', 'A booking was not selected');
            }
        }

        $scope.putBookingMovement = function() {
            // alert(JSON.stringify($scope.booking));
            $http.put('/api/bookings/' + $scope.booking.Booking_id, $scope.booking)
                .then(function(response) {
                    if (response.data.err) {
                        error_Ok('Booking update error', 'An error occured while saving the booking details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                        $scope.error = response.data.err;
                    }
                    else{
                        notifyClient($scope.booking.cid, 'Your booking slot has been moved to ' + $scope.booking.time + ' on ' + $scope.booking.date + ' at Salon Redesign. Your reference number is ' + $scope.booking.reference + '. ', 'Salon Redesign booking change confirmation');
                    };
                });
        };

        $scope.getColor = function(indate, completed) {
            var color = {};
            color.bg = "#FFFCB0";

            var date = {};

            date.d1 = new Date();
            date.d2 = new Date(indate);

            console.log(date);

            // booking missed
            if (date.d1 > date.d2){
                color.bg = "#FFB0B0";
            };

            // booking completed
            if (completed == 1){
                color.bg = "#B0FFB1";
            }

            return color.bg;
        };

        $scope.cancel = function() {
            $window.location.href = $scope.home;
        }

    // routing

        $scope.finalizeBooking = function() {
            timer = undefined;
            if ($scope.booking.completed != 1){
                $window.location.href = $scope.home + 'finalise/' + $scope.booking.bid;
            }
            else
            {
                error_Ok('Finalize booking error', 'This booking has already been finalized.');
            }
        }

        $scope.newClient = function() {
            timer = undefined;
            $window.location.href = '/clients/add';
        }

    // Timer

        $scope.updateCalendar = function() {
            $scope.currentDate = moment();
            myCalendar.fullCalendar( 'RemoveEventSource', $scope.eventSources )
            // console.log($scope.currentDate);
            $scope.colorize();
            // uiCalendarConfig.calendars.myCalendar.fullCalendar('render');
        };

        $scope.$on('$destroy', function() {
            // Make sure that the interval is destroyed too
            timer = undefined;
        });

    // initiating

        $scope.initManage = function(eid, view, date) {
            $scope.getclients();
            $scope.booking.eid = eid;
            $scope.getbookings(eid);
            $scope.getleave(eid);
            $scope.getemployees();

            $scope.settings.eid = eid;
            $scope.settings.view = view;
            $scope.settings.date = moment(date);

            // timer = $interval(function() {$scope.updateCalendar();}, 1000);
        };

        $scope.initAdd = function(date,fullname,eid) {
            $scope.getclients();
            $scope.getemployees();
            $scope.getservices();
            $scope.gethairlengths();
            $scope.gethairlengthservices();

            $scope.booking.datetime = moment(date);
            $scope.booking.date = moment(date).format('dddd, MMMM Do YYYY');
            $scope.booking.time = moment(date).format('HH:mm');
            $scope.booking.eid = eid;
            $scope.booking.efullname = fullname;
            $scope.booking.services = [];
            console.log('initiated add');
        };

        $scope.initUpdate = function(bid) {
            $scope.getclients();
            // $scope.getemployees();
            $scope.getservices();
            $scope.gethairlengths();
            $scope.gethairlengthservices();
            $scope.getbooking(bid);

            console.log('initiated update');
        };

    // calendar

        $scope.refresh = function(){
            $window.location.href = '/booking?date=' + $scope.settings.date + '&view=' + $scope.settings.view + '&eid=' + $scope.settings.eid;
        };

        $scope.calendar.dayClick = function(date, jsEvent, view) {
            $scope.settings.view = view.name;
            // $scope.events.new.date = date;
            // $scope.events.new.view = view.name;

            if (view.name == 'month') {
                console.log(uiCalendarConfig.calendars);
                // $scope.uiConfig.fullCalendar('changeView', 'agendaWeek')
                $scope.changeDate(date,'myCalendar');
                $scope.changeView('agendaDay', 'myCalendar');
            }
            else {
                if (moment(date) > moment()) {
                    var mStart = moment(date);
                    var mEnd = mStart.add(30, 'm');

                    if (bookingLeaveOverlap($scope.booking.eid, mStart, mEnd) == false) {
                        var temp = "";
                        for (index = 0; index < $scope.employees.length; ++index) {
                            // $scope.employees[index].fullname = $scope.employees[index].Name + " " + $scope.employees[index].Surname;
                            if ($scope.booking.eid == $scope.employees[index].Employee_ID) {
                                temp = $scope.employees[index].fullname;
                            }
                        };
                        $window.location.href = $scope.home + 'add?datetime=' + moment(date) +
                            '&stylist=' + temp + '&eid=' + $scope.booking.eid;
                    }
                    else
                    {
                        warning_Ok('Creating bookings during leave', 'You are not allowed to add a booking during a time allocated as employee leave.', function() {return {}});
                    }
                }
                else
                {
                    if ($scope.badclicks > 1){
                        info_Ok('Trying to add a booking?', 'Are you trying to add a booking? You cannot add a booking to a past date/time.', function() {return {}});
                    }
                    else
                    {
                        $scope.badclicks = $scope.badclicks + 1;
                    }
                }
            };
        };

        /* alert on eventClick */
        $scope.calendar.OnEventClick = function( event, jsEvent, view){
            $scope.settings.view = view.name;
            $scope.settings.date = event.start;
            $window.location.href = '/booking/update/' + event.id;
        };

        bookingLeaveOverlap = function(eid, start, end){
            var res = false;
            for (i = 0; i < $scope.employeeLeave.length; i++) {
                if ($scope.employeeLeave[i].Employee_ID == eid){
                    if (start > moment($scope.employeeLeave[i].StartDate) && start < moment($scope.employeeLeave[i].EndDate)){
                        res = true;
                    }
                    if (end > moment($scope.employeeLeave[i].StartDate) && end < moment($scope.employeeLeave[i].EndDate)){
                        res = true;
                    }
                }
            }

            return res;
        };

        /* alert on Drop */
        $scope.calendar.OnDrop = function(event, delta, revertFunc, jsEvent, ui, view){
            $scope.settings.view = view.name;
            $scope.settings.date = event.start;
            $scope.alertMessage = ('booking ' + event.id + ' was moved by ' + delta/1000/60 + ' minutes');
            for (index = 0; index < $scope.bookings.length; ++index) {
                if ($scope.bookings[index].Booking_id == event.id){
                    if ($scope.bookings[index].Completed != 1) {
                        $scope.bookings[index].DateTime = $scope.addminutes($scope.bookings[index].DateTime, delta/1000/60);
                        if (moment($scope.bookings[index].DateTime) > moment()) {
                            var mStart = moment($scope.bookings[index].DateTime);
                            var mEnd = mStart.add($scope.bookings[index].Duration, 'm');

                            if (bookingLeaveOverlap($scope.bookings[index].Employee_id, mStart, mEnd) == false) {
                                // alert($scope.bookings[index].services);

                                $scope.bookings[index].events[0].start = $scope.bookings[index].DateTime;
                                $scope.bookings[index].events[0].end = $scope.addminutes($scope.bookings[index].DateTime, $scope.bookings[index].Duration);

                                $scope.booking = {};
                                $scope.booking.bid = $scope.bookings[index].Booking_id;
                                $scope.booking.datetime = $scope.bookings[index].DateTime;
                                $scope.booking.date = moment($scope.bookings[index].DateTime).format('dddd, MMMM Do YYYY');
                                $scope.booking.time = moment($scope.bookings[index].DateTime).format('HH:mm');
                                $scope.booking.duration = $scope.bookings[index].Duration;
                                $scope.booking.completed = $scope.bookings[index].Completed;
                                $scope.booking.active = $scope.bookings[index].Active;
                                $scope.booking.reference = $scope.bookings[index].ReferenceNumber;
                                $scope.booking.eid = $scope.bookings[index].Employee_id;
                                $scope.booking.iid = $scope.bookings[index].Invoice_id;
                                $scope.booking.cid = $scope.bookings[index].Client_id
                                $scope.booking.services = ($scope.bookings[index].services ? $scope.bookings[index].services : []);

                                $scope.putBookingMovement();
                            }
                            else
                            {
                                warning_Ok('Moving bookings into time allocated as leave', 'You are not allowed to move a booking into a time allocated as employee leave.', function(res) {$scope.refresh()});
                            }
                        }
                        else
                        {
                            warning_Ok('Moving bookings into past dates', 'You are not allowed to move a booking to a past time.', function(res) {$scope.refresh()});
                        }
                    }
                    else
                    {
                        warning_Ok('Working with finalized bookings', 'Updating/Editing of bookings that have been finalised are not allowed.', function(res) {$scope.refresh()});
                    }

                }
            };
        };

        /* alert on Resize */
        $scope.calendar.OnResize = function(event, delta, revertFunc, jsEvent, ui, view ){
            $scope.settings.view = view.name;
            $scope.settings.date = event.start;
            $scope.alertMessage = ('booking ' + event.id + ' duration was changed by ' + delta/1000/60 + ' minutes');
            for (index = 0; index < $scope.bookings.length; ++index) {
                if ($scope.bookings[index].Booking_id == event.id){
                    if ($scope.bookings[index].Completed != 1) {
                        $scope.bookings[index].Duration = $scope.bookings[index].Duration + (delta/1000/60);

                        var mStart = moment($scope.bookings[index].DateTime);
                        var mEnd = mStart.add($scope.bookings[index].Duration, 'm');

                        if (bookingLeaveOverlap($scope.bookings[index].Employee_id, mStart, mEnd) == false) {
                            $scope.bookings[index].events[0].start = $scope.bookings[index].DateTime;
                            $scope.bookings[index].events[0].end = $scope.addminutes($scope.bookings[index].DateTime, $scope.bookings[index].Duration);

                            $scope.booking = {};
                            $scope.booking.bid = $scope.bookings[index].Booking_id;
                            $scope.booking.datetime = $scope.bookings[index].DateTime;
                            $scope.booking.date = moment($scope.bookings[index].DateTime).format('dddd, MMMM Do YYYY');
                            $scope.booking.time = moment($scope.bookings[index].DateTime).format('HH:mm');
                            $scope.booking.duration = $scope.bookings[index].Duration;
                            $scope.booking.completed = $scope.bookings[index].Completed;
                            $scope.booking.active = $scope.bookings[index].Active;
                            $scope.booking.reference = $scope.bookings[index].ReferenceNumber;
                            $scope.booking.eid = $scope.bookings[index].Employee_id;
                            $scope.booking.iid = $scope.bookings[index].Invoice_id;
                            $scope.booking.services = ($scope.bookings[index].services ? $scope.bookings[index].services : []);

                            $scope.putBookingMovement();

                            $scope.getbookings();
                        }
                        else {
                            error_Ok('Booking duration change error', 'A booking can not start or end within a time allocated as employee leave.', function(res) {$scope.refresh()});
                        }
                    }
                    else
                    {
                        error_Ok('Booking update error', 'Updating/Editing of bookings that have been finalised are not allowed.', function(res) {$scope.refresh()});
                    }
                }
            };
        };

        /* add custom event*/
        $scope.addEvent = function() {
          $scope.events.push({
            title: 'Open Sesame',
            start: new Date(y, m, 28),
            end: new Date(y, m, 29),
            className: ['openSesame']
          });
        };
        /* remove event */
        $scope.remove = function(index) {
          $scope.events.splice(index,1);
        };
        /* Change View */
        $scope.changeView = function(view,calendar) {
          uiCalendarConfig.calendars[calendar].fullCalendar('changeView',view);
          $scope.settings.view = view;
        };
        /* Change month/week/day shown */
        $scope.changeDate = function(date,calendar) {
          uiCalendarConfig.calendars[calendar].fullCalendar('gotoDate',date);
          $scope.settings.date = moment(date);
        };
        /* Change View */
        $scope.renderCalender = function(calendar) {
          if(uiCalendarConfig.calendars[calendar]){
            uiCalendarConfig.calendars[calendar].fullCalendar('render');
          }
        };

        /* config object */
        $scope.uiConfig = {
            calendar: {
                defaultView: $scope.settings.view,
                defaultDate: $scope.settings.date,
                minTime: '06:00:00',
                maxTime: '19:00:00',
                height: 640,
                editable: true,
                eventLimit: true, // allow "more" link when too many events
                theme: true,
                timezone: 'local',
                header:{
                  left: 'title',
                  center: 'today',
                  right: 'month agendaWeek agendaDay prev next'
                },
                eventClick: $scope.calendar.OnEventClick,
                eventDrop: $scope.calendar.OnDrop,
                eventResize: $scope.calendar.OnResize,
                eventRender: $scope.calendar.eventRender,
                dayClick: $scope.calendar.dayClick
            }
        };

        /* event sources array*/
        $scope.eventSources = [];

        $scope.items = $scope.bookings; // ['item1', 'item2', 'item3'];

          $scope.animationsEnabled = true;

          $scope.open = function (size) {

            var modalInstance = $modal.open({
              animation: $scope.animationsEnabled,
              templateUrl: 'searchBooking.html',
              controller: 'ModalInstanceCtrl',
              size: size,
              resolve: {
                items: function () {
                  return $scope.bookings;
                }
              }
            });

            modalInstance.result.then(function (selectedItem) {
              $scope.selected = selectedItem;
            }, function () {
              $log.info('Modal dismissed at: ' + new Date());
            });
          };

          $scope.toggleAnimation = function () {
            $scope.animationsEnabled = !$scope.animationsEnabled;
          };
  });

myModule.controller('EmployeeController', function($scope, $http, $window) {
    $scope.loading = true;
    $scope.error = '';

    $scope.employee = {};
    $scope.address = {};
    $scope.user = {};

    $scope.employees = [];

    $scope.provinces = [];
    $scope.cities = [];
    $scope.suburbs = [];

    $scope.searchCriteria = {};

    $scope.home = '/employee';

    // Image


    // Lookup tables

        $scope.getRoles = function() {
            $scope.loading = true;
            $http.get('/api/lookups/roles').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.roles = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getProvinces = function() {
            $scope.loading = true;
            $http.get('/api/lookups/provinces').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.provinces = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getCities = function() {
            if ($scope.address.provinceId) {
                $scope.loading = true;
                $scope.suburbs = [];
                $http.get('/api/lookups/cities/' + $scope.address.provinceId).then(function(response) {
                    $scope.loading = false;
                    console.log(response.data);
                    if (response.data.rows) {
                        $scope.cities = response.data.rows;
                    }
                }, function(err) {
                    $scope.loading = false;
                    $scope.error = err.data;
                });
            };
        };

        $scope.getSuburbs = function() {
            if ($scope.address.cityId) {
                $scope.loading = true;
                $http.get('/api/lookups/suburbs/' + $scope.address.cityId).then(function(response) {
                    $scope.loading = false;
                    console.log(response.data);
                    if (response.data.rows) {
                        $scope.suburbs = response.data.rows;
                    }
                }, function(err) {
                    $scope.loading = false;
                    $scope.error = err.data;
                });
            };
        };

    // Core controller tables
        $scope.getEmployee = function(id) {
            $scope.loading = true;
            $http.get('/api/employees/' + id).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.employee.employeeId = response.data.rows[0].Employee_ID;
                    $scope.employee.cfname = response.data.rows[0].Name;
                    $scope.employee.clname = response.data.rows[0].Surname;
                    $scope.employee.cnumber = response.data.rows[0].ContactNumber;
                    $scope.employee.cemail = response.data.rows[0].email;
                    $scope.employee.salary = response.data.rows[0].Salary
                    $scope.employee.active = response.data.rows[0].Active;
                    $scope.employee.image = response.data.rows[0].Image;
                    $scope.employee.addressId = (response.data.rows[0].Address_ID ? response.data.rows[0].Address_ID : null);
                    $scope.employee.userId = (response.data.rows[0].User_ID ? response.data.rows[0].User_ID : null);

                    if ($scope.employee.addressId){
                        $scope.getAddress($scope.employee.addressId);
                    };
                    if ($scope.employee.userId){
                        $scope.getUser($scope.employee.userId);
                    };
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getAddress = function(id) {
            $scope.loading = true;
            $http.get('/api/address/' + id).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.address.id = response.data.rows[0].Address_ID;
                    $scope.address.line1 = response.data.rows[0].Line1;
                    $scope.address.line2 = response.data.rows[0].Line2;
                    $scope.address.suburbId = response.data.rows[0].Surburb_id;
                    $scope.address.cityId = response.data.rows[0].City_id;
                    $scope.address.provinceId = response.data.rows[0].Province_id;

                    if ($scope.address.provinceId) {
                        $scope.getCities();
                        $scope.getSuburbs();
                    };
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getUser = function(id) {
            $scope.loading = true;
            $http.get('/api/users/' + id).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.user.userId = response.data.rows[0].User_ID;
                    $scope.user.roleId = response.data.rows[0].Role_ID;
                    $scope.user.employeeId = response.data.rows[0].Employee_ID;
                    $scope.user.name = response.data.rows[0].Username;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getEmployees = function() {
            console.log('test');
            $scope.loading = true;
            $http.get('/api/employees').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.employees = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

    // Functionality

        $scope.postEmployee = function() {
            // console.log('Unimplemented')
            if($("#employeeAdd").valid()){
                employee_add('save', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            var obj = {}
                            obj.employee = $scope.employee;
                            obj.user = $scope.user;
                            obj.address = $scope.address;

                            $http.post('/api/employees', obj)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Employee add error', 'An error occured while saving the new employee details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Employee successfully added', 'The details for ' + $scope.employee.cfname + ' ' + $scope.employee.clname + ' has been saved successfully.', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Add employee validation failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        };

        $scope.putEmployee = function() {
            if($("#employeeUpdate").valid()){
                employee_update('update', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            var obj = {}
                            obj.employee = $scope.employee;
                            obj.user = $scope.user;
                            obj.address = $scope.address;

                            $http.put('/api/employees/' + $scope.employee.employeeId, obj)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Employee update error', 'An error occured while saving the employee details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Employee successfully updated', 'The details for ' + $scope.employee.cfname + ' ' + $scope.employee.clname + ' has been updated successfully.', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Update employee validation failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        };

        $scope.deleteEmployee = function() {
            if($scope.employee.employeeId){
                var bookings;



                employee_delete('delete', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.delete('/api/employees/' + $scope.employee.employeeId, $scope.employee)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Employee delete error', 'An error occured while deleting the employee. Please contact support with the following information: ' + JSON.stringify(response.data.err), function(res) {return {};});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Employee deleted', 'The employee has been deleted successfully.', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            break;
                        case 'none':
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Delete employee failed', 'An employee was not selected');
            }
        };

        $scope.searchEmployee = function() {
            $scope.loading = true;
            var criteria = '';
            criteria = criteria + '?fname=' + $scope.searchCriteria.cfname;
            criteria = criteria + '&lname=' + $scope.searchCriteria.clname;
            criteria = criteria + '&role=' + $scope.searchCriteria.role;

            $http.get('/api/employees' + criteria).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.employees = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.searchClearEmployee = function() {
            $scope.searchCriteria.cfname = '';
            $scope.searchCriteria.clname = '';
            $scope.searchCriteria.role = '';
            $scope.searchEmployee();
        }

        $scope.selectRow = function(eid) {
            $scope.employee.employeeId = eid;
        };

        $scope.savefile = function(){
            $http.post('/api/upload', $scope.employee);
            // Simple GET request example:
            // $http({
            //   method: 'POST',
            //   url: '/api/upload',
            //   headers: {'Content-Type': 'multipart/form-data'},
            //   data: $scope.employee
            // }).then(function successCallback(response) {
            //     console.log('success');
            //   }, function errorCallback(response) {
            //     console.log('failure');
            //     console.error(respose);
            //   });
        }

    // Helper functions


    // Routing
        $scope.cancel = function() {
            $window.location.href = $scope.home;
        };

        $scope.addEmployee = function() {
            $window.location.href = '/employee/add';
        };

        $scope.viewEmployee = function() {
            if($scope.employee.employeeId){
                $window.location.href = '/employee/view/' + $scope.employee.employeeId;
            }
            else {
                error_Ok('Employee not selected', 'You have not selected an employee to view.');
            };
        };

        $scope.updateEmployee = function() {
            if($scope.employee.employeeId){
                $window.location.href = '/employee/update/' + $scope.employee.employeeId;
            }
            else {
                error_Ok('Employee not selected', 'You have not selected an employee to update.');
            };
        };

        $scope.back = function() {
            $window.location.href = '/booking';
        };

    //modals

        $scope.changePassword = function() {
            if ($scope.user.password == $scope.user.password2) {
                var obj = {}
                obj.userId = $scope.user.userId;
                obj.password = $scope.user.password;

                $http.put('/api/users/' + obj.userId + '/password', obj)
                    .then(function(response) {
                        if (response.data.err) {
                            error_Ok('Password change error', 'An error occured while changing the password. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                            $scope.error = response.data.err;
                        }
                        else {
                            success_Ok('Password changed successfully', 'The password for ' + $scope.employee.cfname + ' ' + $scope.employee.clname + ' has been changed successfully.', function(res) {
                                return null;
                            });

                            $scope.user.password = "";
                            $scope.user.password2 = "";
                        }
                    });
            }
            else {
                error_Ok('Passwords do not match', 'Some fields have not passed validation, please correct before submitting.');
            }
        };


    //Initializing
        $scope.initAdd = function() {
            $scope.getProvinces();
            $scope.getRoles();
        }

        $scope.initView = function(employee) {
            $scope.getProvinces();
            $scope.getRoles();
            $scope.getEmployee(employee);
        }

        $scope.initUpdate = function(employee) {
            $scope.getProvinces();
            $scope.getRoles();
            $scope.getEmployee(employee);
        }

        $scope.initManage = function() {
            $scope.searchClearEmployee();
            $scope.getRoles();
        }

      });

myModule.controller('InvoiceController', function($scope, $http, $window, $q) {
    // objects
        $scope.loading = true;
        $scope.error = '';

        $scope.invoice = {};
        $scope.invoice.stock = [];
        $scope.invoice.services = [];
        $scope.invoice.voucher = {};
        $scope.invoice.voucher.redeem = {};
        $scope.invoice.voucher.buy = {};
        $scope.booking = {};

        $scope.services = [];
        $scope.hairlengths = [];
        $scope.hairlengthservices = [];

        $scope.stock = [];

        $scope.home = '/booking'

    // core tables

        $scope.getbooking = function(id) {
            $scope.loading = true;
            $http.get('/api/bookings/' + id).then(function(response) {
                $scope.loading = false;
                console.log('Booking details:');
                console.log(response.data);
                if (response.data.rows) {
                    $scope.booking.bid = response.data.rows[0].Booking_id;
                    $scope.booking.datetime = response.data.rows[0].DateTime;
                    $scope.booking.date = moment($scope.booking.datetime).format('dddd, MMMM Do YYYY');
                    $scope.booking.time = moment($scope.booking.datetime).format('HH:mm');

                    $scope.booking.duration = response.data.rows[0].Duration;
                    $scope.booking.completed = response.data.rows[0].Completed;
                    $scope.booking.active = response.data.rows[0].Active;
                    $scope.booking.reference = response.data.rows[0].ReferenceNumber;
                    $scope.booking.cid = response.data.rows[0].Client_id;
                    $scope.booking.cfullname = response.data.rows[0].clientTitle + ' ' + response.data.rows[0].clientFName + ' ' + response.data.rows[0].clientLName
                    $scope.booking.eid = response.data.rows[0].Employee_id;
                    $scope.booking.efullname = response.data.rows[0].employeeFName + ' ' + response.data.rows[0].employeeLName
                    $scope.booking.iid = response.data.rows[0].Invoice_ID;

                    $http.get('/api/bookings/' + id + '/services')
                        .then(
                            function(response) {
                                $scope.invoice.services = response.data.rows;
                                $scope.invoice.serviceprice = 0;
                                for (i = 0; i < $scope.invoice.services.length; ++i) {
                                    if ($scope.invoice.services[i].price){
                                        $scope.invoice.serviceprice += $scope.invoice.services[i].price;
                                    };
                                };

                                $scope.invoice.total = $scope.invoice.stockprice + $scope.invoice.serviceprice;
                            });
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

    // lookups

        $scope.getservices = function() {
            $scope.loading = true;
            $http.get('/api/services').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.services = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.gethistoricservices = function() {
            $scope.loading = true;
            $http.get('/api/historicservices').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.services = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.gethairlengths = function() {
            $scope.loading = true;
            $http.get('/api/lookups/hairlength').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.hairlengths = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.gethairlengthservices = function() {
            $scope.loading = true;
            $http.get('/api/hairlengthservices').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.hairlengthservices = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getstock = function() {
            $scope.loading = true;
            $http.get('/api/stock').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.stock = response.data.rows;
                };
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.calculateVoucherContribution = function() {
            $scope.invoice.vouchertotal = 0;
            if ($scope.invoice.voucher.redeem.Amount && $scope.invoice.voucher.redeem.Barcode){
                console.log('redeem voucher')
                $scope.invoice.vouchertotal -= parseInt($scope.invoice.voucher.redeem.Amount, 10);
            };
            if ($scope.invoice.voucher.buy.Amount && $scope.invoice.voucher.buy.Barcode){
                $scope.invoice.vouchertotal += parseInt($scope.invoice.voucher.buy.Amount, 10);
                console.log('buy voucher')
            };
        };

        $scope.redeemvoucher = function() {
            if ($scope.invoice.voucher.redeem.Barcode){
                $scope.loading = true;
                $http.get('/api/vouchers/' + $scope.invoice.voucher.redeem.Barcode).then(function(response) {
                    $scope.loading = false;
                    console.log(response.data.rows);
                    if (response.data.rows) {
                        $scope.invoice.voucher.redeem = response.data.rows[0];
                    };

                    $scope.calculateVoucherContribution();

                }, function(err) {
                    $scope.loading = false;
                    $scope.error = err.data;
                });

            };

            $scope.updateTotal();
        };

        $scope.buyvoucher = function() {
            if ($scope.invoice.voucher.buy.Amount && $scope.invoice.voucher.buy.Barcode){
                $scope.calculateVoucherContribution();
            };

            $scope.updateTotal();
        };

    // functionality

        $scope.addService = function(){
            var service = {};

            $scope.invoice.services.push(service);
        };

        $scope.updateService = function(index){
            if (($scope.invoice.services[index].hlid) && ($scope.invoice.services[index].sid)) {
                for (i = 0; i < $scope.hairlengthservices.length; ++i) {
                    if (($scope.hairlengthservices[i].Service_id == $scope.invoice.services[index].sid) &&
                        ($scope.hairlengthservices[i].HairLength_id == $scope.invoice.services[index].hlid)) {

                        $scope.invoice.services[index].hlsid = $scope.hairlengthservices[i].HairLengthService_id;
                        $scope.invoice.services[index].duration = $scope.hairlengthservices[i].Duration;
                    };
                };
                for (i = 0; i < $scope.services.length; ++i) {
                    if (($scope.services[i].Service_id == $scope.invoice.services[index].sid)) {

                        $scope.invoice.services[index].price = $scope.services[i].Price;
                    };
                };
            };

            $scope.invoice.serviceprice = 0;
            for (i = 0; i < $scope.invoice.services.length; ++i) {
                if ($scope.invoice.services[i].price){
                    $scope.invoice.serviceprice += $scope.invoice.services[i].price;
                };
            }
            $scope.updateTotal();
        };

        $scope.removeService = function(index){
            $scope.invoice.services.splice(index, 1);
        };

        $scope.addStock = function(){
            var stock = {};

            $scope.invoice.stock.push(stock);
        };

        $scope.updateStock = function(index){
            if (($scope.invoice.stock[index].barcode)) {
                for (i = 0; i < $scope.stock.length; ++i) {
                    if ($scope.stock[i].Barcode == $scope.invoice.stock[index].barcode) {

                        $scope.invoice.stock[index].sid = $scope.stock[i].Stock_id;
                        $scope.invoice.stock[index].bname = $scope.stock[i].BrandName;
                        $scope.invoice.stock[index].pname = $scope.stock[i].ProductName;
                        $scope.invoice.stock[index].price = $scope.stock[i].Price;
                        if ($scope.invoice.stock[index].quantity){
                            $scope.invoice.stock[index].quantity = $scope.invoice.stock[index].quantity;
                        }
                        else{
                            $scope.invoice.stock[index].quantity = 1
                        }
                        console.log($scope.stock[i].Quantity - $scope.invoice.stock[index].quantity);
                        if (($scope.stock[i].Quantity - $scope.invoice.stock[index].quantity) < 0){
                            alert('Not enough stock left. Please do a stock reconcilliation if this is not the case.')
                        }
                    };
                };
            };

            $scope.invoice.stockprice = 0;
            for (i = 0; i < $scope.invoice.stock.length; ++i) {
                if ($scope.invoice.stock[i].price){
                    $scope.invoice.stockprice += ($scope.invoice.stock[i].price * $scope.invoice.stock[i].quantity);
                };
            }
            $scope.updateTotal();
        };

        $scope.removeStock = function(index){
            $scope.invoice.stock.splice(index, 1);
        };

        $scope.updateTotal = function(){
            $scope.invoice.total = $scope.invoice.stockprice + $scope.invoice.serviceprice + $scope.invoice.vouchertotal;
            console.log($scope.invoice.total);
        };

    // Database

        $scope.postInvoice = function(){
            var obj = {}
            obj.invoice = $scope.invoice;
            obj.booking = $scope.booking;

            if($("#finaliseBooking").valid()){
                invoice_add('save', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.post('/api/bookings/' + $scope.booking.bid + '/invoice', obj)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Finalise invoice error', 'An error occured while saving the new invoice details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Invoice successfully captured', 'The invoice has successfully been captured', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Create new invoice failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        };

        $scope.postSale = function(){
            var obj = {}
            obj.invoice = $scope.invoice;

            if($("#makeSale").valid()){
                sale_add('save', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.post('/api/makesale', obj)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Finalise invoice error', 'An error occured while saving the new invoice details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Invoice successfully captured', 'The invoice has successfully been captured', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Create new invoice failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        };

    // initiating

        $scope.finalizeInvoice = function(bid) {
            console.log(bid);
            $scope.invoice.vouchers = 0;
            $scope.invoice.stockprice = 0;
            $scope.invoice.serviceprice = 0;
            $scope.invoice.total = 0;

            $scope.voucher = {};
            $scope.invoice.voucher.buy.Barcode = "";
            $scope.invoice.voucher.buy.Amount = 0;

            $scope.invoice.voucher.redeem.Barcode = "";
            $scope.invoice.voucher.redeem.Amount = 0;

            $scope.invoice.serviceprice = 0
            $scope.invoice.stockprice = 0;
            $scope.invoice.vouchertotal = 0;

            $scope.getbooking(bid);
            $scope.getservices()
            $scope.gethairlengths();
            $scope.gethairlengthservices();
            $scope.getstock();

        };

        $scope.makesale = function(bid) {
            console.log(bid);
            $scope.invoice.vouchers = 0;
            $scope.invoice.stockprice = 0;
            $scope.invoice.serviceprice = 0;
            $scope.invoice.total = 0;

            $scope.voucher = {};
            $scope.invoice.voucher.buy.Barcode = "";
            $scope.invoice.voucher.buy.Amount = 0;

            $scope.invoice.voucher.redeem.Barcode = "";
            $scope.invoice.voucher.redeem.Amount = 0;

            $scope.invoice.stockprice = 0;
            $scope.invoice.vouchertotal = 0;

            $scope.getstock();
        };
  });

myModule.controller('ServiceController', function($scope, $http, $window, $q) {
    // objects
        $scope.loading = true;
        $scope.error = '';

        $scope.service = {};
        $scope.service.duration = {};
        $scope.services = {};

        $scope.searchCriteria = {};

        $scope.home = '/service'

    // core tables

        $scope.getService = function(id){
            $http.get('/api/services/' + id).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.service.serviceId = id;
                    $scope.service.name = response.data.rows[0].Name;
                    $scope.service.info = response.data.rows[0].AdditionalInformation;
                    $scope.service.price = response.data.rows[0].Price;

                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });

            $http.get('/api/services/' + id + '/duration?len=l').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.service.duration.long = response.data.rows[0].Duration;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });

            $http.get('/api/services/' + id + '/duration?len=m').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.service.duration.medium = response.data.rows[0].Duration;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });

            $http.get('/api/services/' + id + '/duration?len=s').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.service.duration.short = response.data.rows[0].Duration;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
         };

        $scope.getServices = function(){
            $http.get('/api/services').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.services = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
         };

    // Routing

        $scope.addService = function() {
            $window.location.href = $scope.home + '/add';
         };

        $scope.viewService= function() {
            if ($scope.service.serviceId) {
                $window.location.href = $scope.home + '/view/' + $scope.service.serviceId;
            }
            else
            {
                error_Ok('Service not selected', 'You have not selected a Service to view.');
            }
         };

        $scope.updateService= function() {
            if ($scope.service.serviceId) {
                $window.location.href = $scope.home + '/update/' + $scope.service.serviceId;
            }
            else
            {
                error_Ok('Service not selected', 'You have not selected a Service to update.');
            }
         };

        $scope.cancel = function() {
            $window.location.href = $scope.home;
         }

    // functionality

        $scope.searchServices = function() {
            $scope.loading = true;
            var criteria = '?service=' + $scope.searchCriteria.name;

            $http.get('/api/services' + criteria).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.services = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
         };

        $scope.searchClearServices = function() {
            $scope.searchCriteria.name = '';
            $scope.searchServices();
         };

        $scope.selectRow = function(id) {
            console.log(id);
            $scope.service.serviceId = id;
         };

    // Database

        $scope.postService = function() {
            if($("#serviceAdd").valid()){
                service_add('save', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.post('/api/services/', $scope.service)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Add service error', 'An error occured while saving the new service details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Service successfully added', 'The service has successfully been captured', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Add new supplier failed', 'Some fields have not passed validation, please correct before submitting.');
            }
         };

        $scope.putService = function() {
            if($("#serviceUpdate").valid()){
                service_update('update', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.put('/api/services/' + $scope.service.serviceId, $scope.service)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Update service error', 'An error occured while updating the services details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Service successfully updated', 'The service has successfully been updated', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Update supplier failed', 'Some fields have not passed validation, please correct before submitting.');
            }
         };

        $scope.deleteService = function() {
            if($scope.service.serviceId){
                service_delete('delete', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.delete('/api/services/' + $scope.service.serviceId, $scope.service)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Delete service error', 'An error occured while deleting the service. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Service successfully deleted', 'The service has successfully been deleted', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Delete supplier failed', 'Some fields have not passed validation, please correct before submitting.');
            }
         };

    // initiating

        $scope.initManage = function() {
            $scope.searchCriteria.name = "";
            $scope.getServices();
         };

        $scope.initUpdate = function(id) {
            $scope.getService(id);
         };

        $scope.initView = function(id) {
            $scope.getService(id);
         };

  });

myModule.controller('SupplierController', function($scope, $http, $window, $q) {
    // objects
        $scope.loading = true;
        $scope.error = '';

        $scope.supplier = {};
        $scope.suppliers = {};

        $scope.searchCriteria = {};

        $scope.home = '/supplier'

    // core tables

        $scope.getSuppliers = function(){
            $http.get('/api/supplier').then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.suppliers = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

    // lookups


    // Routing

        $scope.addSupplier = function() {
            $window.location.href = $scope.home + '/add';
        };

        $scope.viewSupplier = function() {
            if ($scope.supplier.supplierid) {
                $window.location.href = $scope.home + '/view/' + $scope.supplier.supplierid;
            }
            else
            {
                error_Ok('Supplier not selected', 'You have not selected a Supplier to view.');
            }
        };

        $scope.updateSupplier = function() {
            if ($scope.supplier.supplierid) {
                $window.location.href = $scope.home + '/update/' + $scope.supplier.supplierid;
            }
            else
            {
                error_Ok('Supplier not selected', 'You have not selected a Supplier to view.');
            }
        };

        $scope.cancel = function() {
            $window.location.href = $scope.home;
        }

    // functionality

        $scope.searchSupplier = function() {
            $scope.loading = true;
            var criteria = '?sname=' + $scope.searchCriteria.sname

            $http.get('/api/supplier' + criteria).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.suppliers = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.searchClearSupplier = function() {
            $scope.searchCriteria.Name = '';
        };

        $scope.selectRow = function(id) {
            console.log(id);
            $scope.supplier.supplierid = id;
        };

    // Database

        $scope.postSupplier = function() {
            if($("#supplierAdd").valid()){
                supplier_add('save', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.post('/api/supplier/', $scope.supplier)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Add supplier error', 'An error occured while saving the new supplier details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Supplier successfully added', 'The supplier has successfully been captured', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Add new supplier failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        }

        $scope.putSupplier = function() {
            if($("#supplierUpdate").valid()){
                supplier_update('update', function(res) {
                    console.log(res);
                    switch (res){
                        case 'yes':
                            $http.put('/api/supplier/' + $scope.supplier.supplierid, $scope.supplier)
                                .then(function(response) {
                                    if (response.data.err) {
                                        error_Ok('Update supplier error', 'An error occured while updating the supplier details. Please contact suport with the following details: ' + JSON.stringify(response.data.err), function() {return {}});
                                        $scope.error = response.data.err;
                                    }
                                    else {
                                        success_Ok('Supplier successfully updated', 'The supplier has successfully been updated', function(res) {
                                            $window.location.href = $scope.home;
                                        });
                                    }
                                });
                            break;
                        case 'no':
                            break;
                        case 'cancel':
                            $window.location.href = $scope.home;
                            break;
                        default:
                            error_Ok('Response error', 'Sorry, we missed that. Please only use a provided button.');
                            break;
                    }
                })
            }
            else {
                error_Ok('Update supplier failed', 'Some fields have not passed validation, please correct before submitting.');
            }
        }

    // initiating

        $scope.initManage = function() {
            $scope.getSuppliers();
        };

        $scope.initAdd = function() {
            $scope.supplier = {};
        };

        $scope.initUpdate = function(id) {
            $http.get('/api/supplier/' + id).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.supplier.supplierid = response.data.rows[0].Supplier_ID;
                    $scope.supplier.name = response.data.rows[0].Name;
                    $scope.supplier.contactNumber = response.data.rows[0].ContactNumber;
                    $scope.supplier.contactEmail = response.data.rows[0].Email;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.initView = function(id) {
            $http.get('/api/supplier/' + id).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.supplier.supplierid = response.data.rows[0].Supplier_ID;
                    $scope.supplier.name = response.data.rows[0].Name;
                    $scope.supplier.contactNumber = response.data.rows[0].ContactNumber;
                    $scope.supplier.contactEmail = response.data.rows[0].Email;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };
  });

myModule.controller('ReportController', function($scope, $http, $window, $q) {
    // objects
        $scope.loading = true;
        $scope.error = '';

        $scope.audit = [];
        $scope.expense = [];
        $scope.expenseCategories = [];
        $scope.stocklevel = [];
        $scope.date;

        $scope.searchCriteria = {};


    // Test report data

        $scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
        $scope.series = ['Series A', 'Series B'];
        $scope.data = [
            [65, 59, 80, 81, 56, 55, 40],
            [28, 48, 40, 19, 86, 27, 90]
        ];

        $scope.onClick = function (points, evt) {
            console.log(points, evt);
        };

    // core tables

        $scope.getAudit = function(){
            $scope.loading = true;
            var criteria = '?action=' + $scope.searchCriteria.action + '&name=' + $scope.searchCriteria.uname + '&date=' + $scope.searchCriteria.date

            $http.get('/api/reports/audit' + criteria).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.audit = response.data.rows;
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

        $scope.getStocklevel = function(){
            $scope.loading = true;

            $http.get('/api/reports/stocklevel')
                .then(function(response) {
                    $scope.loading = false;
                    console.log(response.data);
                    if (response.data.rows) {
                        $scope.stocklevel = response.data.rows;
                        console.log('1st');
                    }
                }, function(err) {
                    $scope.loading = false;
                    $scope.error = err.data;
                })
                .then(function() {
                    formatStockLevelChartData();
                });
        };

        $scope.getExpense = function(){
            $scope.loading = true;
            var criteria = '?dateFrom=' + $scope.searchCriteria.dateFrom +
                          '&dateTo=' + $scope.searchCriteria.dateTo

            $http.get('/api/reports/expense' + criteria).then(function(response) {
                $scope.loading = false;
                console.log(response.data);
                if (response.data.rows) {
                    $scope.expense = response.data.rows;

                    var found = false;

                    for (exp = 0; exp < $scope.expense.length; exp++){
                        if ($scope.expense[exp].Category == null){
                          $scope.expense[exp].Category = "No Category"
                        };

                        found = false
                        for (cat = 0; cat < $scope.expenseCategories.length; cat++){
                            if ($scope.expense[exp].Category == $scope.expenseCategories[cat].Category){
                                found = true;
                            }
                        }
                        if (found == false){
                            $scope.expenseCategories.push({"Category" : $scope.expense[exp].Category});
                        }
                    }
                    for (cat = 0; cat < $scope.expenseCategories.length; cat++){
                        if ($scope.expenseCategories[cat].Category == null){
                            $scope.expenseCategories[cat].Category = "No Category";
                        }
                    }
                }
            }, function(err) {
                $scope.loading = false;
                $scope.error = err.data;
            });
        };

    // lookups

    // Routing

    // functionality

        $scope.searchClearAudit = function() {
            $scope.searchCriteria.action = '';
            $scope.searchCriteria.uname = '';
            $scope.searchCriteria.date = '';
            $scope.getAudit();
        };

        $scope.searchClearExpense = function() {
            $scope.searchCriteria.dateFrom = '';
            $scope.searchCriteria.dateTo = '';
            $scope.getExpense();
        };

        formatStockLevelChartData = function() {
            console.log('2nd');
            console.log($scope.stocklevel);
            $scope.labels = [];
            $scope.series = ['Products'];
            $scope.data = [[]];

            for (i = 0; i < $scope.stocklevel.length; i++){
                $scope.labels.push($scope.stocklevel[i].ProductName);

                $scope.data[0].push($scope.stocklevel[i].Quantity);
            };
        };

    // Database

    // initiating

        $scope.initAudit = function() {
            $scope.searchCriteria.action = '';
            $scope.searchCriteria.uname = '';
            $scope.searchCriteria.date = '';
            $scope.getAudit();
        };

        $scope.initStocklevel = function() {
            $scope.getStocklevel();
            $scope.date = moment().format("YYYY-MM-DD");
        };

        $scope.initExpense = function() {
          $scope.searchCriteria.dateFrom = '';
          $scope.searchCriteria.dateTo = '';
          $scope.getExpense();
        };
  });

myModule.controller('FixingController', function($scope, $http, $window, $q) {
    // objects
        $scope.loading = true;
        $scope.error = '';

        $scope.employeeManage = function() {
            $window.location.href = '/employee/';
        };

        $scope.employeeCancel = function() {
            $window.location.href = '/employee/';
        };

        $scope.routehome = function() {
            $window.location.href = '/booking/';
        };

  });

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.

myModule.controller('ModalInstanceCtrl', function ($scope, $modalInstance, items) {

  $scope.items = items;
  $scope.selected = {
    item: $scope.items[0]
  };

  $scope.ok = function () {
    $modalInstance.close($scope.selected.item);
  };

  $scope.cancel = function () {
    $modalInstance.dismiss('cancel');
  };
});

myModule.directive("fileread", [function () {
    return {
        scope: {
            fileread: "="
        },
        link: function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                var reader = new FileReader();
                reader.onload = function (loadEvent) {
                    scope.$apply(function () {
                        scope.fileread = loadEvent.target.result;
                    });
                }
                reader.readAsDataURL(changeEvent.target.files[0]);
            });
        }
    }
}]);
