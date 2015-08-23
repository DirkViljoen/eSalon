var myModule = angular.module('app', ['smart-table']);

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

    $scope.home = '/client/manage/cl';

    // Lookup tables
    $scope.getNotificationMethods = function() {
        $scope.loading = true;
        $http.get('/lookups/notificationMethods').then(function(response) {
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
        $http.get('/lookups/provinces').then(function(response) {
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
            $http.get('/lookups/cities/' + $scope.client.provinceId).then(function(response) {
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
            $http.get('/lookups/suburbs/' + $scope.client.cityId).then(function(response) {
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
        $http.get('/client/' + id).then(function(response) {
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
        $http.get('/client/' + id + '/address').then(function(response) {
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
        $scope.loading = true;
        $http.get('/client').then(function(response) {
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
        $http.get('/client/' + id + '/services').then(function(response) {
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
        $http.get('/client/' + id + '/products').then(function(response) {
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
                        $http.post('/client/', $scope.client)
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
                        $http.put('/client/' + $scope.client.clientid, $scope.client)
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
                        $http.delete('/client/' + $scope.client.clientid, $scope.client)
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

        $http.get('/client' + criteria).then(function(response) {
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

    $scope.selectRow = function(id) {
        $scope.client.clientid = id;
    };

    // Helper functions

    $scope.formatDateTime = function(inDate) {
        return (inDate.slice(0,10) + ' ' + indate.slice(11,15));
    }

    // Routing
    $scope.cancel = function() {
        $window.location.href = $scope.home;
    };

    $scope.addClient = function() {
        $window.location.href = '/client/add/new';
    };

    $scope.viewClient = function() {
        if($scope.client.clientid){
            $window.location.href = '/client/view/' + $scope.client.clientid;
        }
        else {
            error_Ok('Client not selected', 'You have not selected a client to view.');
        };
    };

    $scope.updateClient = function() {
        if($scope.client.clientid){
            $window.location.href = '/client/update/' + $scope.client.clientid;
        }
        else {
            error_Ok('Client not selected', 'You have not selected a client to update.');
        };
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
