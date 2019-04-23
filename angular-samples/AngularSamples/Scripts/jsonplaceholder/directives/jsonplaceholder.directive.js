/// <reference path="../jsonplaceholder.module.js" />

jsonplaceholder.directive("filterId", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='number' ng-model='tableFilter.id' placeholder='find id'></div>"
    };
});

jsonplaceholder.directive("filterName", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='tableFilter.name' placeholder='find name'></div>"
    };
});

jsonplaceholder.directive("filterEmail", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='tableFilter.email' placeholder='find email'></div>"
    };
});

jsonplaceholder.directive("filterUsername", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='tableFilter.username' placeholder='find username'></div>"
    };
});

jsonplaceholder.directive("filterAddress", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='tableFilter.address' placeholder='find address'></div>"
    };

});

jsonplaceholder.directive("filterGeo", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='tableFilter.geo' placeholder='find lat/lng'></div>"
    };
});

jsonplaceholder.directive("filterPhone", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='tableFilter.phone' placeholder='find phone'></div>"
    };
});

jsonplaceholder.directive("filterWebsite", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='tableFilter.website' placeholder='find website'></div>"
    };
});

jsonplaceholder.directive("filterCompany", function () {
    return {
        restrict: "E",
        template: "<div class='table-filter'><input type='text' ng-model='company' placeholder='find company'></div>"
    };
});