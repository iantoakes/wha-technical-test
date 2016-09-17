(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("customerRiskResource",
        [
            "$resource",
            customerRiskResource
        ]);

    function customerRiskResource($resource) {
        return $resource("/api/customerrisk/:successRate");
    }

}());