using System.Collections.Generic;
using System.Linq;
using Wr.UmbracoPhoneManager.Criteria.QueryString;
using Wr.UmbracoPhoneManager.Criteria.Referrer;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Data;

namespace Wr.UmbracoPhoneManager.Criteria
{
    /// <summary>
    /// Run each Criteria against the phone manager phonenumber data provided
    /// </summary>
    public class CriteriaProcessor : ICriteriaProcessor
    {
        private IDataProvider _dataProvider;
        private IQuerystringProvider _querystringProvider;
        private IReferrerProvider _referrerProvider;

        public CriteriaProcessor(IDataProvider dataProvider, IQuerystringProvider querystringProvider, IReferrerProvider referrerProvider)
        {
            _dataProvider = dataProvider;
            _querystringProvider = querystringProvider;
            _referrerProvider = referrerProvider;
        }

        /// <summary>
        /// Return the first matching phone manager phonenumber record 
        /// </summary>
        /// <returns>PhoneNumber</returns>
        public PhoneNumber GetMatchingRecord()
        {
            List<ICriteria> criteriaToCheck = new List<ICriteria>();
            criteriaToCheck.Add(new QuerystringCriteria(_dataProvider, _querystringProvider));
            criteriaToCheck.Add(new ReferrerCriteria(_dataProvider, _referrerProvider));

            List<PhoneNumber> foundRecords = new List<PhoneNumber>();

            foreach (var item in criteriaToCheck)
            {
                foundRecords.AddRange(item.MatchingRecords());
            }

            // work out which record to use
            // - group identical records, then order by group member count. We will want to use the first grouped item, as this will have the most matching criteria
            var priorityList = (from rec in foundRecords
                                group rec by rec into gr // group duplicate records
                                orderby gr.Count() descending // order by the group item count. The most number of duplicate records, the higher the priority, so we will want the first record in the list
                                orderby gr.First().priorityOrder descending // then order by the priority of a member of the group. 'First' is chosen for convenience and also as all groups will have at least one member
                                select gr.First()
                                ).FirstOrDefault(); // get the first item in each group

            return priorityList;
        }
    }
}