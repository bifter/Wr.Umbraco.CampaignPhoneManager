namespace Wr.Umbraco.CampaignPhoneManager.Models
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
            if (!string.IsNullOrEmpty(TelephoneNumber)) // needs the minimum of a phone number
                return true;

            return false;
        }
    }
}