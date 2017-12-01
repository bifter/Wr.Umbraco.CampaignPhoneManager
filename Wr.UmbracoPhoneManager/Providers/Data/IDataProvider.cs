using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers.Data
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
        /// Returns all matching phone manger records with a Campaign code set
        /// </summary>
        /// <returns></returns>
        List<PhoneNumber> GetAllCampaignCodeRecords();

        /// <summary>
        /// Returns all the phone manager records which have referrer criteria
        /// </summary>
        /// <returns>List of matching records</returns>
        List<PhoneNumber> GetAllReferrerRecords();
    }
    
}