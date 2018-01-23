using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Wr.UmbracoPhoneManager.Models
{
    /// <summary>
    /// Must match the campaignPhoneManager umbraco doctype
    /// </summary>

    [Serializable()]
    [XmlRoot(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManager)]
    public partial class PhoneManagerModel
    {
        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.DefaultCampaignQueryStringKey)]
        [DefaultValue("")]
        public string DefaultCampaignQueryStringKey { get; set; }

        [XmlIgnore]
        private int _defaultPersistDurationInDays;
        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.DefaultPersistDurationInDays)]
        [DefaultValue(0)]
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

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.GlobalDisableOverwritePersistingItems)]
        [DefaultValue(false)]
        public bool GlobalDisableOverwritePersistingItems { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManagerCampaignDetail)]
        public List<PhoneManagerCampaignDetail> CampaignDetail { get; set; }

        public PhoneManagerModel()
        {
            CampaignDetail = new List<PhoneManagerCampaignDetail>();
        }
    }

    /// <summary>
    /// Clone of the campaignDetail Umbraco doctype
    /// </summary>
    [Serializable()]
    [XmlRoot(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManagerCampaignDetail)]
    public partial class PhoneManagerCampaignDetail
    {
        [XmlAttribute(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.Id)]
        [DefaultValue("")]
        public string Id { get; set; }

        [XmlAttribute(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.NodeName)]
        [DefaultValue("")]
        public string NodeName { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.DoNotPersistAcrossVisits)]
        [DefaultValue(false)]
        public bool DoNotPersistAcrossVisits { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.TelephoneNumber)]
        [DefaultValue("")]
        public string TelephoneNumber { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.PersistDurationOverride)]
        [DefaultValue(0)]
        public int PersistDurationOverride { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.Referrer)]
        [DefaultValue("")]
        public string Referrer { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.CampaignCode)]
        public string CampaignCode { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.EntryPage)]
        [DefaultValue("")]
        public string EntryPage { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.OverwritePersistingItem)]
        [DefaultValue(false)]
        public bool OverwritePersistingItem { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.AltMarketingCode)]
        [DefaultValue("")]
        public string AltMarketingCode { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.PriorityOrder)]
        [DefaultValue(0)]
        public int PriorityOrder { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.UseAltCampaignQueryStringKey)]
        [DefaultValue("")]
        public string UseAltCampaignQueryStringKey { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.PhoneManagerModel_PhoneManagerCampaignDetail.IsDefault)]
        [DefaultValue(false)]
        public bool IsDefault { get; set; }

        /// <summary>
        /// Checks if the PhoneNumber data is usable
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsValid()
        {           
            if (!string.IsNullOrEmpty(TelephoneNumber)) // needs a minimum of a phone number
                return true;

            return false;
        }

        /// <summary>
        /// Checks if the PhoneNumber data is usable, and wants to be persisted (i.e. in a cookie)
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsValidToSaveAsCookie()
        {
            if (IsValid())
                return !DoNotPersistAcrossVisits;

            return false;
        }
    }
}