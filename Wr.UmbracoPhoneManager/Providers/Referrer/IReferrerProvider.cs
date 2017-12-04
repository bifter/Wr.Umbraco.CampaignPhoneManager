namespace Wr.UmbracoPhoneManager.Providers
{
    public interface IReferrerProvider
    {
        /// <summary>
        /// Return the current referrer domain, not including local referrals
        /// </summary>
        /// <returns>Domain string</returns>
        string GetReferrer();

        /// <summary>
        /// Return the current referrer domain, if not present then return 'none'
        /// </summary>
        /// <returns>Domain string or 'none'</returns>
        string GetReferrerOrNone();

    }
}