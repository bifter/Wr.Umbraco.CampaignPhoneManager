namespace Umbraco.Web
{
    using Umbraco.Core.Models.PublishedContent;
    using Wr.UmbracoPhoneManager.Models;
    using Wr.UmbracoPhoneManager.Providers;

    public static class IPublishedContentExtensions
    {
        /// <summary>
        /// Extends the IPublishedContent model to include the phone manager OutputModel onto all IPublishedContent objects, so it is accessible in the templates
        /// </summary>
        /// <param name="content"></param>
        /// <returns>OutputModel. Phone Manager data</returns>
        public static OutputModel PhoneManager(this IPublishedContent content)
        {
            return new SessionProvider()?.GetSession() ?? new OutputModel();
        }
    }
}