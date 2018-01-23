namespace Wr.UmbracoPhoneManager.Models
{
    public partial class OutputModel
    {
        public string Id { get; set; }
        public string TelephoneNumber { get; set; }
        public string CampaignCode { get; set; }
        public string AltMarketingCode { get; set; }

        /// <summary>
        /// Checks if the output model is usable
        /// </summary>
        /// <returns>bool</returns>
        public virtual bool IsValid()
        {
            if (!string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(TelephoneNumber)) // needs the minimum of a id and telephone number
                return true;

            return false;
        }

        public bool MatchesFoundRecord(PhoneManagerCampaignDetail campaignDetail)
        {
            if (campaignDetail != null && this != null)
            {
                if (CampaignCode == campaignDetail.CampaignCode && TelephoneNumber == campaignDetail.TelephoneNumber)
                {
                    return true;
                }
            }
            return false;
        }
    }
}