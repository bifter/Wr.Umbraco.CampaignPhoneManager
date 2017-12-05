using System.Web;
using System.Web.SessionState;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class SessionProvider : ISessionProvider
    {
        HttpSessionState _session;

        // constructor
        public SessionProvider()
        {
            _session = HttpContext.Current.Session;
        }

        /// <summary>
        /// Attempts to get the session
        /// </summary>
        /// <returns>OutputModel data</returns>
        public OutputModel GetSession(string key = "")
        {
            if (_session != null)
            {
                var sessionHolder = _session[AppConstants.SessionKeys.PM_Session];
                if (sessionHolder != null)
                {
                    OutputModel returnResult = new OutputModel();
                    returnResult = (OutputModel)_session[(!string.IsNullOrEmpty(key)) ? key : AppConstants.SessionKeys.PM_Session];
                    return returnResult;
                }
            }
            return null;
        }

        /// <summary>
        /// Set/Save Session
        /// </summary>
        /// <param name="model">OutputModel</param>
        /// <returns>true if session saved</returns>
        public bool SetSession(OutputModel model, string key = "")
        {
            if (_session != null)
            {
                _session.Add((!string.IsNullOrEmpty(key)) ? key : AppConstants.SessionKeys.PM_Session, model);
                return true;
            }
            return false;
        }

    }
}