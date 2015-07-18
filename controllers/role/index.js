'use strict';


var RoleModel = require('../../models/role');


module.exports = function (router) {

    var model = new RoleModel();


    router.get('/add', function (req, res) {
        res.render('role/role-add', model);
    });

    router.get('/maintain', function (req, res) {
        res.render('role/role-maintain', model);
    });
};
