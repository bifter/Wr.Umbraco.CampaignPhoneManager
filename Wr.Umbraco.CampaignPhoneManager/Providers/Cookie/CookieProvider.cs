using System.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class CookieProvider
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
                if (cookie.HasKeys)
                {
                    if (!string.IsNullOrEmpty(cookie[AppConstants.CookieKeys.SubKey_PhoneNumber])) // a phone number is present
                    {
                        var result = new CookieHolder();
                        result.Expires = cookie.Expires;
                        result.Model = new OutputModel()
                        {
                            PhoneNumber = cookie[AppConstants.CookieKeys.SubKey_PhoneNumber],
                            CampaignCode = cookie[AppConstants.CookieKeys.SubKey_CampaignCode],
                            AltMarketingCode = cookie[AppConstants.CookieKeys.SubKey_AltMarketingCode]
                        };

                        return result;
                    }
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
            cookie.Values[AppConstants.CookieKeys.SubKey_PhoneNumber] = model.Model.PhoneNumber;
            cookie.Values[AppConstants.CookieKeys.SubKey_CampaignCode] = model.Model.CampaignCode;
            cookie.Values[AppConstants.CookieKeys.SubKey_AltMarketingCode] = model.Model.AltMarketingCode;
            cookie.Expires = model.Expires;

            _cookieImplementation.SetCookie(cookie);
        }
    }
}