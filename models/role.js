'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function RoleModel() {

    function getPermissions(obj) {
        console.log('Module - Other - Audit');

        var deferred = q.defer();

        db.query('CALL sp_getPermissions();')
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

    function get(id) {
        console.log('Module - Role - get');

        var deferred = q.defer();

        db.query('CALL sp_getRolePermissions('+id+');')
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

    function create(obj) {
        console.log('Module - Role - create');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Role.');
            db.execute('CALL sp_Insert_Role (' +
                obj.name + ')'
            )
                .then(
                    function (result){
                        console.log('Role created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Role.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to create role. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };


    function update(obj) {
        console.log('Module - Role - update');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Role.');
            db.execute('CALL sp_Update_Role (' +
                obj.id + ',' +
                obj.name + ')'
            )
                .then(
                    function (result){
                        console.log('Role updated.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to update Role.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to update role. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function createPermission(obj) {
        console.log('Module - Role - createPermission');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Role Permission.');
            db.execute('CALL sp_Insert_RolePermission (' +
                obj.rid + ',' +
                obj.pid + ')'
            )
                .then(
                    function (result){
                        console.log('Role permission created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Role permission.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to create role permission. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function deletePermissions(obj) {
        console.log('Module - Role - createPermission');

        var deferred = q.defer();

        if (obj) {
            console.log('Deleting Role Permission.');
            db.execute('CALL sp_Delete_RolePermissions (' +
                obj.rid + ')'
            )
                .then(
                    function (result){
                        console.log('Role permission deleted.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to delete Role permission.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to delete role permission. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    return {
        permissions: getPermissions,
        getRole: get,
        create: create,
        createPermission: createPermission,
        update: update,
        deletePermission: deletePermissions
    };
};
