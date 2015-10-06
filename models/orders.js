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
          db.query('CALL spOrderLine_Read(' + id + ');')
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

      db.query('CALL spOrder_Search(' + sname + ', "' + dateTo + '","' + dateFrom + '");')
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
      console.log('Module - Order - Add');
      console.log(obj);

      var deferred = q.defer();

      if (obj.stock && obj.stock != []){
        console.log('Creating Order.');
        db.execute('CALL sp_Insert_Order (' +
                obj.datePlaced + ',' +
                obj.dateReceived + ',' +
                obj.supplierID + ')'
            )
            .then(
                function (result){
                    obj.orderID = result.SQLstats.insertId;
                    console.log('Order created, creating new Order_Line.');

                    for (i = 0; i < obj.stock.length; i++){
                        console.log('Adding service ' + i + 'out of ' + obj.stock.length);

                        db.execute('CALL sp_Insert_Order_Line (' +
                            obj.stock[i].quantity + ',' +
                            obj.stock[i].stockID + ',' +
                            obj.orderID + ')'
                        )
                        .then(
                            function (result){
                                console.log('New Order_Line created.');
                                if (i + 1 == obj.stock.length){
                                  deferred.resolve(result);
                                };
                            },
                            function (err){
                                console.error(new Error('Unable to create new Order_Line.'))
                                deferred.reject(err);
                            }
                        );
                    }
                },
                function (err){
                    console.error(new Error('Unable to create new Order.'))
                    deferred.reject(err);
                }
            );
      }
      else {
        deferred.reject(new error("No stock to add to order"));
      }

      return deferred.promise;
  };

  function update(obj) {
      console.log('Module - Order - Update');

      var deferred = q.defer();

      if (obj) {
          console.log('Updating Order Item.');
          db.execute('CALL sp_Update_Order (' +
            obj.date + ',' +
            obj.supplierID + ',' + ')'
          )
          .then(
              function (result){
                  console.log('Order updated, updating new Order_Line.');
                  db.execute('CALL sp_Update_Order_Line (' +
                      obj.quantity + ',' +
                      obj.stockID + ',' +
                      obj.orderID + ')'
                  )
                  .then(
                      function (result){
                          console.log('Order_Line updated.');
                          deferred.resolve(result);
                      },
                      function (err){
                          console.error(new Error('Unable to update Order_Line.'))
                          deferred.reject(err);
                      }
                  );
              },
              function (err){
                  console.error(new Error('Unable to update Order.'))
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

      if (obj.rOrderLine_id == null) {
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
      }
      else
      {
        console.log('Deleteing Order_line.');
        db.execute('CALL sp_Delete_Order_Line (' +
                obj.rOrderLine_id + ')'
            )
            .then(
                function (result){
                    console.log('Order_line deleted.');
                    deferred.resolve(result);
                },
                function (err){
                    console.error(new Error('Unable to delete Order_line.'))
                    deferred.reject(err);
                }
            );
      };

      return deferred.promise;
  };

  /*function addLine(obj) {
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
    console.log('Module - Order Line - PUT');

    var deferred = q.defer();

    if (oid) {
        console.log('Order Line update order Line');
        console.log(oid);
        db.execute('CALL sp_Update_Order_Line(' +
          oid.orderLineID + ',' +
          oid.quantity + ',null,null);'
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

  function readLine(obj){
  	console.log('Module - OrderLine - Get');

        var deferred = q.defer();

        if (obj) {
            console.log('OrderLine get');
            db.query('CALL spOrderLine_Read(' + obj.id + ');')
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

  	return {err: 'Not implemented'}
}*/

  return {
      index: get,
      find: search,
      create: add,
      update: update,
      remove: disable
  };
};
