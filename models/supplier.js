'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function supplierModel() {

    function get(id) {
          console.log('Module - Supplier - Get');

        var deferred = q.defer();

        if (id) {
            console.log('Supplier get');
            db.query('CALL spSupplier_Read(' + id + ');')
                .then(
                    function (result){
                        deferred.resolve(result);
                    },
                    function (err){
                        deferred.reject(new Error(err));
                    }
                );
        }
        else{
            deferred.reject(new Error('No ID'));
        }

        return deferred.promise;
    };

    function search(sname, pname) {
        console.log('Module - Supplier - Search');

        var deferred = q.defer();

        db.query('CALL spSupplier_Search("%' + sname + '%","%' + pname + '%");')
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
        console.log('Module - supplier - Create');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating supplier Item.');
            db.execute('CALL sp_Insert_Supplier (' +
                obj.name + ',' +
                obj.contact + ',' +
                obj.email + ',' +
                obj.active + ')'

            )
            .then(
                  function(result){
                    deferred.resolve(result);
                  },
                  function(err) {
                    console.error(new Error('Unable to create supplier.'))
                    deferred.reject(err);
                  }
            );
        }
        else {
            console.error(new Error('Unable to create supplier. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function update(obj) {
        console.log('Module - supplier - Update');

        var deferred = q.defer();

        if (obj) {
            console.log('Updating supplier Item.');
            db.execute('CALL sp_Update_Supplier (' +
              obj.supplierID + ',' +
              obj.name + ',' +
              obj.contact + ',' +
              obj.email +')'

            )
                .then(
                  function (result){
                    deferred.resolve(result);
                  },
                  function(err) {
                    console.error(new Error('Unable to update supplier History.'))
                    deferred.reject(err);
                  }
                );
        }
        else {
            console.error(new Error('Unable to update supplier item. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function disable(obj) {
        console.log('Module - supplier - Delete');

        var deferred = q.defer();

        if (obj.supplierID) {
            console.log('Deleteing supplier.');
            db.execute('CALL sp_Delete_Supplier (' +
                    obj.supplierID + ')'
                )
                .then(
                    function (result){
                        console.log('supplier deleted.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to delete supplier.'))
                        deferred.reject(err);
                    }
                );
        };

        return deferred.promise;
    };


    return {
        /*supName: 'Biokenetics4u',
        supContactNum: '+27 83 123 1234',
        supEmail: 'john@bk4u.com',
        supProducts:
          [
            {
              'id': '1',
              'value': 'Shampoo'
            }
          ],
        arrSearchProducts:
          [
            {
              'id': '',
              'value': 'Search based on ordered products'
            },
            {
              'id': '1',
              'value': 'Shampoo'
            },
            {
              'id': '2',
              'value': 'Gel'
            },
            {
              'id': '3',
              'value': 'Conditioner'
            }

          ],
        supOrders:
          [
            {
              'date': '12 June 2015',
              'product': 'Shampoo',
              'quantity': '5',
              'price': '50'
            },
            {
              'date': '26 June 2015',
              'product': 'Conditioner',
              'quantity': '5',
              'price': '55'
            }
          ]*/

          index: get,
          find: search,
          create: add,
          update: update,
          remove: disable
    };
};
