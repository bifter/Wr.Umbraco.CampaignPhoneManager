namespace Umbraco.Web
{
    using Umbraco.Core.Models;
    using Wr.Umbraco.CampaignPhoneManager.Models;
    using Wr.Umbraco.CampaignPhoneManager.Providers;

    public static class IPublishedContentExtensions
    {
        public static string _telephoneNumber { get; set; }
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
                return model;

            return new SessionProvider()?.GetSession() ?? new OutputModel();
            /*
            if (model != null)
            {
                _telephoneNumber = model.TelephoneNumber;
                _campaignCode = model.CampaignCode;
                _altMarketingCode = model.AltMarketingCode;
            }

            return new OutputModel()
            {
                TelephoneNumber = _telephoneNumber,
                CampaignCode = _campaignCode,
                AltMarketingCode = _altMarketingCode
            };*/
        }

    }
}