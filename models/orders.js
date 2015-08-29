'use strict';

var q = require('q');
var db = require('../libs/db');
var moment = require('moment');

module.exports = function OrdersModel() {
  function get(id) {
        console.log('Module - Order - Get');

      var deferred = q.defer();

      if (id) {
          console.log('Order get');
          db.query('CALL spOrder_Read(' + id + ');')
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

  function search(sname, dateTo, dateFrom) {
      console.log('Module - Order - Search');

      var deferred = q.defer();

      db.query('CALL spOrder_Search("' + dateTo + '","' + dateFrom + '", ' + sname + ');')
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
      console.log('Module - Order - Create');

      var deferred = q.defer();

      if (obj) {
          console.log('Creating Order Item.');
          db.execute('CALL sp_Insert_Order (' +
              obj.dateTo + ',' +
              obj.dateFrom + ',' +
              obj.sname + ');'

          )
              .then(
                  function (result){
                      console.log('Order created.');
                      deferred.resolve(result);
                  },
                  function (err){
                      console.error(new Error('Unable to create Order.'))
                      deferred.reject(err);
                  }
              );

      }
      else {
          console.error(new Error('Unable to create Order item. No object provided.'))
          deferred.reject(err);
      }

      return deferred.promise;
  };

  function update(obj) {
      console.log('Module - Order - Update');

      var deferred = q.defer();

      if (obj) {
          console.log('Updating Order Item.');
          db.execute('CALL sp_Update_Order (' +
            obj.OrderID + ',' +
            obj.dateTo + ',' +
            obj.dateFrom + ',' +
            obj.sname + ',' +
            obj.quantity + ',' +
            obj.notify + ',' + ')'
          )
              .then(
                  function (result){
                      console.log('Order Item updated. Updating History');

                      updateHistory(obj.OrderID, obj.price)
                        .then(
                          function(result){
                            deferred.resolve(result);
                          },
                          function(err) {
                            console.error(new Error('Unable to update Order History.'))
                            deferred.reject(err);
                          }
                        )
                  },
                  function (err){
                      console.error(new Error('Unable to update Order Item.'))
                      deferred.reject(err);
                  }
              );

      }
      else {
          console.error(new Error('Unable to update Order item. No object provided.'))
          deferred.reject(err);
      }

      return deferred.promise;
  };

  function disable(obj) {
      console.log('Module - Order - Delete');

      var deferred = q.defer();

      if (obj.OrderID) {
          console.log('Deleteing Order.');
          db.execute('CALL sp_Delete_Order (' +
                  obj.OrderID + ')'
              )
              .then(
                  function (result){
                      console.log('Order deleted.');
                      deferred.resolve(result);
                  },
                  function (err){
                      console.error(new Error('Unable to delete Order.'))
                      deferred.reject(err);
                  }
              );
      };

      return deferred.promise;
  };

  function addLine(obj) {
      console.log('Module - Order - Add line');

      var deferred = q.defer();

      if (obj) {
          console.log(obj.orderID);
          console.log(obj.quantity);
          console.log(obj.stockID);

          db.execute('CALL sp_Insert_Order_Line(' +
            obj.quantity + ',' +
            obj.stockID + ',' +
            obj.orderID + ')'
          )
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

  function updateLine(oid, quantity){
    console.log('Module - Order Line - Get');

    var deferred = q.defer();

    if (oid) {
        console.log('Order Line update order Line');
        console.log(oid);
        console.log(quantity);
        console.log(stockID);
        db.execute('CALL sp_Update_Order_Line(' +
          quantity + ',"' +
          stockID + ',"' +
          oid +');'
        )

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

  }

  return {
      index: get,
      find: search,
      create: add,
      update: update,
      remove: disable,
      addLine: addLine
  };
};
