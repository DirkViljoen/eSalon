function addNGValidation(myForm) {
    $(myForm).validate({
        highlight: function(element) {
            $(element).closest('.form-group').addClass('has-error');
            $(element).closest('.form-group').removeClass('has-success');
        },
        unhighlight: function(element) {
            $(element).closest('.form-group').removeClass('has-error');
            $(element).closest('.form-group').addClass('has-success');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function(error, element) {
            if(element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        },
        rules: {
            amount: {
                myCurrency: true,
                range: [0, 10000]
            },
            contactEmail: {
                myEmail: true
            },
            contactNumber: {
                myContactNumber: true
            },
            paymentMethod: {
                required: true,
                valueNotEquals: ''
            },
            currency_sm_pos: {
                myCurrency: true,
                range: [0, 1000]
            },
            currency_md_pos: {
                myCurrency: true,
                range: [0, 100000]
            },
            currency_lg_pos: {
                myCurrency: true,
                range: [0, 10000000]
            },
            currency_sm: {
                myCurrency: true,
                range: [-1000, 1000]
            },
            currency_md: {
                myCurrency: true,
                range: [-100000, 100000]
            },
            currency_lg: {
                myCurrency: true,
                range: [-10000000, 10000000]
            },
            number_sm: {
                range: [-1000, 1000]
            },
            number_md: {
                range: [-100000, 100000]
            },
            number_lg: {
                range: [-10000000, 10000000]
            },
            number_sm_pos: {
                range: [0, 1000]
            },
            number_md_pos: {
                range: [0, 100000]
            },
            number_lg_pos: {
                range: [0, 10000000]
            }


        }
    });
};

// Sub-letter
    function subletter_manage(form, usrResponse) {
        switch(usrResponse){
            case 'Search':
                form.submit();
                break;
            case 'View':
                form.submit();
                break;
            case 'Update':
                form.submit();
                break;
            case 'Delete':
                warning_YesNo('Delete sub-letter', 'Are you sure you want to delete the sub-letter?', function(res) {
                    switch(res) {
                        case 'Yes':
                            form.submit();
                            break;
                        case 'Cancel':
                            window.location='/sub-letters';
                            break;
                    };
                });
                break;
            case 'Capture payment':
                form.submit();
                break;
            default:
                alert('unknown submit action: ' + usrResponse)
                break;
        };
    };

    function subletter_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Create sub-letter', 'Are you sure you want to add the new sub-letter?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function subletter_update(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Update sub-letter', 'Are you sure you want to update the sub-letters details?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function subletter_delete(usrResponse, callback) {
        switch(usrResponse){
            case 'delete':
                warning_YesNo('Delete sub-letter', 'Are you sure you want to delete the sub-letter?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function subletter_payment(usrResponse, callback) {
        switch(usrResponse){
            case 'capture':
                confirm_YesNoCancel('Capture sub-letter payment', 'Are you sure you want to capture the sub-letters payment?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// Booking
    function booking_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Add booking', 'Are you sure you want to add the new booking?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function booking_update(usrResponse, callback) {
        switch(usrResponse){
            case 'update':
                confirm_YesNoCancel('Update booking', 'Are you sure you want to update the booking?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function booking_delete(usrResponse, callback) {
        switch(usrResponse){
            case 'delete':
                warning_YesNo('Cancel booking', 'Are you sure you want to cancel the selected booking?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// Invoice
    function invoice_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Capture Invoice', 'Are you sure you want to capture the invoice for the selected booking?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function sale_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Capture sale', 'Are you sure you want to capture the sale?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// Client

    function client_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Add client', 'Are you sure you want to add the new client?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function client_update(usrResponse, callback) {
        switch(usrResponse){
            case 'update':
                confirm_YesNoCancel('Update client', 'Are you sure you want to update the client?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function client_delete(usrResponse, callback) {
        switch(usrResponse){
            case 'delete':
                warning_YesNo('Delete client', 'Are you sure you want to delete the selected client?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// Employees

    function employee_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Add employee', 'Are you sure you want to add the new employee?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function employee_update(usrResponse, callback) {
        switch(usrResponse){
            case 'update':
                confirm_YesNoCancel('Update employee', 'Are you sure you want to update the employee?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function employee_delete(usrResponse, callback) {
        switch(usrResponse){
            case 'delete':
                warning_YesNo('Delete employee', 'Are you sure you want to delete the selected employee?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// Roles

    function role_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Add role', 'Are you sure you want to save the permissions as a new role?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function role_update(usrResponse, callback) {
        switch(usrResponse){
            case 'update':
                confirm_YesNoCancel('Update role', 'Are you sure you want to update the role permissions?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function role_update_newName(usrResponse, callback) {
        switch(usrResponse){
            case 'update':
                confirm_YesNoCancel('Update role', 'You have provided a new role name. Are you sure you want to save the role permissions with the new name?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// Services

    function service_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Add service', 'Are you sure you want to add the new service?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function service_update(usrResponse, callback) {
        switch(usrResponse){
            case 'update':
                confirm_YesNoCancel('Update service', 'Are you sure you want to update the service?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function service_delete(usrResponse, callback) {
        switch(usrResponse){
            case 'delete':
                warning_YesNo('Delete service', 'Are you sure you want to delete the selected service? This will only remove the service from dropdown menus', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// Supplier
    function supplier_add(usrResponse, callback) {
        switch(usrResponse){
            case 'save':
                confirm_YesNoCancel('Add supplier', 'Are you sure you want to add the new supplier?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function supplier_update(usrResponse, callback) {
        switch(usrResponse){
            case 'update':
                confirm_YesNoCancel('Update supplier', 'Are you sure you want to update the supplier?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

    function supplier_delete(usrResponse, callback) {
        switch(usrResponse){
            case 'delete':
                warning_YesNo('Delete supplier', 'Are you sure you want to delete the selected supplier?', function(res) {
                    callback(res);
                });
                break;
            default:
                callback('none');
                break;
        };
    };

// messages

    function success_Ok(myTitle, msg, callback) {
        var res = 'no value';

        var dialog = new BootstrapDialog({
            title: myTitle,
            message: msg,
            type: BootstrapDialog.TYPE_INFO,
            buttons: [{
                id: 'btn-Ok',
                label: 'Ok'
            }]
        });
        dialog.realize();

        var btn1 = dialog.getButton('btn-Ok');
        btn1.click({'result': 'ok'}, function(event){
            callback(event.data.result);
            dialog.close();
        });

        dialog.open();
    };

    function confirm_YesNoCancel(myTitle, msg, callback) {
        var res = 'no value';

        var dialog = new BootstrapDialog({
            title: myTitle,
            message: msg,
            type: BootstrapDialog.TYPE_PRIMARY,
            buttons: [{
                id: 'btn-Yes',
                label: 'Yes'
            },
            {
                id: 'btn-No',
                label: 'No'
            },
            {
                id: 'btn-Cancel',
                label: 'Cancel'
            }]
        });
        dialog.realize();

        var btn1 = dialog.getButton('btn-Yes');
        btn1.click({'result': 'yes'}, function(event){
            callback(event.data.result);
        });
        var btn2 = dialog.getButton('btn-No');
        btn2.click({'result': 'no'}, function(event){
            callback(event.data.result);
            dialog.close();
        });
        var btn3 = dialog.getButton('btn-Cancel');
        btn3.click({'result': 'cancel'}, function(event){
            callback(event.data.result);
        });

        dialog.open();
    };

    function confirm_YesNo(myTitle, msg, callback) {
        var res = 'no value';

        var dialog = new BootstrapDialog({
            title: myTitle,
            message: msg,
            type: BootstrapDialog.TYPE_PRIMARY,
            buttons: [{
                id: 'btn-Yes',
                label: 'Yes'
            },
            {
                id: 'btn-No',
                label: 'No'
            }]
        });
        dialog.realize();

        var btn1 = dialog.getButton('btn-Yes');
        btn1.click({'result': 'Yes'}, function(event){
            callback(event.data.result);
        });
        var btn2 = dialog.getButton('btn-No');
        btn2.click({'result': 'No'}, function(event){
            callback(event.data.result);
        });

        dialog.open();
    }

    function warning_YesNo(myTitle, msg, callback) {
        var res = 'no value';

        var dialog = new BootstrapDialog({
            title: myTitle,
            message: msg,
            type: BootstrapDialog.TYPE_WARNING,
            buttons: [{
                id: 'btn-Yes',
                label: 'Yes'
            },
            {
                id: 'btn-No',
                label: 'No'
            }]
        });
        dialog.realize();

        var btn1 = dialog.getButton('btn-Yes');
        btn1.click({'result': 'yes'}, function(event){
            callback(event.data.result);
            dialog.close();
        });
        var btn2 = dialog.getButton('btn-No');
        btn2.click({'result': 'no'}, function(event){
            callback(event.data.result);
            dialog.close();
        });

        dialog.open();
    }

    function error_Ok(myTitle, msg, callback) {
        var res = 'no value';

        var dialog = new BootstrapDialog({
            title: myTitle,
            message: msg,
            type: BootstrapDialog.TYPE_DANGER,
            buttons: [{
                id: 'btn-Ok',
                label: 'Ok'
            }]
        });
        dialog.realize();

        var btn1 = dialog.getButton('btn-Ok');
        btn1.click({'result': 'Ok'}, function(event){
            if (callback) {
                callback(event.data.result);
            };
            dialog.close();
        });

        dialog.open();
    }

    function warning_Ok(myTitle, msg, callback) {
        var res = 'no value';

        var dialog = new BootstrapDialog({
            title: myTitle,
            message: msg,
            type: BootstrapDialog.TYPE_WARNING,
            buttons: [{
                id: 'btn-Ok',
                label: 'Ok'
            }]
        });
        dialog.realize();

        var btn1 = dialog.getButton('btn-Ok');
        btn1.click({'result': 'Ok'}, function(event){
            if (callback) {
                // alert('Call callback');
                callback(event.data.result);
            };
            dialog.close();
        });

        dialog.open();
    }

    function info_Ok(myTitle, msg, callback) {
        var res = 'no value';

        var dialog = new BootstrapDialog({
            title: myTitle,
            message: msg,
            type: BootstrapDialog.TYPE_INFO,
            buttons: [{
                id: 'btn-Ok',
                label: 'Ok'
            }]
        });
        dialog.realize();

        var btn1 = dialog.getButton('btn-Ok');
        btn1.click({'result': 'Ok'}, function(event){
            if (callback) {
                callback(event.data.result);
            };
            dialog.close();
        });

        dialog.open();
    }

// Rules
    jQuery.validator.addMethod('myCurrency', function(value, element) {
        return this.optional(element) || /^-{0,1}(?=\(.*\)|[^()]*$)\(?\d{1,3}(,?\d{3})?(\.\d{2}?)?\)?$/.test(value);
    }, 'Please enter a valid currency value. e.g 12,345.67');

    jQuery.validator.addMethod('myEmail', function(value, element) {
        return this.optional(element) || /^[A-Za-z0-9._%+-]{1,}@[A-Za-z0-9.-]{1,}\.[A-Za-z]{2,4}$/.test(value);
    }, 'Please enter a valid email address');

    jQuery.validator.addMethod('myContactNumber', function(value, element) {
        return this.optional(element) || /^[0-9]{3}\s{1}[0-9]{3}\s{1}[0-9]{4}$/.test(value);
    }, 'Please enter a valid South African contact number in the folowing format: 099 999 9999');

    jQuery.validator.addMethod('myRadioGroup', function(value, element) {
        return this.optional(element) || /^[0-9]{3}\s{1}[0-9]{3}\s{1}[0-9]{4}$/.test(value);
    }, 'Please enter a valid South African contact number');

    jQuery.validator.addMethod("valueNotEquals", function(value, element, arg){
        if (arg === undefined) {
            arg = '';
        };

        return arg != value;
    }, "Please select an item.");
