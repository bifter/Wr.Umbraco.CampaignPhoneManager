using Newtonsoft.Json;
using System;
using System.Web;
using Wr.UmbracoCampaignPhoneManager.Models;

namespace Wr.UmbracoCampaignPhoneManager.Providers
{
    public class CookieProvider : ICookieProvider
    {
        ICookieImplementation _cookieImplementation;

        public CookieProvider()
        {
            _cookieImplementation = new HttpContextCookieImplementation();
        }

        public CookieProvider(ICookieImplementation cookieImplementation)
        {
            _cookieImplementation = cookieImplementation;
        }

        /// <summary>
        /// Get cookie data and map to CookieHolder model
        /// </summary>
        /// <returns>Populated CookieHolder</returns>
        public CookieHolder GetCookie()
        {
            var cookie = _cookieImplementation.GetCookie(AppConstants.CookieKeys.CookieMainKey);
            if (cookie != null)
            {
                var result = new CookieHolder();
                result.Expires = cookie.Expires;
                try
                {
                    result.Model = JsonConvert.DeserializeObject<OutputModel>(cookie.Value);
                    return result;
                }
                catch (JsonReaderException)
                {
                    throw new ArgumentException($"Provided definition is not valid JSON: {cookie.Value}");
                }
            }
            return null;
        }

        /// <summary>
        /// Set cookie adding subvalues for the PhoneManagerResultModel values
        /// </summary>
        /// <param name="model">CookieHolder</param>
        public void SetCookie(CookieHolder model)
        {
            HttpCookie cookie = new HttpCookie(AppConstants.CookieKeys.CookieMainKey);
            cookie.Expires = model.Expires;
            try
            {
                cookie.Value = JsonConvert.SerializeObject(model.Model);
                _cookieImplementation.SetCookie(cookie);
            }
            catch (JsonReaderException)
            {
                throw new ArgumentException($"Can't convert oject to JSON");
            }
        }
    }
}