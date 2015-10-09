'use strict';

var OtherModel = require('../../models/other');


module.exports = function (router) {

    var model = new OtherModel();

    router.get('/expenses', function (req, res) {

        res.render("Import/ImportExpenses", {});

    });

};
