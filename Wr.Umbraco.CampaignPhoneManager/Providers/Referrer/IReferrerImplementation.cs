namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    /// <summary>
    /// A separate Provider Source interface to allow easy unit testing
    /// </summary>
    public interface IReferrerImplementation
    {
        /// <summary>
        /// Return the current referrer domain, not including local referrals
        /// </summary>
        /// <returns>Domain string</returns>
        string GetReferrer();
    }
}