using System.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public class CookieProvider
    {
        ICookieProviderSource _cookieProviderSource;

        public CookieProvider()
        {
            _cookieProviderSource = new HttpContextCookieProviderSource();
        }

        public CookieProvider(ICookieProviderSource cookieProviderSource)
        {
            _cookieProviderSource = cookieProviderSource;
        }

        /// <summary>
        /// Get cookie data and map to CookieHolder model
        /// </summary>
        /// <returns>Populated CookieHolder</returns>
        public CookieHolder GetCookie()
        {
            var cookie = _cookieProviderSource.GetCookie(AppConstants.CookieKeys.CookieMainKey);
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

            _cookieProviderSource.SetCookie(cookie);
        }
    }
}