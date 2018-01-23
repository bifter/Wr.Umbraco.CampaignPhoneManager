using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Tests
{
    public static class MockConstants
    {
        public static class DefaultData
        {
            public const string DefaultPhoneNumber = "DEFAULT ADMIN NUMBER";
            public const string DefaultCampaignQuerystringKey = "fsource";
            public const int DefaultPersistDurationInDays = 30;
            public const string LastResortPlaceholder = "XXX XXX XXXX"; // must match the value in wr.umbracophonemanager\appdataprocessor.cs -> Main()
        }

        public static class MockTestPhoneNumberData
        {
            public const string PhoneNumber = "0800 000 0001";
            public const string CampaignCode = "test";
        }

        public static class MockTestCookieData
        {
            public static DateTime Expires => new DateTime(2017, 12, 31);
            public const string PhoneNumber = "COOKIE PHONENUMBER";
            public const string CampaignCode = "COOKIE CC";
            public const string AltMarketingCode = "COOKIE ALT CC";
        }

    }
}
