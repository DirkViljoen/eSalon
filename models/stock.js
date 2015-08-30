'use strict';

var q = require('q');
var db = require('../libs/db');
var moment = require('moment');

module.exports = function StockModel() {

    function get(id) {
          console.log('Module - Stock - Get');

        var deferred = q.defer();

        if (id) {
            console.log('Stock get');
            db.query('CALL spStock_Read(' + id + ');')
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

    function search(sname, pname, bname) {
        console.log('Module - Stock - Search');

        var deferred = q.defer();

        db.query('CALL spStock_Search("%' + sname + '%","%' + bname + '%","%' + pname + '%");')
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
        console.log('Module - Stock - Create');

        var deferred = q.defer();

        if (obj) {
            console.log('Creating Stock Item.');
            db.execute('CALL sp_Insert_Stock (' +
                obj.brandName + ',' +
                obj.productName + ',' +
                obj.price + ',' +
                obj._size + ',' +
                obj.active + ',' +
                obj.quantity + ',' +
                obj.barcode + ',' +
                '1' + ',' +
                obj.supplierID + ')'

            )
                .then(
                    function (result){
                        console.log('Stock Item created. cREATING HistorY');

                        addHistory(result.SQLstats.insertId, obj.price)
                          .then(
                            function(result){
                              deferred.resolve(result);
                            },
                            function(err) {
                              console.error(new Error('Unable to create Stock History.'))
                              deferred.reject(err);
                            }
                          )
                    },
                    function (err){
                        console.error(new Error('Unable to create Stock Item.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to create stock item. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function update(obj) {
        console.log('Module - Stock - Update');

        var deferred = q.defer();

        if (obj) {
            console.log('Updating Stock Item.');
            db.execute('CALL sp_Update_Stock (' +
              obj.stockID + ',' +
              obj.brandName + ',' +
              obj.productName + ',' +
              obj.price + ',' +
              obj._size + ',' +
              obj.quantity + ',' +
              obj.barcode + ',' +
              obj.supplierID + ')'

            )
                .then(
                    function (result){
                        console.log('Stock Item updated. Updating History');

                        updateHistory(obj.stockID, obj.price)
                          .then(
                            function(result){
                              deferred.resolve(result);
                            },
                            function(err) {
                              console.error(new Error('Unable to update Stock History.'))
                              deferred.reject(err);
                            }
                          )
                    },
                    function (err){
                        console.error(new Error('Unable to update Stock Item.'))
                        deferred.reject(err);
                    }
                );

        }
        else {
            console.error(new Error('Unable to update stock item. No object provided.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    function disable(obj) {
        console.log('Module - Stock - Delete');

        var deferred = q.defer();

        if (obj.stockID) {
            console.log('Deleteing Stock.');
            db.execute('CALL sp_Delete_Stock (' +
                    obj.stockID + ')'
                )
                .then(
                    function (result){
                        console.log('Stock deleted.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to delete Stock.'))
                        deferred.reject(err);
                    }
                );
        };

        return deferred.promise;
    };

    function addHistory(id, price) {
        console.log('Module - Stock History - Get');

        var deferred = q.defer();

        var startdate = moment().format('YYYY-MM-DD');
        var endDate = null;

        if (id) {
            console.log('Stock Histort get products history');
            db.execute('CALL sp_Insert_Stock_History(' +
              price + ',"' +
              startdate + '",' +
              endDate + ',' +
              id +');'
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

    function updateHistory(sid, price){
      console.log('Module - Stock History - Get');

      var deferred = q.defer();

      var date = moment().format('YYYY-MM-DD');

      if (sid) {
          console.log('Stock Histort get products history');
          db.execute('CALL sp_Update_Stock_History(' +
            price + ',"' +
            date + '",' +
            sid +');'
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
        remove: disable
    };
};
