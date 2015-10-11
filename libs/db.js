var mysql = require('mysql');
var q = require('q');

function mysql_escape (str) {
    return str.replace(/[\0\x08\x09\x1a\n\r"'\\\%]/g, function (char) {
        switch (char) {
            case "\0":
                return "\\0";
            case "\x08":
                return "\\b";
            case "\x09":
                return "\\t";
            case "\x1a":
                return "\\z";
            case "\n":
                return "\\n";
            case "\r":
                return "\\r";
            case "\"":
            case "'":
            case "\\":
            case "%":
                return "\\"+char; // prepends a backslash to backslash, percent,
                                  // and double/single quotes
        }
    });
}

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

                    // console.log(result);
                    deferred.resolve(result);
                };
            });
            return deferred.promise;
        },

        audit: function(obj){
            console.log("-libs/db Audit");
            console.log(obj);
            var q = 'Call sp_audit_create(' + obj.id + ',"' + obj.action + '","' + obj.description + '")';
            connection.query(q, function(err, rows){
                if (err) {
                    console.log(err);
                    console.log('-- -- -- -- Audit log failed. -- -- -- --');
                }
                else
                {
                    console.log('-- -- -- -- Audit logged. -- -- -- --')
                }
            })
            return {done:true};
        }
    }
};

module.exports = db();
