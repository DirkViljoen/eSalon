function addFormValidation(myForm, formHandler) {
    $(myForm).validate({
        submitHandler: function(form){

            var usrResponse = $('#btnClicked').val();
            switch(formHandler) {
                case 'subletter-manage':
                    subletter_manage(form, usrResponse);
                    break;
                case 'subletter-add':
                    subletter_add(form, usrResponse);
                    break;
                case 'subletter-update':
                    subletter_update(form, usrResponse);
                    break;
                case 'subletter-payment':
                    subletter_payment(form, usrResponse);
                    break;
            };

        },
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
        rules:{
            Amount: {
                myCurrency: true,
                range: [0, 10000]
            },
            ContactEmail: {
                myEmail: true
            },
            ContactNumber: {
                myContactNumber: true
            }
        }
    });
};

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
            }

        }
    });
};

function addSelectionValidation(myForm, formHandler) {
    $(myForm).validate({
        submitHandler: function(form){
            if ($("input[type='radio'][name='Sub_Letter_id']:checked").val()) {
                var usrResponse = $('#btnClicked').val();
                switch(formHandler) {
                    case 'subletter-manage':
                        subletter_manage(form, usrResponse);
                        break;
                    case 'subletter-add':
                        subletter_add(form, usrResponse);
                        break;
                    case 'subletter-update':
                        subletter_update(form, usrResponse);
                        break;
                };
            }
            else {
                var usrResponse = $('#btnClicked').val();
                if (usrResponse == 'Search'){
                    form.submit();
                }
                else
                {
                    error_Ok('Not selected', 'You have not selected an item.', function(res) {
                        switch(res) {
                            case 'Ok':
                                break;
                        };
                    });
                };
            };

        },
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
        rules:{
            Amount: {
                myCurrency: true,
                range: [0, 10000]
            },
            ContactEmail: {
                myEmail: true
            },
            ContactNumber: {
                myContactNumber: true
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

    function subletter_add(form, usrResponse) {
        switch(usrResponse){
            case 'Save':
                confirm_YesNoCancel('Add new sub-letter', 'Are you sure you want to add the new sub-letter?', function(res) {
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
            default:
                alert('unknown submit action: ' + usrResponse)
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

// Supplier
supplier_add
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

jQuery.validator.addMethod('myCurrency', function(value, element) {
    return this.optional(element) || /^(?=\(.*\)|[^()]*$)\(?\d{1,3}(,?\d{3})?(\.\d{2}?)?\)?$/.test(value);
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
