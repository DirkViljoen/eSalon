'use strict';

var q = require('q');
var db = require('../libs/db');

module.exports = function ExpensesModel() {
    function add(obj) {
        console.log('Module - Expenses - Add');

        var deferred = q.defer();

        if (obj) {
            db.execute('CALL sp_Insert_Expense (' +
                    obj.name + ',' +
                    obj.quantity + ',' +
                    obj.date + ',' +
                    obj.price + ',' +
                    obj.category + ',' +
                    obj.paymentMethod + ')'
                )
                .then(
                    function (result){
                        console.log('Expense created.');
                        deferred.resolve(result);
                    },
                    function (err){
                        console.error(new Error('Unable to create Expense.'))
                        deferred.reject(err);
                    }
                );
        }
        else {
            console.error(new Error('Unable to create Address.'))
            deferred.reject(err);
        }

        return deferred.promise;
    };

    return {
        create: add
    };
};
