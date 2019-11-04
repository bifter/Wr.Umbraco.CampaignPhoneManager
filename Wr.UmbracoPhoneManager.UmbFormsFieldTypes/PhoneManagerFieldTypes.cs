using System;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Enums;

namespace Wr.UmbracoPhoneManager.UmbFormsFieldTypes
{
    /// <summary>
    /// Umbraco Forms field type for the Phone Manager Telephone Number
    /// </summary>
    public class PhoneManagerTelephone : FieldType
    {
        public PhoneManagerTelephone()
        {
            this.Id = new Guid("917c5905-bc2f-4a72-9fe2-24f296adec19");
            this.Name = "PhoneManager - Telephone number";
            this.Description = "A hidden field with the PhoneManager Telephone number";
            this.Icon = "icon-phone";
            this.DataType = FieldDataType.String;
            this.SortOrder = 10;
            this.FieldTypeViewName = "FieldType.PhoneManagerTelephone.cshtml";
            this.HideField = true;
            this.HideLabel = true;
        }
    }

    /// <summary>
    /// Umbraco Forms field type for the Phone Manager Campaign Code
    /// </summary>
    public class PhoneManagerCampaignCode : FieldType
    {
        public PhoneManagerCampaignCode()
        {
            this.Id = new Guid("cf8b128b-eadd-4c57-93bc-a42c24ad4005");
            this.Name = "PhoneManager - Campaign code";
            this.Description = "A hidden field with the PhoneManager Campaign code";
            this.Icon = "icon-phone";
            this.DataType = FieldDataType.String;
            this.SortOrder = 10;
            this.FieldTypeViewName = "FieldType.PhoneManagerCampaignCode.cshtml";
            this.HideField = true;
            this.HideLabel = true;
        }
    }

    /// <summary>
    /// Umbraco Forms field type for the Phone Manager Alt Marketing Code
    /// </summary>
    public class PhoneManagerAltMarketingCode : FieldType
    {
        public PhoneManagerAltMarketingCode()
        {
            this.Id = new Guid("2aa1ed86-12cd-457f-b65d-b6792a384aa6");
            this.Name = "PhoneManager - Alt Marketing code";
            this.Description = "A hidden field with the PhoneManager Alt Marketing code";
            this.Icon = "icon-phone";
            this.DataType = FieldDataType.String;
            this.SortOrder = 10;
            this.FieldTypeViewName = "FieldType.PhoneManagerAltMarketingCode.cshtml";
            this.HideField = true;
            this.HideLabel = true;
        }
    }
}