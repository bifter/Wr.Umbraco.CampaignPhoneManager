using System;
using Wr.UmbracoPhoneManager.Criteria;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers;

namespace Wr.UmbracoPhoneManager
{
    public class PhoneManager
    {
        private ICookieProvider _cookieProvider;
        private readonly IPhoneManagerService _phoneManagerService;
        private readonly QueryStringProvider _querystringProvider;
        private readonly ReferrerProvider _referrerProvider;
        private ISessionProvider _sessionProvider;
        private IUmbracoProvider _umbracoProvider;

        private string _querystring { get; set; }
        private string _referrer { get; set; }

        /// <summary>
        /// Processes the current request to find a relevant phone number to output 
        /// </summary>
        public PhoneManager()
        {
            _phoneManagerService = Umbraco.Web.Composing.Current.Factory.GetInstanceFor<IPhoneManagerService, PhoneManagerService>();

            // default providers/repository
            _cookieProvider = new CookieProvider(new HttpContextCookieImplementation());
            _querystringProvider = new QueryStringProvider(new HttpContextQueryStringImplementation());
            _referrerProvider = new ReferrerProvider(new HttpContextReferrerImplementation());
            _sessionProvider = new SessionProvider();
            _umbracoProvider = new UmbracoProvider();
        }

        public PhoneManager(ICookieProvider cookieProvider, IPhoneManagerService phoneManagerService, QueryStringProvider querystringProvider, ReferrerProvider referrerProvider, ISessionProvider sessionProvider, IUmbracoProvider umbracoProvider)
        {
            _cookieProvider = cookieProvider;
            _phoneManagerService = phoneManagerService;
            _querystringProvider = querystringProvider;
            _referrerProvider = referrerProvider;
            _sessionProvider = sessionProvider;
            _umbracoProvider = umbracoProvider;
        }

        /// <summary>
        /// Taking the current request, find a relevant Phone Manager record to display to the user
        /// </summary>
        /// <returns></returns>
        public OutputModel ProcessRequest()
        {
            // check for active phone manager session
            var returnResult = _sessionProvider.GetSession();
            if (returnResult?.IsValid() ?? false)
                return returnResult; // valid phone manager session exists so return it. No need to continue with next steps

            // load exisiting cookie if it exisits, null if not
            var exisitingCookie = _cookieProvider.GetCookie();

            // if there is a valid cookie and the GlobalDisableOverwritePersistingItems admin setting is set then we don't need to look  For matching records in the system, we can just use the cookie info.
            bool lookForMatchingRecord = true;
            if ((exisitingCookie?.Model?.IsValid() ?? false) && (_phoneManagerService.GetDefaultSettings()?.GlobalDisableOverwritePersistingItems ?? false))
            {
                lookForMatchingRecord = false;
            }

            // try and find a relevant phone number from the phone manager records based on the criteria's. Null if none found
            var foundRecord = (lookForMatchingRecord) ? FindMatchingPhoneManagerPhoneNumberUsingGatheredRequestInfo() : null;

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
        private PhoneManagerCampaignDetail FindMatchingPhoneManagerPhoneNumberUsingGatheredRequestInfo()
        {
            var criteriaParameters = new CriteriaParameterHolder()
            {
                CleansedQueryStrings = _querystringProvider.GetQueryStrings(),
                RequestInfo_NotIncludingQueryStrings =
                    new PhoneManagerCampaignDetail()
                    {
                        EntryPage = _umbracoProvider.GetCurrentPageId(),
                        Referrer = _referrerProvider.GetReferrerOrNone(),
                    }
            };

            return new CriteriaProcessor(criteriaParameters, _phoneManagerService).GetMatchingRecordFromPhoneManager();
        }

        /// <summary>
        /// Pass all available available PhoneNumber records to decide finally which PhoneNumber to use
        /// </summary>
        internal FinalResultModel ProcessAllPotentialCandidatePhoneNumbers(CookieHolder exisitingCookie, PhoneManagerCampaignDetail foundRecord)
        {
            FinalResultModel result = new FinalResultModel();

            // check if there is an exisiting cookie we can use, and check if we want to use it
            if (exisitingCookie?.Model?.IsValid() ?? false) // we have a valid exisiting cookie
            {
                bool useExisitingCookieForSession = true; // let's assume we will want to use the existing cookie

                if ((foundRecord?.IsValidToSaveAsCookie() ?? false) && (foundRecord?.OverwritePersistingItem ?? false) && !exisitingCookie.Model.MatchesFoundRecord(foundRecord)) // foundRecordFromCriteria needs persisting and it should override any exisiting cookie
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
                    Id = foundRecord.Id,
                    TelephoneNumber = foundRecord.TelephoneNumber,
                    CampaignCode = foundRecord.CampaignCode,
                    AltMarketingCode = foundRecord.AltMarketingCode
                };

                if (foundRecord.IsValidToSaveAsCookie()) // it is requesting to be persisted via a cookie
                {
                    result.OutputCookieHolder = new CookieHolder()
                    {
                        Expires = DateTime.Today.AddDays((foundRecord.PersistDurationOverride > 0) ? foundRecord.PersistDurationOverride : _phoneManagerService.GetDefaultSettings()?.DefaultPersistDurationInDays ?? 0), // persist duration in days - if foundRecord has persistDurationOverride set then use that, otherwise use the default admin setting
                        Model = result.OutputModelResult
                    };
                }
                result.OutputResultSource = OutputSource.FoundRecordFromCriteria;
                return result;
            }

            // as a last resort, output placeholder phone number
            result.OutputModelResult = new OutputModel()
            {
                Id = "P",
                TelephoneNumber = AppConstants.LastResortPhoneNumberPlaceholder
            };

            result.OutputResultSource = OutputSource.LastResortPlaceholder;
            return result;
        }
    }
}