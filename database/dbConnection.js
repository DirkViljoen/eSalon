var mysql = require('mysql');

function newConnection() {

    var connection = mysql.createConnection({
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

    connection.end();

    return connection;
};

function opencon(connection){
    var mysql = require('mysql');
    connection.connect();
};

function closecon(connection){
    var mysql = require('mysql');
    connection.end();
};
