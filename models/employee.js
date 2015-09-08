'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function EmployeeModel() {
    function get(id) {
        console.log('Module - Employee - Get');

        var deferred = q.defer();

        if (id) {
            db.query('CALL spEmployee_Read_ID(' + id + ');')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else
        {
            deferred.reject(new Error('No ID'));
        }

        return deferred.promise;
    };

    function search(fname, lname, role) {
        console.log('Module - Employee - Search');

        var deferred = q.defer();

        db.query('CALL spEmployee_Read_Search("%' + fname + '%","%' + lname + '%","%' + role + '%");')
            .then(
                function (result){
                    deferred.resolve(result);
                },
                function (err){
                    deferred.reject(new Error(err));
                }
            );

        return deferred.promise;
    };

    function add(obj) {
        console.log('Module - Employee - Add');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL spEmployee_Create (' +
                    obj.cfname + ',' +
                    obj.clname + ',' +
                    obj.cnumber + ',' +
                    obj.cemail + ',' +
                    obj.salary + ',' +
                    obj.image + ',' +
                    obj.addressId + ')'
                )
                .then(
                    function (result){
                        console.log('Employee created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Employee.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to create Employee.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function update(obj) {
        console.log('Module - Employee - Update');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL spEmployee_Update (' +
                    obj.employeeId + ',' +
                    obj.cfname + ',' +
                    obj.clname + ',' +
                    obj.cnumber + ',' +
                    obj.cemail + ',' +
                    obj.salary + ',true,' +
                    obj.image + ',' +
                    obj.addressId + ')'
                )
                .then(
                    function (result){
                        console.log('Employee updated.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to update Employee.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to update Employee. No employee object.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function disable(obj) {
        console.log('Module - Employee - Delete');

        var deferred = q.defer();

        if (obj.employeeId != null) {
            console.log('Deleteing Employee.');
            db.execute('CALL spEmployee_Delete (' +
                    obj.employeeId + ')'
                )
                .then(
                    function (result){
                        console.log('Employee deleted.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to delete Employee.'))
                        deferred.reject(err);
                    }
                );
        };

        return deferred.promise;
    };

    function getBookings(id) {
        console.log('Module - Employee - Get bookings');

        var deferred = q.defer();

        if (id) {
            db.query('CALL spEmployeeBookings_Read_ID(' + id + ');')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else
        {
            deferred.reject(new Error('No ID'));
        }

        return deferred.promise;
    };

    function getLeave(id) {
        console.log('Module - Employee - Get leave');

        var deferred = q.defer();

        if (id) {
            db.query('CALL spEmployee_Leave_ID(' + id + ');')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else
        {
            deferred.reject(new Error('No ID'));
        }

        return deferred.promise;
    };

    return {
        index: get,
        find: search,
        create: add,
        update: update,
        remove: disable,
        bookings: getBookings,
        leave: getLeave
    };
};
