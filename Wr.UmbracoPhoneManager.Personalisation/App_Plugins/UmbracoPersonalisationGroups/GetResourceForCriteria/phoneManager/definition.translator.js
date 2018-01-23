angular.module("umbraco.services")
    .factory("UmbracoPersonalisationGroups.PhoneManagerTranslatorService", function () {

        var service = {
            translate: function (definition) {
                var translation = "";
                if (definition) {
                    var selectedPhoneManagerDetails = JSON.parse(definition);
                    translation = selectedPhoneManagerDetails.Title;
                }

                return translation;
            }
        };

        return service;
    });