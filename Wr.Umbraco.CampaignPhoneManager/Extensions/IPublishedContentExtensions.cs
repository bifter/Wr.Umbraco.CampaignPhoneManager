namespace Umbraco.Web
{
    using Umbraco.Core.Models;
    using Wr.Umbraco.CampaignPhoneManager.Models;
    using Wr.Umbraco.CampaignPhoneManager.Providers;

    public static class IPublishedContentExtensions
    {
        /// <summary>
        /// Extends the IPublishedContent model to include the phone manager OutputModel onto all IPublishedContent objects, so it is accessible in the templates
        /// </summary>
        /// <param name="content"></param>
        /// <returns>OutputModel. Campaign Phone Manager data</returns>
        public static OutputModel CampaignPhoneManager(this IPublishedContent content)
        {
            return new SessionProvider()?.GetSession() ?? new OutputModel();
        }
    }
}