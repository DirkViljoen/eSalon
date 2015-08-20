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
    };
};

function subletter_update(form, usrResponse) {
    switch(usrResponse){
        case 'Save':
            confirm_YesNoCancel('Update sub-letter', 'Are you sure you want to update the sub-letters details?', function(res) {
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
    };
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
    btn1.click({'result': 'Yes'}, function(event){
        callback(event.data.result);
    });
    var btn2 = dialog.getButton('btn-No');
    btn2.click({'result': 'No'}, function(event){
        callback(event.data.result);
        dialog.close();
    });
    var btn3 = dialog.getButton('btn-Cancel');
    btn3.click({'result': 'Cancel'}, function(event){
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
    btn1.click({'result': 'Yes'}, function(event){
        callback(event.data.result);
    });
    var btn2 = dialog.getButton('btn-No');
    btn2.click({'result': 'No'}, function(event){
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
        callback(event.data.result);
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
}, 'Please enter a valid South African contact number');

jQuery.validator.addMethod('myRadioGroup', function(value, element) {
    return this.optional(element) || /^[0-9]{3}\s{1}[0-9]{3}\s{1}[0-9]{4}$/.test(value);
}, 'Please enter a valid South African contact number');
