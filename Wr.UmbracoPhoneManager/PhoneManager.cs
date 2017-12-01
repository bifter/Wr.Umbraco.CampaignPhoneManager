using Umbraco.Web;
using Wr.UmbracoPhoneManager.Criteria;
using Wr.UmbracoPhoneManager.Criteria.QueryString;
using Wr.UmbracoPhoneManager.Criteria.Referrer;
using Wr.UmbracoPhoneManager.Models;
using Wr.UmbracoPhoneManager.Providers.Cookie;
using Wr.UmbracoPhoneManager.Providers.Data;
using Wr.UmbracoPhoneManager.Providers.Session;

namespace Wr.UmbracoPhoneManager
{
    public class PhoneManager
    {
        private readonly IQuerystringProvider _querystringProvider;
        private readonly IReferrerProvider _referrerProvider;
        private readonly IDataProvider _dataProvider;
        private ISessionProvider _sessionProvider;
        private ICookieProvider _cookieProvider;

        private UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

        /// <summary>
        /// Processes the current request to find a relevant phone number to output 
        /// </summary>
        public PhoneManager()
        {
            _querystringProvider = new HttpContextQuerystringProvider();
            _referrerProvider = new HttpContextReferrerProvider();
            _dataProvider = new XPathDataProvider(umbracoHelper);
            _sessionProvider = new SessionProvider();
            _cookieProvider = new CookieProvider();
        }

        public PhoneManager(IDataProvider dataProvider, ISessionProvider sessionProvider, ICookieProvider cookieProvider, IQuerystringProvider querystringProvider, IReferrerProvider referrerProvider)
        {
            _dataProvider = dataProvider;
            _sessionProvider = sessionProvider;
            _cookieProvider = cookieProvider;
            _querystringProvider = querystringProvider;
            _referrerProvider = referrerProvider;
        }

        // Methods
        public OutputModel ProcessRequest()
        {

            // check for active phone manager session
            var returnResult = _sessionProvider.GetSession<OutputModel>();
            if (returnResult?.IsValid() ?? false)
                return returnResult; // valid phone manager session exists so return it

            // try and find a relevant phone number from the phone manager records based on the criteria's. Null if none found
            var criteriaProcessor = new CriteriaProcessor(_dataProvider, _querystringProvider, _referrerProvider);
            var foundRecord = criteriaProcessor.GetMatchingRecord();

            // load exisiting cookie if it exisits, null if not
            var exisitingCookie = _cookieProvider.GetCookie();

            // pass all avaible data into the AppDataProcessor which decides which data to use
            var logicBox = new AppDataProcessor(_dataProvider.GetDefaultSettings(), exisitingCookie, foundRecord);

            if (logicBox.OutputCookieHolder != null)
                _cookieProvider.SetCookie(logicBox.OutputCookieHolder);

            // save session
            _sessionProvider.SetSession(logicBox.OutputModelResult);

            return logicBox.OutputModelResult;
            
        }

    }
}