using System.Web;
using System.Web.SessionState;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class SessionProvider : ISessionProvider
    {
        HttpContext _httpContext;
        HttpSessionState _session;

        // constructor
        public SessionProvider()
        {
            _httpContext = HttpContext.Current;
            _session = _httpContext.Session;
        }

        /// <summary>
        /// Attempts to get the session
        /// </summary>
        /// <returns>Generic Type</returns>
        public T GetSession<T>(string key = "") where T : class, new()
        {
            if (_session != null)
            {
                var sessionHolder = _session[AppConstants.SessionKeys.PM_Session];
                if (sessionHolder != null)
                {
                    T returnResult = new T();
                    returnResult = (T)_session[(!string.IsNullOrEmpty(key)) ? key : AppConstants.SessionKeys.PM_Session];
                    return returnResult;
                }
            }
            return null; // default(T);
        }

        /// <summary>
        /// Set/Save Session using Generics
        /// </summary>
        /// <param name="model">PhoneManagerResultModel</param>
        /// <returns>true if session saved</returns>
        public bool SetSession<T>(T model, string key = "")
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