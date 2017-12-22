using System;
using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// Dataprovider to maintain separation between the data access logic and the consuming methods
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Return the interface to discover all associated Criteria classes
        /// </summary>
        /// <returns></returns>
        Type InterfaceSelector();

        /// <summary>
        /// Returns the default phone manager setting set in the admin settings panel
        /// </summary>
        /// <returns></returns>
        CampaignPhoneManagerModel GetDefaultSettings();

        /// <summary>
        /// Return the best matching record from the phone manager
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        List<CampaignDetail> GetMatchingRecordsFromPhoneManager();// CampaignDetail requestInfo, NameValueCollection cleansedQuerystrings);

    }
    
}