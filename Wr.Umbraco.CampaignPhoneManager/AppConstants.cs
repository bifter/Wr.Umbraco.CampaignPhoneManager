using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wr.Umbraco.CampaignPhoneManager
{
    public static class AppConstants
    {
        public static class SessionKeys
        {
            public const string PM_Session = "umbPMSess";
        }

        public static class CookieKeys
        {
            public const string CookieMainKey = "umbPMCookie";
            public const string SubKey_PhoneNumber = "umbPM_PN";
            public const string SubKey_CampaignCode = "umbPM_CC";
            public const string SubKey_AltMarketingCode = "umbPM_AMC";
        }

    }
}