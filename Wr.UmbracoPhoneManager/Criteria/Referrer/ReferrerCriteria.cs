using System.Collections.Generic;
using System.Linq;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Data;

namespace Wr.UmbracoPhoneManager.Criteria.Referrer
{
    /// <summary>
    /// For selecting phonenumber records based on the current request referrer
    /// </summary>
    public class ReferrerCriteria : CriteriaBase, ICriteria
    {
        private readonly IReferrerProvider _referrerProvider;
        private readonly IDataProvider _dataProvider;

        public ReferrerCriteria(IDataProvider dataProvider, IReferrerProvider referrerProvider)
        {
            _referrerProvider = referrerProvider;
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Returns the first matching phonenumber record it finds from the data
        /// </summary>
        /// <returns></returns>
        public List<PhoneNumber> MatchingRecords()
        {
            var referrerFromRequest = _referrerProvider.GetReferrer();

            if (!string.IsNullOrEmpty(referrerFromRequest))
                referrerFromRequest = referrerFromRequest.ToLower(); // convert to lower case

            var recordsWithReferrer = _dataProvider.GetAllReferrerRecords();

            if (recordsWithReferrer.Any())
            {
                // check for an empty referrer and a phone mananer record with 'none'
                if (string.IsNullOrEmpty(referrerFromRequest)) // no referrer present
                {
                    var noneMatches = recordsWithReferrer.Where(x => x.referrer.Equals("none", System.StringComparison.InvariantCultureIgnoreCase)).ToList();
                    if (noneMatches.Any())
                        return noneMatches;
                }
                else // there is a referrer
                {
                    var matchesContainingReferrer = recordsWithReferrer.Where(x => referrerFromRequest.Contains(x.referrer.ToLower())).ToList();
                    if (matchesContainingReferrer.Any())
                    {
                        // try an exact match
                        var exactMatchesWithReferrer = matchesContainingReferrer.Where(x => x.referrer.Equals(referrerFromRequest, System.StringComparison.InvariantCultureIgnoreCase)).ToList();
                        if (exactMatchesWithReferrer.Any())
                            return exactMatchesWithReferrer;

                        // no exact matches so return the 'contains' records
                        return matchesContainingReferrer;
                    }
                }
            }

            return new List<PhoneNumber>();
        }
    }
}