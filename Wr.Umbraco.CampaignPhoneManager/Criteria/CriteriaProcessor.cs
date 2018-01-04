using System.Collections.Generic;
using System.Linq;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class CriteriaProcessor
    {
        private readonly IRepository _repository;
        private readonly CriteriaParameterHolder _criteriaParameters;

        public CriteriaProcessor(CriteriaParameterHolder criteriaParameters, IRepository repository) // override default data provider
        {
            _repository = repository;
            _criteriaParameters = criteriaParameters;
        }

        public CampaignDetail GetMatchingRecordFromPhoneManager()
        {
            List<CampaignDetail> foundRecords = new List<CampaignDetail>();

            foreach (var item in AvailableCriteria.GetCriteriaList())
            {
                foundRecords.AddRange(item.GetMatchingRecordsFromPhoneManager(_criteriaParameters, _repository));
            }

            // work out which record to use
            // - group identical records (using the Id property), then order by group member count. We will want to use the first grouped item, as this will have the most matching criteria
            var bestMatch = (from rec in foundRecords
                                group rec by rec.Id into gr // group duplicate records
                                orderby gr.Count() descending // order by the group item count. The most number of duplicate records, the higher the priority, so we will want the first record in the list
                                orderby gr.First().PriorityOrder descending // then order by the priority of a member of the group. 'First' is chosen for convenience and also as all groups will have at least one member
                                select gr.First() // get the first item in each group
                                ).FirstOrDefault(); // return the first group

            return bestMatch;
        }
    }
}