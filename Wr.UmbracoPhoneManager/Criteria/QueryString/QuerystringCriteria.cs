using System.Collections.Generic;
using System.Linq;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Data;

namespace Wr.UmbracoPhoneManager.Criteria.QueryString
{
    /// <summary>
    /// For selecting phonenumber records based on the current request querystring values
    /// </summary>
    public class QuerystringCriteria : CriteriaBase, ICriteria
    {

        private readonly IQuerystringProvider _querystringProvider;
        private readonly IDataProvider _dataProvider;

        public QuerystringCriteria(IDataProvider dataProvider, IQuerystringProvider querystringProvider)
        {
            _querystringProvider = querystringProvider;
            _dataProvider = dataProvider;
        }

        /// <summary>
        /// Returns all matching phonenumber records it finds from the data
        /// </summary>
        /// <returns></returns>
        public List<PhoneNumber> MatchingRecords()
        {
            var querystring = _querystringProvider.GetQuerystring();
            if (querystring.Count == 0) // no querystrings present in the current request
                return new List<PhoneNumber>();

            var qsKey = _dataProvider.GetDefaultSettings().DefaultCampaignQuerystringKey;
            var foundQuerystringValue = querystring[qsKey];

            if (string.IsNullOrEmpty(foundQuerystringValue)) // no matching querystring found for the key set in the phone manager settings
                return new List<PhoneNumber>();

            var matchRecords = _dataProvider.GetAllCampaignCodeRecords();

            if (matchRecords != null)
            {
                var hits = matchRecords.Where(x => x.campaignCode == foundQuerystringValue).ToList();
                if (hits.Any())
                    return hits;
            }  

            return new List<PhoneNumber>();
        }
    }
}