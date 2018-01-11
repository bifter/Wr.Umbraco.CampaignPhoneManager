namespace Wr.UmbracoCampaignPhoneManager.Models
{
    /// <summary>
    /// Holder for the multiple properies outputed by the ProcessAllPhoneNumberData 'LogicBox'
    /// </summary>
    public class FinalResultModel
    {
        /// <summary>
        /// Model storing the cookie to be saved. It will be null if a cookie is not to be set
        /// </summary>
        public CookieHolder OutputCookieHolder { get; set; }

        /// <summary>
        /// Model storing the phone data to use. It should always output a result with a phone number, even if it is a placeholder string
        /// </summary>
        public OutputModel OutputModelResult { get; set; }

        /// <summary>
        /// Output of information on which data source the result came from. For debug purposes
        /// </summary>
        public OutputSource OutputResultSource { get; set; }

    }

    /// <summary>
    /// Information on which data source the result came from
    /// </summary>
    public enum OutputSource
    {
        ExisitingCookie,
        FoundRecordFromCriteria,
        DefaultNumberFromAdmin,
        LastResortPlaceholder
    }
}