angular.module("umbraco")
    .controller("UmbracoPersonalisationGroups.CampaignPhoneManagerPersonalisationGroupCriteriaController",
    function ($scope, $http) {

        function initCampaignDetailList() {
            $scope.availableItems = [];
            $http.post("/App_Plugins/UmbracoPersonalisationGroups/CampaignPhoneManager/ListCampaignDetails")
                .then(function (result) {
                    $scope.availableItems = result.data;
                    if (result.data.length > 0 && !$scope.renderModel) {
                        $scope.renderModel = result.data[0];
                    }
                });
        };

        $scope.renderModel = { };

        initCampaignDetailList();

        if ($scope.dialogOptions.definition) {
            var campaignPhoneManagerCriteriaSettings = JSON.parse($scope.dialogOptions.definition);
            for (var i = 0; i < $scope.availableItems.length; i++) {
                if ($scope.availableItems[i].NodeId === campaignPhoneManagerCriteriaSettings.NodeId) {
                    $scope.renderModel = $scope.availableItems[i];
                    break;
                }
            }
        }

        $scope.saveAndClose = function () {
            var serializedResult = "{ \"NodeId\": \"" + $scope.renderModel.NodeId + "\", " +
                "\"Title\": \"" + $scope.renderModel.Title + "\" }";
            $scope.submit(serializedResult);
        };
    });