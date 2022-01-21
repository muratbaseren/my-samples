/// <reference path="../jsonplaceholder.module.js" />

jsonplaceholder.filter("toUpper", function () {
    return function (x) {
        return x.toUpperCase();
    };
});