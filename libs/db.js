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
            console.log('libs/db.js - query - ' + myQuery);

            connection.query(myQuery, function(err, rows, fields) {
                if (err) {
                    console.error(err);
                    deferred.reject(err);
                }
                else {
                    console.log('Query completed.');
                    result = {"rows": rows[0], "SQLstats": rows[1]};

                    // console.log(result);

                    deferred.resolve(result);
                }
            });
            return deferred.promise;
        },

        execute: function(myQuery) {
            var deferred = q.defer();
            console.log('libs/db.js - execute - ' + myQuery);

            connection.query(myQuery, function(err, rows, fields) {
                if (err) {
                    console.error(err);
                    deferred.reject(err);
                }
                else {
                    console.log('Execute completed.');

                    // Determine if rows are returned
                    if (rows.length) {
                        // Determine if an insert id is returned
                        if (rows[0][0].insertId) {
                            rows[1].insertId = rows[0][0].insertId;
                        }
                        result = {"rows": [], "SQLstats": rows[1]};
                    }
                    else {
                        result = {"rows": [], "SQLstats": rows}
                    };

                    action = "Other";

                    if (myQuery.indexOf("elete") >= 0) action = "Delete";
                    if (myQuery.indexOf("nsert") >= 0) action = "Insert";
                    if (myQuery.indexOf("pdate") >= 0) action = "Update";

                    temp = 'Call sp_audit_create(1,"' + action + '","' + myQuery.replace(/"/g,'\\\"') + '")';

                    console.log(temp);
                    connection.query(temp, function(err, rows){
                        if (err) {
                            console.log('-- -- -- -- Audit log failed. -- -- -- --');
                        }
                        else
                        {
                            console.log('-- -- -- -- Audit logged. -- -- -- --')
                        }
                    })

                    // console.log(result);
                    deferred.resolve(result);
                };
            });
            return deferred.promise;
        }
    }
};

module.exports = db();
