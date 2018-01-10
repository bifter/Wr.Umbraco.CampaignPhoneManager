using Newtonsoft.Json;
using System;
using System.Diagnostics;
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
                var sessionKey = (!string.IsNullOrEmpty(key)) ? key : AppConstants.SessionKeys.PM_Session;
                //Debug.WriteLine("Get Session sessionKey: " + sessionKey);
                var sessionHolder = _session[AppConstants.SessionKeys.PM_Session];
                if (sessionHolder != null)
                {
                    try
                    {
                        OutputModel result = JsonConvert.DeserializeObject<OutputModel>(sessionHolder.ToString());
                        Debug.WriteLine("GetSession: IsValid: " + result.IsValid());
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
            if (_session != null)
            {
                var sessionKey = (!string.IsNullOrEmpty(key)) ? key : AppConstants.SessionKeys.PM_Session;
                try
                {
                    var jsonData = JsonConvert.SerializeObject(model);
                    _session[sessionKey] = jsonData;
                    Debug.WriteLine("SetSession: jsonData: " + jsonData);
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