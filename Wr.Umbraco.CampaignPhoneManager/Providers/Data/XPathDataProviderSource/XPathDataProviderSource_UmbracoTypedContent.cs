using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
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
                result.DefaultCampaignQueryStringKey = item.GetPropertyValue<string>("campaignQueryStringKey");
                result.DefaultPhoneNumber = item.GetPropertyValue<string>("defaultPhoneNumber");
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
                EntryPage = content.GetPropertyValue<string>("entryPage"),
                AltMarketingCode = content.GetPropertyValue<string>("altMarketingCode"),
                CampaignCode = content.GetPropertyValue<string>("campaignCode"),
                OverwritePersistingItem = content.GetPropertyValue<bool>("overwritePersistingItem"),
                DoNotPersistAcrossVisits = content.GetPropertyValue<bool>("doNotPersistAcrossVisits"),
                PersistDurationOverride = content.GetPropertyValue<int>("persistDurationOverride"),
                PhoneNumber = content.GetPropertyValue<string>("phoneNumber"),
                Referrer = content.GetPropertyValue<string>("referrer"),
                UseAltCampaignQueryStringKey = content.GetPropertyValue<string>("useAltCampaignQueryStringKey")
            };

            if (string.IsNullOrEmpty(result.PhoneNumber)) // not a valid record if there is no telephone number
                return null;

            return result;
        }
    }
}