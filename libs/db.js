var mysql = require('mysql');
var q = require('q');

var db = function() {
    var connection;

    return {
        config: function(conf) {
            connection = mysql.createConnection({
                host     : conf.host,
                user     : conf.user,
                password : conf.password,
                database : conf.database
            });

            connection.connect(function(err) {
                if (err) {
                    console.error('error connecting: ' + err.stack);
                    return;
                }

                console.log('connected as id ' + connection.threadId);
                connection.query('USE ' + conf.database, function(err, result) {
                    if (!err){
                        console.log('Connection Successfull');
                    }
                    else{
                        console.log('Error while connecting to database.', err);
                    }
                });
            });
        },

        query: function(myQuery) {
            var deferred = q.defer();
            console.log(myQuery);
            connection.query(myQuery, function(err, rows, fields) {
                if (err) {
                    console.error(err);
                    deferred.reject(new Error(err));
                } else {
                    console.log('Query completed: ' + myQuery);
                    result = JSON.parse('{"rows": ' + JSON.stringify(rows[0]) + ',' + '"SQLstats": ' + JSON.stringify(rows[1]) + '}');
                    deferred.resolve(result);
                }
            });
            return deferred.promise;
        },

        execute: function(myQuery) {
            var deferred = q.defer();
            console.log(myQuery);
            connection.query(myQuery, function(err, rows, fields) {
                if (err) {
                    console.error(err);
                    deferred.reject(new Error(err));
                } else {
                    console.log('Execute completed: ' + myQuery);
                    result = JSON.parse('{"rows": {},' + '"SQLstats": ' + JSON.stringify(rows) + '}');
                    deferred.resolve(result);
                }
            });
            return deferred.promise;
        },

        echo: function(a) {
            return a;
        }
    }
};

module.exports = db();
