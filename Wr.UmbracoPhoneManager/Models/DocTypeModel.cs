using System;
using System.Collections.Generic;

namespace Wr.UmbracoPhoneManager.Models
{
    /// <summary>
    /// Vague clone of the PhoneManager doctype
    /// </summary>
    public class DocTypeModel
    {
        public DefaultSettings DefaultSettings { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }

        public DocTypeModel()
        {
            DefaultSettings = new DefaultSettings();
            PhoneNumbers = new List<PhoneNumber>();
        }
    }

    /// <summary>
    /// Vague clone of the PhoneManager default settings fields
    /// </summary>
    public class DefaultSettings
    {
        public string DefaultCampaignQuerystringKey { get; set; }
        public string DefaultPhoneNumber { get; set; }

        private int _defaultPersistDurationInDays;
        public int DefaultPersistDurationInDays
        {
            get
            {
                if (_defaultPersistDurationInDays > 0)
                    return _defaultPersistDurationInDays;

                return 30; // default to 30 days if no default has been set in the phone manager settings
            }
            set
            {
                _defaultPersistDurationInDays = value;
            }
        }
    }

    /// <summary>
    /// Vague clone of the PhoneNumber Umbraco doctype
    /// </summary>
    public class PhoneNumber
    {
        public string Id { get; set; }
        public bool doNotPersistAcrossVisits { get; set; }
        public string phoneNumber { get; set; }
        public int persistDurationOverride { get; set; }
        public string referrer { get; set; }
        public string campaignCode { get; set; }
        public string entryPage { get; set; }
        public bool overwritePersistingItem { get; set; }
        public string altMarketingCode { get; set; }
        public int priorityOrder { get; set; }
        public string useAltCampaignQuerystringKey { get; set; }
        public DateTime lastUsedDate { get; set; }

        /// <summary>
        /// Checks if the PhoneNumber data is usable
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValid()
        {           
            if (!string.IsNullOrEmpty(phoneNumber)) // needs a minimum of a phone number
                return true;

            return false;
        }

        /// <summary>
        /// Checks if the PhoneNumber data is usable, and wants to be persisted (i.e. in a cookie)
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValidToSaveAsCookie()
        {
            if (IsValid())
                return !doNotPersistAcrossVisits;

            return false;
        }
    }
}