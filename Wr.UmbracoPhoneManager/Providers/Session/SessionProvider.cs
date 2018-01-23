using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.SessionState;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Providers
{
    public class SessionProvider : ISessionProvider
    {
        // constructor
        public SessionProvider() { }

        /// <summary>
        /// Attempts to get the session
        /// </summary>
        /// <returns>OutputModel data</returns>
        public OutputModel GetSession(string key = "")
        {
            var _session = HttpContext.Current.Session;
            if (_session != null)
            {
                var sessionKey = (!string.IsNullOrEmpty(key)) ? key : AppConstants.SessionKeys.PM_Session;
                var sessionHolder = _session[AppConstants.SessionKeys.PM_Session];
                if (sessionHolder != null)
                {
                    try
                    {
                        OutputModel result = JsonConvert.DeserializeObject<OutputModel>(sessionHolder.ToString());
                        return result;
                    }
                    catch (JsonReaderException)
                    {
                        throw new ArgumentException($"Provided definition is not valid JSON: {sessionHolder.ToString()}");
                    }
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
            var _session = HttpContext.Current.Session;
            if (_session != null)
            {
                var sessionKey = (!string.IsNullOrEmpty(key)) ? key : AppConstants.SessionKeys.PM_Session;
                try
                {
                    var jsonData = JsonConvert.SerializeObject(model);
                    _session[sessionKey] = jsonData;
                    return true;
                }
                catch (JsonReaderException)
                {
                    throw new ArgumentException($"Object could not be serialized to JSON");
                }
            }
            return false;
        }

    }
}