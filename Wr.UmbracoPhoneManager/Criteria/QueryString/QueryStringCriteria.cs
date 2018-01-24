using System.Collections.Generic;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Storage;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public class QueryStringCriteria : IPhoneManagerCriteria
    {
        /// <summary>
        /// For the selection of records that match the querystrings passed with the request
        /// </summary>
        public QueryStringCriteria() { }

        public List<PhoneManagerCampaignDetail> GetMatchingRecordsFromPhoneManager(CriteriaParameterHolder _criteriaParameters, IRepository _repository)
        {
            List<PhoneManagerCampaignDetail> foundRecords = new List<PhoneManagerCampaignDetail>();

            var cleansedQueryStrings = _criteriaParameters.CleansedQueryStrings;

            if (cleansedQueryStrings?.HasKeys() ?? false)
            {
                var foundDefaultCampaignQSValue = cleansedQueryStrings[_repository.GetDefaultSettings().DefaultCampaignQueryStringKey] ?? string.Empty;

                if (!string.IsNullOrEmpty(foundDefaultCampaignQSValue))
                {
                    foundRecords.AddRange(_repository.GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(
                        foundDefaultCampaignQSValue,
                        AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.CampaignCode,
                        AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.UseAltCampaignQueryStringKey, cleansedQueryStrings
                        ));
                }

                foundRecords.AddRange(_repository.GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(
                        AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.CampaignCode,
                        AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.UseAltCampaignQueryStringKey, cleansedQueryStrings
                        ));
            }

            return foundRecords;
        }
    }
}