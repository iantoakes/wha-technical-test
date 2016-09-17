(function () {
    "use strict";

    angular
        .module("riskManager")
        .factory("customerRiskResource", ["$resource", customerRiskResource]);

    function customerRiskResource($resource) {
        return $resource("/api/customerrisk/:successRate");
    };
}());