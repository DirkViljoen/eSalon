//require the Twilio module and create a REST client

var q = require('q');

var sms = function() {
    var client;
    var myNumber;

    return {
        config: function(conf) {
            client = require('twilio')(conf.accountSid, conf.authToken);
            myNumber = conf.from;

            console.log("sms active. Mode: " + conf.mode);
        },

        send: function(number, message) {
            var deferred = q.defer();
            var num = number.replace(/[' ']/gi, '');
            num = '+27' + num.substring(1, 10);
            console.log('libs/sms.js - send - ' + message + '; to: ' + num);

            client.messages.create({
                to: num,
                from: myNumber,
                body: message,
            }, function(err, message) {
                if (err) {
                    console.error(err);
                    deferred.reject(err);
                }
                else {
                    console.log('Message send success. sid: ' + message.sid);
                    deferred.resolve(result);
                }
            });
            return deferred.promise;
        }
    }
};

module.exports = sms();
