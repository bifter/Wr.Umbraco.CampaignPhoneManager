namespace Umbraco.Web
{
    using Umbraco.Core.Models;
    using Wr.UmbracoPhoneManager.Models;

    public static class IPublishedContentExtensions
    {
        public static string _phoneNumber { get; set; }
        public static string _campaignCode { get; set; }
        public static string _altMarketingCode { get; set; }

        /// <summary>
        /// Extends the IPublishedContent model to include the phone manager OutputModel onto all IPublishedContent objects, so it is accessible in the templates
        /// </summary>
        /// <param name="content"></param>
        /// <param name="model"></param>
        /// <returns>OutputModel. Phone manager phonenumber data</returns>
        public static OutputModel CampaignPhoneManager(this IPublishedContent content, OutputModel model = null)
        {
            if (model != null)
            {
                _phoneNumber = model.PhoneNumber;
                _campaignCode = model.CampaignCode;
                _altMarketingCode = model.AltMarketingCode;
            }

            return new OutputModel()
            {
                PhoneNumber = _phoneNumber,
                CampaignCode = _campaignCode,
                AltMarketingCode = _altMarketingCode
            };
        }

    }
}