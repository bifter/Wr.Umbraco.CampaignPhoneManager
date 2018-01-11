angular.module("umbraco")
    .controller("UmbracoPersonalisationGroups.CampaignPhoneManagerPersonalisationGroupCriteriaController",
    function ($scope, $http) {

        function initCampaignDetailList() {
            $scope.availableItems = [];
            $http.post("/App_Plugins/UmbracoPersonalisationGroups/CampaignPhoneManager/ListCampaignDetails")
                .then(function (result) {
                    $scope.availableItems = result.data;
                    /*if (result.data.length > 0 && !$scope.renderModel.groupName) {
                        $scope.renderModel.groupName = result.data[0];
                    }*/
                });
        };

        $scope.renderModel = {};

        initCampaignDetailList();

        if ($scope.dialogOptions.definition) {
            var campaignPhoneManagerCriteriaSettings = JSON.parse($scope.dialogOptions.definition);
            $scope.renderModel = campaignPhoneManagerCriteriaSettings;
        }

        $scope.saveAndClose = function () {
            var serializedResult = "{ \"DocumentId\": \"" + $scope.renderModel.DocumentId + "\", " +
                "\"Title\": \"" + $scope.renderModel.Title + "\" }";
            $scope.submit(serializedResult);
        };

    });