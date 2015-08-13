var mysql = require('mysql');

function newConnection(connection) {
    console.log('test');
    connection = mysql.createConnection({
        host     : 'localhost',
        user     : 'root',
        // password : '',
        database : 'eSalon'
    });

    connection.connect();

    connection.query('USE eSalon', function(err, result) {
        if (!err){
            console.log('Connection Successfull');
        }
        else{
            console.log('Error while connecting to database.', err);
        }
    });

    return connection;
};

function closecon(connection){
    var mysql = require('mysql');
    connection.end();
};
