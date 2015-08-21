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

myModule.controller('PaymentController', function($scope, $http, $window) {
    $scope.loading = true;
    $scope.error = '';
    $scope.subLetter = {};
    $scope.paymentMethods = [];
    $scope.payment = {};

    $scope.getPayment = function() {
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

    $scope.getSubLetter = function(id) {
        $scope.loading = true;
        $http.get('/sub-letters/get/' + id).then(function(response) {
            $scope.loading = false;
            console.log(response.data);
            if (response.data.rows) {
                $scope.subLetter = response.data.rows;
                $scope.subLetter.businessName = response.data.rows[0].BusinessName;
                $scope.payment.id = response.data.rows[0].Sub_Letter_id;
                $scope.payment.date = new Date().toJSON().slice(0,10)
                $scope.payment.amount = response.data.rows[0].Amount;
            }
        }, function(err) {
            $scope.loading = false;
            $scope.error = err.data;
        });
    };

    $scope.postPayment = function() {
        if($("#sub-letterPayment").valid()){
            subletter_payment('capture', function(res) {
                console.log(res);
                switch (res){
                    case 'yes':
                        $http.post('/sub-letters/payment', $scope.payment)
                            .then(function(response) {
                                success_Ok('Payment successfull', 'Payment captured successfully', function(res) {
                                    $window.location.href = '/sub-letters';
                                });
                            }, function(err) {
                                error_Ok('Capture payment error', 'The following error occured while capturing the payment: ' + err.data, function(res) {
                                    $window.location.href = '/sub-letters';
                                });
                                $scope.error = err.data;
                            });
                        break;
                    case 'no':
                        break;
                    case 'cancel':
                        $window.location.href = '/sub-letters';
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

    $scope.cancelPayment = function() {
        $window.location.href = '/sub-letters';
    };

    $scope.init = function(subLetter) {
        $scope.getPayment();
        $scope.getSubLetter(subLetter);
    }
  });
