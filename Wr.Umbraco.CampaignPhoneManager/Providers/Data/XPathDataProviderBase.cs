﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.XPath;
using Umbraco.Web;
using Wr.Umbraco.CampaignPhoneManager.Models;

namespace Wr.Umbraco.CampaignPhoneManager.Providers
{
    public abstract class XPathDataProviderBase
    {
        internal const string baseXpath = "$ancestorOrSelf/ancestor-or-self::home[position()=1]//";

        public static string xpath4CampaignDetailHolder = string.Format("{0}{1}/{2}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignDetail) + "[{0}]";

        public static string xpath4DefaultCampaignPhoneManagerSettings = string.Format("{0}{1}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager);

    }
}