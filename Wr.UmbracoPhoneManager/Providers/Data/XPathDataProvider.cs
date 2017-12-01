using System.Collections.Generic;
using Umbraco.Core.Models;
using Umbraco.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers.Data
{

    public class XPathDataProvider : IDataProvider
    {

        private DefaultSettings _defaultSettings { get; set; }
        private UmbracoHelper _umbracoHelper;

        public XPathDataProvider(UmbracoHelper umbracoHelper)
        {
            _defaultSettings = LoadDefaultSettings();
            _umbracoHelper = umbracoHelper;
        }

        public DefaultSettings GetDefaultSettings()
        {
            return _defaultSettings;
        }

        private DefaultSettings LoadDefaultSettings()
        {
            var content = _umbracoHelper.TypedContentAtXPath("$ancestorOrSelf/ancestor-or-self::home[position()=1]//phoneManager");

            var result = new DefaultSettings();

            foreach (var item in content)
            {
                result.DefaultCampaignQuerystringKey = item.GetPropertyValue<string>("campaignQuerystringKey");
                result.DefaultPhoneNumber = item.GetPropertyValue<string>("defaultPhoneNumber");
            }

            return new DefaultSettings();
        }


        public List<PhoneNumber> GetAllCampaignCodeRecords()
        {
            var xpath = string.Format("$ancestorOrSelf/ancestor-or-self::home[position()=1]//phoneManager/phoneNumber[campaignCode/Text()!='']");
            var content = _umbracoHelper.TypedContentAtXPath(xpath);

            if (content == null)
                return null;

            return ConvertIPublishedContentToModel(content);

        }

        public List<PhoneNumber> GetAllReferrerRecords()
        {
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
                overwritePersistingNumber = content.GetPropertyValue<bool>("overwritePersistingNumber"),
                doNotPersist = content.GetPropertyValue<bool>("doNotPersist"),
                persistDurationOverride = content.GetPropertyValue<int>("persistDurationOverride"),
                phoneNumber = content.GetPropertyValue<string>("pphoneNumber"),
                referrer = content.GetPropertyValue<string>("referrer")
            };

            if (string.IsNullOrEmpty(result.phoneNumber)) // not a valid record if there is no telephone number
                return null;

            return result;
        }
    }
}