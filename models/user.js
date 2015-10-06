'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function UserModel() {
    function get(id) {
        console.log('Module - User - Get');

        var deferred = q.defer();

        if (id) {
            db.query('CALL spUser_Read_ID(' + id + ');')
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

    function search() {
        // console.log('Module - Employee - Search');

        // var deferred = q.defer();

        // db.query('CALL spEmployee_Read_Search("%' + fname + '%","%' + lname + '%","%' + role + '%");')
        //     .then(
        //         function (result){
        //             deferred.resolve(result);
        //         },
        //         function (err){
        //             deferred.reject(new Error(err));
        //         }
        //     );

        // return deferred.promise;
    };

    function add(obj) {
        console.log('Module - User - Create');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL sp_Insert_User (' +
                    obj.name + ',' +
                    obj.password + ',' +
                    obj.employeeId + ',' +
                    obj.roleId + ')'
                )
                .then(
                    function (result){
                        console.log('User created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create User.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to create User.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function update(obj) {
        console.log('Module - User - Update');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL sp_Update_User (' +
                    obj.userId + ',' +
                    obj.name + ',' +
                    obj.employeeId + ',' +
                    obj.roleId + ')'
                )
                .then(
                    function (result){
                        console.log('User updated.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to update User.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to update User. No user object.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function disable(obj) {
        // console.log('Module - Client - Delete');

        // var deferred = q.defer();

        // if (obj.clientId != null) {
        //     console.log('Deleteing Client.');
        //     db.execute('CALL spClient_Delete (' +
        //             obj.clientId + ')'
        //         )
        //         .then(
        //             function (result){
        //                 console.log('Client deleted.');
        //                 deferred.resolve(result);
        //             },
        //             function (err){
        //                 console.error(new Error('Unable to delete Client.'))
        //                 deferred.reject(err);
        //             }
        //         );
        // };

        // return deferred.promise;
    };

    function changePwd(obj) {
        console.log('Module - User - Change Password');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL sp_Update_User_pwd (' +
                    obj.userId + ',' +
                    obj.password + ')'
                )
                .then(
                    function (result){
                        console.log('Password updated.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to update password.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to update Password. No user object.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    return {
        index: get,
        find: search,
        create: add,
        update: update,
        remove: disable,
        changePassword: changePwd
    };
};
