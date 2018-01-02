﻿using System.Collections.Generic;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers.Storage;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria
{
    public class QueryStringCriteria : ICampaignPhoneManagerCriteria
    {
        private IRepository _repository;
        private readonly CriteriaParameterHolder _criteriaParameters;

        public QueryStringCriteria(CriteriaParameterHolder criteriaParameters)
        {
            _criteriaParameters = criteriaParameters;
            _repository = new XPathRepository();
        }

        public QueryStringCriteria(CriteriaParameterHolder criteriaParameters, IRepository repository)
        {
            _criteriaParameters = criteriaParameters;
            _repository = repository;
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            var cleansedQueryStrings = _criteriaParameters.CleansedQueryStrings;

            if (cleansedQueryStrings.Count > 0)
            {
                return _repository.GetMatchingCriteriaRecords_QueryString(cleansedQueryStrings);
            }

            return new List<CampaignDetail>();
        }
    }
}