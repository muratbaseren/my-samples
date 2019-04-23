/// <reference path="../../angular.js" />
/// <reference path="../jsonplaceholder.module.js" />
/// <reference path="../services/jsonplaceholder.service.js" />

jsonplaceholder.controller("ctrlJsonplaceholder", ["$scope", "jphService", function ($scope, jphService) {

    $scope.tableFilter = {
        id: "",
        name: "",
        email: "",
        username: "",
        address: "",
        geo: "",
        phone: "",
        website: "",
        company: ""
    };
    $scope.users = jphService.getUsers();

}]);