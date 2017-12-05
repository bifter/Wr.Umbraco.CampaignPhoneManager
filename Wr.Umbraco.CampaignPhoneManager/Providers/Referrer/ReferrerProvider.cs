namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class ReferrerProvider
    {
        IReferrerProviderSource _referrerProviderSource;

        public ReferrerProvider()
        {
            _referrerProviderSource = new HttpContextReferrerProviderSource();
        }

        public ReferrerProvider(IReferrerProviderSource referrerProviderSource)
        {
            _referrerProviderSource = referrerProviderSource;
        }

        public string GetReferrer()
        {
            return _referrerProviderSource.GetReferrer();
        }

        public string GetReferrerOrNone()
        {
            string referrer = GetReferrer();
            return (!string.IsNullOrEmpty(referrer)) ? referrer : "none";
        }
    }
}