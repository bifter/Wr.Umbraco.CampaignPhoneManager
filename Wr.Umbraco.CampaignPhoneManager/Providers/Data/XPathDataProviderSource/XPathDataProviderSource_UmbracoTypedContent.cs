using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// Data source access using Umbraco TypedContentAtXPath methods
    /// </summary>
    public class XPathDataProviderSource_UmbracoTypedContent : IXPathDataProviderSource
    {

        private UmbracoHelper _umbracoHelper;

        public XPathDataProviderSource_UmbracoTypedContent()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public CampaignPhoneManagerModel LoadDefaultSettings(string xpath)
        {
            var content = _umbracoHelper.TypedContentAtXPath(xpath);

            var result = new CampaignPhoneManagerModel();

            foreach (var item in content)
            {
                result.DefaultCampaignQueryStringKey = item.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultCampaignQueryStringKey);
                result.DefaultPhoneNumber = item.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultPhoneNumber);
            }

            return new CampaignPhoneManagerModel();
        }

        public List<CampaignDetail> GetDataByXPath(string xpath)
        {
            var qscontent = _umbracoHelper.TypedContentAtXPath(xpath);

            if (qscontent != null)
                return ConvertIPublishedContentToModel(qscontent);

            return new List<CampaignDetail>();
        }

        private List<CampaignDetail> ConvertIPublishedContentToModel(IEnumerable<IPublishedContent> content)
        {
            var result = new List<CampaignDetail>();
            if (content != null)
            {
                foreach (var item in content)
                {
                    result.Add(ConvertIPublishedContentToModel(item));
                }
            }
            return result;
        }

        private CampaignDetail ConvertIPublishedContentToModel(IPublishedContent content)
        {
            CampaignDetail result = new CampaignDetail()
            {
                Id = content.Id.ToString(),
                EntryPage = content.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage),
                AltMarketingCode = content.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.AltMarketingCode),
                CampaignCode = content.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode),
                OverwritePersistingItem = content.GetPropertyValue<bool>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.OverwritePersistingItem),
                DoNotPersistAcrossVisits = content.GetPropertyValue<bool>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.DoNotPersistAcrossVisits),
                PersistDurationOverride = content.GetPropertyValue<int>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.PersistDurationOverride),
                PhoneNumber = content.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.PhoneNumber),
                Referrer = content.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.Referrer),
                UseAltCampaignQueryStringKey = content.GetPropertyValue<string>(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey)
            };

            if (string.IsNullOrEmpty(result.PhoneNumber)) // not a valid record if there is no telephone number
                return null;

            return result;
        }
    }
}