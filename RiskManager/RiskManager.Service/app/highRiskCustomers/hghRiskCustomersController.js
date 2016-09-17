(function () {
    "use strict";
    angular
        .module("riskManager")
        .controller("hghRiskCustomersController", ["$scope", "$http",  hghRiskCustomersController]);
    //"customerRiskResource",customerRiskResource
    function hghRiskCustomersController($scope, $http) {
        //customerRiskResource.query(function(data) {
        //    $scope.highRiskCustomers = data;
        //});
        $http.get("/api/customerrisk").then(function(data) {
            $scope.highRiskCustomers = data.data;
        });
    };

}());
