(function () {
    "use strict";
    angular
        .module("riskManager")
        .controller("highRiskBetsController", ["$scope", "$http", highRiskBetsController]);

    function highRiskBetsController($scope, $http) {
        $http.get("/api/highriskbets").then(function (data) {
            $scope.highRiskBets = data.data;
        });

        $scope.isIconVisible = function(category, riskCategories) {
            return riskCategories.indexOf(category) !== -1;
        };
    };

}());