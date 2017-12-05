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

        public DefaultSettings LoadDefaultSettings(string xpath)
        {
            var content = _umbracoHelper.TypedContentAtXPath(xpath);

            var result = new DefaultSettings();

            foreach (var item in content)
            {
                result.DefaultCampaignQueryStringKey = item.GetPropertyValue<string>("campaignQueryStringKey");
                result.DefaultPhoneNumber = item.GetPropertyValue<string>("defaultPhoneNumber");
            }

            return new DefaultSettings();
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
                id = content.Id.ToString(),
                entryPage = content.GetPropertyValue<string>("entryPage"),
                altMarketingCode = content.GetPropertyValue<string>("altMarketingCode"),
                campaignCode = content.GetPropertyValue<string>("campaignCode"),
                overwritePersistingItem = content.GetPropertyValue<bool>("overwritePersistingItem"),
                doNotPersistAcrossVisits = content.GetPropertyValue<bool>("doNotPersistAcrossVisits"),
                persistDurationOverride = content.GetPropertyValue<int>("persistDurationOverride"),
                phoneNumber = content.GetPropertyValue<string>("phoneNumber"),
                referrer = content.GetPropertyValue<string>("referrer"),
                useAltCampaignQueryStringKey = content.GetPropertyValue<string>("useAltCampaignQueryStringKey")
            };

            if (string.IsNullOrEmpty(result.phoneNumber)) // not a valid record if there is no telephone number
                return null;

            return result;
        }
    }
}