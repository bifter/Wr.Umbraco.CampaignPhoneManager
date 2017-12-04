using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class XPathDataSource_Live : IXPathDataSource
    {

        private UmbracoHelper _umbracoHelper;

        public XPathDataSource_Live()
        {
            _umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
        }

        public DefaultSettings LoadDefaultSettings(string xpath)
        {
            var content = _umbracoHelper.TypedContentAtXPath(xpath);

            var result = new DefaultSettings();

            foreach (var item in content)
            {
                result.DefaultCampaignQuerystringKey = item.GetPropertyValue<string>("campaignQuerystringKey");
                result.DefaultPhoneNumber = item.GetPropertyValue<string>("defaultPhoneNumber");
            }

            return new DefaultSettings();
        }

        public List<PhoneNumber> GetDataByXPath(string xpath)
        {
            var qscontent = _umbracoHelper.TypedContentAtXPath(xpath);

            if (qscontent != null)
                return ConvertIPublishedContentToModel(qscontent);

            return new List<PhoneNumber>();
        }

        private List<PhoneNumber> ConvertIPublishedContentToModel(IEnumerable<IPublishedContent> content)
        {
            var result = new List<PhoneNumber>();
            if (content != null)
            {
                foreach (var item in content)
                {
                    result.Add(ConvertIPublishedContentToModel(item));
                }
            }
            return result;
        }

        private PhoneNumber ConvertIPublishedContentToModel(IPublishedContent content)
        {
            PhoneNumber result = new PhoneNumber()
            {
                entryPage = content.GetPropertyValue<string>("entryPage"),
                altMarketingCode = content.GetPropertyValue<string>("altMarketingCode"),
                campaignCode = content.GetPropertyValue<string>("campaignCode"),
                overwritePersistingItem = content.GetPropertyValue<bool>("overwritePersistingItem"),
                doNotPersistAcrossVisits = content.GetPropertyValue<bool>("doNotPersistAcrossVisits"),
                persistDurationOverride = content.GetPropertyValue<int>("persistDurationOverride"),
                phoneNumber = content.GetPropertyValue<string>("phoneNumber"),
                referrer = content.GetPropertyValue<string>("referrer"),
                useAltCampaignQuerystringKey = content.GetPropertyValue<string>("useAltCampaignQuerystringKey")
            };

            if (string.IsNullOrEmpty(result.phoneNumber)) // not a valid record if there is no telephone number
                return null;

            return result;
        }
    }
}