angular.module("umbraco")
    .controller("UmbracoPersonalisationGroups.CampaignPhoneManagerPersonalisationGroupCriteriaController",
    function ($scope, $http) {

        function initCampaignDetailList() {
            $scope.availableItems = [];
            $http.post("/App_Plugins/UmbracoPersonalisationGroups/CampaignPhoneManager/ListCampaignDetails")
                .then(function (result) {
                    $scope.availableItems = result.data;
                });
        };

        $scope.renderModel = { };

        initCampaignDetailList();

        if ($scope.dialogOptions.definition) {
            var campaignPhoneManagerCriteriaSettings = JSON.parse($scope.dialogOptions.definition);
            $scope.renderModel = campaignPhoneManagerCriteriaSettings;
        }

        $scope.saveAndClose = function () {
            var serializedResult = "{ \"NodeId\": \"" + $scope.renderModel.NodeId + "\", " +
                "\"Title\": \"" + $scope.renderModel.Title + "\" }";
            $scope.submit(serializedResult);
        };

        $scope.saveAndClose2 = function (data) {
            alert(data.Title);
        };

    });