/// <reference path="../../angular.js" />
/// <reference path="../jsonplaceholder.module.js" />

jsonplaceholder.service("jphService", ["$http", function ($http) {

    this.getUsers = function () {
        var list = [];

        $http.get("http://jsonplaceholder.typicode.com/users").then(function (response) {

            for (var i = 0; i < response.data.length; i++) {
                var item = response.data[i];

                list.push({
                    id: item.id,
                    name: item.name,
                    email: item.email,
                    username: item.username,
                    address: item.address.street + " " + item.address.suite + " " +
                             item.address.city + " " + item.address.zipcode,
                    geo: "lat : " + item.address.geo.lat + ";" + "lng : " + item.address.geo.lng,
                    phone: item.phone,
                    website: item.website,
                    company: item.company.name
                });
            }
        });

        return list;

    };
}]);