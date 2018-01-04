using System.Collections.Specialized;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// A separate Provider Source interface to allow easy unit testing
    /// </summary>
    public interface IQueryStringImplementation
    {
        /// <summary>
        /// A collection of querystring keys and values
        /// </summary>
        /// <returns></returns>
        NameValueCollection GetQueryStrings();
    }
}