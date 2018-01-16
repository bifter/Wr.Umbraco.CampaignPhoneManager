angular.module("umbraco.services")
    .factory("UmbracoPersonalisationGroups.CampaignPhoneManagerTranslatorService", function () {

        var service = {
            translate: function (definition) {
                var translation = "";
                if (definition) {
                    var selectedCampaignPhoneManagerDetails = JSON.parse(definition);
                    translation = selectedCampaignPhoneManagerDetails.Title;
                }

                return translation;
            }
        };

        return service;
    });