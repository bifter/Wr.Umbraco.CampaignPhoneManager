using System;
using Wr.Umbraco.CampaignPhoneManager.Models;
using Wr.Umbraco.CampaignPhoneManager.Providers;

namespace Wr.Umbraco.CampaignPhoneManager
{
    public class CampaignPhoneManager
    {
        private CookieProvider _cookieProvider;
        private readonly IDataProvider _dataProvider;
        private readonly QueryStringProvider _querystringProvider;
        private readonly ReferrerProvider _referrerProvider;
        private ISessionProvider _sessionProvider;
        private IUmbracoProvider _umbracoProvider;

        private string _querystring { get; set; }
        private string _referrer { get; set; }

        /// <summary>
        /// Processes the current request to find a relevant phone number to output 
        /// </summary>
        public CampaignPhoneManager()
        {
            _cookieProvider = new CookieProvider(new HttpContextCookieProviderSource());
            _dataProvider = new XPathDataProvider(new XPathDataProviderSource_UmbracoTypedContent());
            _querystringProvider = new QueryStringProvider(new HttpContextQueryStringProviderSource());
            _referrerProvider = new ReferrerProvider(new HttpContextReferrerProviderSource());
            _sessionProvider = new SessionProvider();
            _umbracoProvider = new UmbracoProvider();
        }

        public CampaignPhoneManager(CookieProvider cookieProvider, IDataProvider dataProvider, QueryStringProvider querystringProvider, ReferrerProvider referrerProvider, ISessionProvider sessionProvider, IUmbracoProvider umbracoProvider)
        {
            _cookieProvider = cookieProvider;
            _dataProvider = dataProvider;
            _querystringProvider = querystringProvider;
            _referrerProvider = referrerProvider;
            _sessionProvider = sessionProvider;
            _umbracoProvider = umbracoProvider;
        }

        // Methods
        public OutputModel ProcessRequest()
        {
            // check for active phone manager session
            var returnResult = _sessionProvider.GetSession();
            if (returnResult?.IsValid() ?? false)
                return returnResult; // valid phone manager session exists so return it. No need to continue with next steps

            // try and find a relevant phone number from the phone manager records based on the criteria's. Null if none found
            var foundRecord = FindMatchingPhoneManagerPhoneNumberUsingGatheredRequestInfo();

            // load exisiting cookie if it exisits, null if not
            var exisitingCookie = _cookieProvider.GetCookie();

            // pass all available data into the method which decides which data to use
            var logicBox = ProcessAllPotentialCandidatePhoneNumbers(exisitingCookie, foundRecord);

            if (logicBox.OutputCookieHolder != null)
                _cookieProvider.SetCookie(logicBox.OutputCookieHolder);

            // save session
            _sessionProvider.SetSession(logicBox.OutputModelResult);

            return logicBox.OutputModelResult;          
        }

        /// <summary>
        /// Try and find matching phone manager phone number records using request info i.e. referrer, valid querystring, entry page
        /// </summary>
        /// <returns>A found PhoneNumber</returns>
        private CampaignDetail FindMatchingPhoneManagerPhoneNumberUsingGatheredRequestInfo()
        {
            var requestInfo = new CampaignDetail()
            {
                referrer = _referrerProvider.GetReferrerOrNone(),
                entryPage = _umbracoProvider.GetCurrentPageId()
            };
            
            var foundNumber = _dataProvider.GetMatchingRecordFromPhoneManager(requestInfo, _querystringProvider.GetCleansedQueryStrings());

            return null;
        }

        /// <summary>
        /// Pass all available available PhoneNumber records to decide finally which PhoneNumber to use
        /// </summary>
        private FinalResultModel ProcessAllPotentialCandidatePhoneNumbers(CookieHolder exisitingCookie, CampaignDetail foundRecord)
        {
            FinalResultModel result = new FinalResultModel();

            // check if there is an exisiting cookie we can use, and check if we want to use it
            if (exisitingCookie?.Model?.IsValid() ?? false) // we have a valid exisiting cookie
            {
                bool useExisitingCookieForSession = true; // let's assume we will want to use the existing cookie

                if ((foundRecord?.IsValidToSaveAsCookie() ?? false) && (foundRecord?.overwritePersistingItem ?? false)) // foundRecordFromCriteria needs persisting and it should override any exisiting cookie
                {
                    useExisitingCookieForSession = false; // don't use the cookie as the _foundRecordFromCriteria has requested to overwrite any exisiting cookie
                }

                if (useExisitingCookieForSession) // continue using the exisiting cookie - no need to save a newc ookie
                {
                    result.OutputModelResult = exisitingCookie.Model; // use the cookie data
                    result.OutputResultSource = OutputSource.ExisitingCookie;
                    return result;
                }
            }

            // check if we have a valid _foundRecordFromCriteria that we can use
            if (foundRecord?.IsValid() ?? false) // we have a valid foundRecordFromCriteria object that we can use
            {
                result.OutputModelResult = new OutputModel()
                {
                    PhoneNumber = foundRecord.phoneNumber,
                    CampaignCode = foundRecord.campaignCode,
                    AltMarketingCode = foundRecord.altMarketingCode
                };

                if (foundRecord.IsValidToSaveAsCookie()) // it is requesting to be persisted via a cookie
                {
                    result.OutputCookieHolder = new CookieHolder()
                    {
                        Expires = DateTime.Today.AddDays((foundRecord.persistDurationOverride > 0) ? foundRecord.persistDurationOverride : _dataProvider.GetDefaultSettings()?.DefaultPersistDurationInDays ?? 0), // persist duration in days - if foundRecord has persistDurationOverride set then use that, otherwise use the default admin setting
                        Model = result.OutputModelResult
                    };
                }
                result.OutputResultSource = OutputSource.FoundRecordFromCriteria;
                return result;
            }

            // check if a default phone number has been set in the admin system
            if (!string.IsNullOrEmpty(_dataProvider.GetDefaultSettings()?.DefaultPhoneNumber ?? string.Empty))
            {
                result.OutputModelResult = new OutputModel()
                {
                    PhoneNumber = _dataProvider.GetDefaultSettings()?.DefaultPhoneNumber
                };
                result.OutputResultSource = OutputSource.DefaultNumberFromAdmin;
                return result;
            }

            // as a last resort, output placeholder phone number
            result.OutputModelResult = new OutputModel()
            {
                PhoneNumber = "XXX XXX XXXX"
            };

            result.OutputResultSource = OutputSource.LastResortPlaceholder;
            return result;
        }
    }
}