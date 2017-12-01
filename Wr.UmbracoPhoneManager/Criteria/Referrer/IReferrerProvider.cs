namespace Wr.UmbracoPhoneManager.Criteria.Referrer
{
    public interface IReferrerProvider
    {
        /// <summary>
        /// Return the current referrer domain, not including local referrals
        /// </summary>
        /// <returns>Domain string</returns>
        string GetReferrer();
    }
}