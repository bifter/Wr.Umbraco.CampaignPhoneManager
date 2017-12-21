using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager.Criteria.EntryPage
{
    public class EntryPageCriteria : ICampaignPhoneManagerCriteria
    {
        private IDataProvider _iDataProvider;

        public EntryPageCriteria()
        {
            _iDataProvider = new EntryPageCriteria_DataSource_XPath();
        }

        public List<CampaignDetail> GetMatchingRecordsFromPhoneManager()
        {
            if (!string.IsNullOrEmpty(requestInfoNotIncludingQueryStrings.EntryPage))
            {
               
            }

            return _iDataProvider.GetMatchingRecordsFromPhoneManager();
        }
    }
} 