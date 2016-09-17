(function () {
    "use strict";
    angular
        .module("riskManager")
        .controller("highRiskCustomersController", ["$scope", "$http", highRiskCustomersController]);
    //"customerRiskResource",customerRiskResource
    function highRiskCustomersController($scope, $http) {
        //customerRiskResource.query(function(data) {
        //    $scope.highRiskCustomers = data;
        //});
        $http.get("/api/customerrisk").then(function(data) {
            $scope.highRiskCustomers = data.data;
        });
    };

}());
