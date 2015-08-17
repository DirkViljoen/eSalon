$('.form-validate').validate({
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
        sAmount: {
            myCurrency: true,
            range: [0, 10000]
        },
        sContactEmail: {
            myEmail: true
        },
        sContactNumber: {
            myContactNumber: true
        }
    }
});

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
