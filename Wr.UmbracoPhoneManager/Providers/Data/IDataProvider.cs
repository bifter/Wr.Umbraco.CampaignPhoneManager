using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers
{
    /// <summary>
    /// Dataprovider to maintain separation between the data access logic and the consuming methods
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Returns the default phone manager setting set in the admin settings panel
        /// </summary>
        /// <returns></returns>
        DefaultSettings GetDefaultSettings();

        /// <summary>
        /// Return the best matching record from the phone manager
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="queryString"></param>
        /// <returns></returns>
        PhoneNumber GetMatchingRecordFromPhoneManager(PhoneNumber requestInfo, NameValueCollection cleansedQuerystrings, string testXmlForUnitTesting = "");

    }
    
}