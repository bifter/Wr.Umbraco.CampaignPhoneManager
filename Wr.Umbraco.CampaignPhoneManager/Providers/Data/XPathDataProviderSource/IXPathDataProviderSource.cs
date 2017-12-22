using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// A separate Provider Source interface to allow easy unit testing
    /// </summary>
    public interface IXPathDataProviderSource
    {
        CampaignPhoneManagerModel GetDefaultSettings();

        /// <summary>
        /// Get default settings from the source data store
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        CampaignPhoneManagerModel LoadDefaultSettings(string xpath);

        /// <summary>
        /// Get data from the source data store
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        List<CampaignDetail> GetDataByXPath(string xpath);

    }
}