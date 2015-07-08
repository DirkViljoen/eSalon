'use strict';


var SubLettersModel = require('../../models/sub-letters');


module.exports = function (router) {

    var model = new SubLettersModel();


    router.get('/', function (req, res) {
        //You can find me at /sub-letters
        res.send('<p>sub-letters/</p><code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
    });

    router.get('/add', function (req, res) {
    	console.log('test add');
        //You can find me at /sub-letters/add
        //res.send('<code><pre>' + JSON.stringify(model, null, 2) + '</pre></code>');
        res.render('subletter-add', model)
    });
};
