using static Wr.Umbraco.CampaignPhoneManager.Helpers.ENums;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class ReferrerProvider
    {
        IReferrerImplementation _referrerImplementation;

        public ReferrerProvider()
        {
            _referrerImplementation = new HttpContextReferrerImplementation();
        }

        public ReferrerProvider(IReferrerImplementation referrerImplementation)
        {
            _referrerImplementation = referrerImplementation;
        }

        public string GetReferrer()
        {
            return _referrerImplementation.GetReferrer().ToSafeString(ProviderType.Referrer);
        }

        public string GetReferrerOrNone()
        {
            string referrer = GetReferrer();
            return (!string.IsNullOrEmpty(referrer)) ? referrer : "none";
        }
    }
}