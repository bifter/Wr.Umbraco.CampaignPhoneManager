using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Wr.Umbraco.CampaignPhoneManager.Models
{
    /// <summary>
    /// Clone of the campaignPhoneManager doctype
    /// </summary>

    [Serializable()]
    [XmlRoot(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager)]
    public partial class CampaignPhoneManagerModel
    {
        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultCampaignQueryStringKey)]
        [DefaultValue("")]
        public string DefaultCampaignQueryStringKey { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultPhoneNumber)]
        [DefaultValue("")]
        public string DefaultPhoneNumber { get; set; }

        [XmlIgnore]
        private int _defaultPersistDurationInDays;
        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultPersistDurationInDays)]
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

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.GlobalDisableOverwritePersistingItems)]
        [DefaultValue(false)]
        public bool GlobalDisableOverwritePersistingItems { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignDetail)]
        public List<CampaignDetail> CampaignDetail { get; set; }

        public CampaignPhoneManagerModel()
        {
            CampaignDetail = new List<CampaignDetail>();
        }
    }

    /// <summary>
    /// Clone of the campaignDetail Umbraco doctype
    /// </summary>
    [Serializable()]
    [XmlRoot(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignDetail)]
    public partial class CampaignDetail
    {
        [XmlAttribute(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.Id)]
        [DefaultValue("")]
        public string Id { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.DoNotPersistAcrossVisits)]
        [DefaultValue(false)]
        public bool DoNotPersistAcrossVisits { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.TelephoneNumber)]
        [DefaultValue("")]
        public string TelephoneNumber { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.PersistDurationOverride)]
        [DefaultValue(0)]
        public int PersistDurationOverride { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.Referrer)]
        [DefaultValue("")]
        public string Referrer { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.CampaignCode)]
        public string CampaignCode { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage)]
        [DefaultValue("")]
        public string EntryPage { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.OverwritePersistingItem)]
        [DefaultValue(false)]
        public bool OverwritePersistingItem { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.AltMarketingCode)]
        [DefaultValue("")]
        public string AltMarketingCode { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.PriorityOrder)]
        [DefaultValue(0)]
        public int PriorityOrder { get; set; }

        [XmlElement(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey)]
        [DefaultValue("")]
        public string UseAltCampaignQueryStringKey { get; set; }

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