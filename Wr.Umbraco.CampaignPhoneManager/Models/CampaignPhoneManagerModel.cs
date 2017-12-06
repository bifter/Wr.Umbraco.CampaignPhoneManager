using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Wr.Umbraco.CampaignPhoneManager.Models
{
    /// <summary>
    /// Clone of the PhoneManager doctype
    /// </summary>

    [Serializable()]
    [XmlRoot("campaignPhoneManager")]
    public class CampaignPhoneManagerModel
    {
        [XmlElement("defaultCampaignQueryStringKey")]
        public string DefaultCampaignQueryStringKey { get; set; }

        [XmlElement("defaultPhoneNumber")]
        public string DefaultPhoneNumber { get; set; }

        [XmlIgnore]
        private int _defaultPersistDurationInDays;
        [XmlElement("defaultPersistDurationInDays")]
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

        [XmlArray("campaignDetail")]
        public List<CampaignDetail> CampaignDetail { get; set; }

        public CampaignPhoneManagerModel()
        {
            CampaignDetail = new List<CampaignDetail>();
        }
    }

    /// <summary>
    /// Clone of the PhoneManager default settings fields
    /// </summary>
    /*public class DefaultSettings
    {
        public string DefaultCampaignQueryStringKey { get; set; }
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
    }*/

    /// <summary>
    /// Clone of the PhoneNumber Umbraco doctype
    /// </summary>
    [Serializable()]
    [XmlRoot("campaignDetail", IsNullable = true)]
    public class CampaignDetail
    {
        [XmlAttribute("id")]
        [DefaultValue("")]
        public string Id { get; set; }

        [XmlElement("doNotPersistAcrossVisits")]
        [DefaultValue(false)]
        public bool DoNotPersistAcrossVisits { get; set; }

        [XmlElement("phoneNumber")]
        [DefaultValue("")]
        public string PhoneNumber { get; set; }

        [XmlElement("persistDurationOverride")]
        [DefaultValue(0)]
        public int PersistDurationOverride { get; set; }

        [XmlElement("referrer")]
        [DefaultValue("")]
        public string Referrer { get; set; }

        [XmlElement("campaignCode")]
        public string CampaignCode { get; set; }

        [XmlElement("entryPage")]
        [DefaultValue("")]
        public string EntryPage { get; set; }

        [XmlElement("overwritePersistingItem")]
        [DefaultValue(false)]
        public bool OverwritePersistingItem { get; set; }

        [XmlElement("altMarketingCode")]
        [DefaultValue("")]
        public string AltMarketingCode { get; set; }

        [XmlElement("priorityOrder")]
        [DefaultValue(0)]
        public int PriorityOrder { get; set; }

        [XmlElement("useAltCampaignQueryStringKey")]
        [DefaultValue("")]
        public string UseAltCampaignQueryStringKey { get; set; }

        /// <summary>
        /// Checks if the PhoneNumber data is usable
        /// </summary>
        /// <returns>bool</returns>
        public bool IsValid()
        {           
            if (!string.IsNullOrEmpty(PhoneNumber)) // needs a minimum of a phone number
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
                return !DoNotPersistAcrossVisits;

            return false;
        }
    }
}