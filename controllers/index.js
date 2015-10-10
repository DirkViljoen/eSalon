'use strict';

module.exports = function (router) {

// Navigation

    router.get('/', function (req, res) {
        res.redirect('/login');
    });
};
