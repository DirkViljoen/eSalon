//require the Twilio module and create a REST client

var q = require('q');

var email = function() {

    return {
        config: function(conf) {
            // mailer.extend(express, {
            //   from: 'no-reply@example.com',
            //   host: 'smtp.gmail.com', // hostname
            //   secureConnection: true, // use SSL
            //   port: 465, // port for secure SMTP
            //   transportMethod: 'SMTP', // default is SMTP. Accepts anything that nodemailer accepts
            //   auth: {
            //     user: 'gmail.user@gmail.com',
            //     pass: 'userpass'
            //   }
            // });


            // client = require('twilio')(conf.accountSid, conf.authToken);
            // myNumber = conf.from;

            // console.log("email active.");
        },



        send: function(number, message) {
            // var deferred = q.defer();
            // var num = number.replace(/[' ']/gi, '');
            // num = '+27' + num.substring(1, 10);
            // console.log('libs/sms.js - send - ' + message + '; to: ' + num);

            // client.messages.create({
            //     to: num,
            //     from: myNumber,
            //     body: message,
            // }, function(err, message) {
            //     if (err) {
            //         console.error(err);
            //         deferred.reject(err);
            //     }
            //     else {
            //         console.log('Message send success. sid: ' + message.sid);
            //         deferred.resolve(result);
            //     }
            // });
            // return deferred.promise;
        }
    }
};

module.exports = email();
