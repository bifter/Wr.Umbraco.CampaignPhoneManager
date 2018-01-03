using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class QueryStringCriteria : ICampaignPhoneManagerCriteria
    {
        private IRepository _repository;
        private readonly CriteriaParameterHolder _criteriaParameters;

        public QueryStringCriteria(CriteriaParameterHolder criteriaParameters)
        {
            _criteriaParameters = criteriaParameters;
            _repository = new XPathRepository();
        }

        public QueryStringCriteria(CriteriaParameterHolder criteriaParameters, IRepository repository)
        {
            _criteriaParameters = criteriaParameters;
            _repository = repository;
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            var cleansedQueryStrings = _criteriaParameters.CleansedQueryStrings;

            if (cleansedQueryStrings?.HasKeys() ?? false)
            {
                var foundDefaultCampaignQSValue = cleansedQueryStrings[_repository.GetDefaultSettings().DefaultCampaignQueryStringKey] ?? string.Empty;

                if (!string.IsNullOrEmpty(foundDefaultCampaignQSValue))
                {
                    foundRecords.AddRange(_repository.GetMatchingCriteriaRecords_QueryString_UsingDefaultCampaignQSKey(
                        foundDefaultCampaignQSValue,
                        AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode,
                        AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, cleansedQueryStrings
                        ));
                }

                foundRecords.AddRange(_repository.GetMatchingCriteriaRecords_QueryString_UsingAltCampaignQueryStringKey(
                        AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode,
                        AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey, cleansedQueryStrings
                        ));
            }

            return foundRecords;
        }
    }
}