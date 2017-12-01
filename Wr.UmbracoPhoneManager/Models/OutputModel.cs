using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wr.UmbracoPhoneManager.Models
{
    public class OutputModel
    {
        public string PhoneNumber { get; set; }
        public string CampaignCode { get; set; }
        public string AltMarketingCode { get; set; }

        /// <summary>
        /// Checks if the output model is usable
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValid()
        {
            if (!string.IsNullOrEmpty(PhoneNumber)) // needs the minimum of a phone number
                return true;

            return false;
        }
    }
}