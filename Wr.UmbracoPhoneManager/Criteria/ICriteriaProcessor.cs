using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Criteria
{
    public interface ICriteriaProcessor
    {
        /// <summary>
        /// Return the first matching phone manager phonenumber record
        /// </summary>
        /// <returns>PhoneNumber</returns>
        PhoneNumber GetMatchingRecord();
    }
}