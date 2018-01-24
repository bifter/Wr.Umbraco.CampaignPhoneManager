namespace Wr.UmbracoPhoneManager.Providers.Storage
{
    public abstract class XPathBaseSettings
    {
        //public static string baseXpath = "$ancestorOrSelf/ancestor-or-self::home[position()=1]//";//"$ancestorOrSelf/ancestor-or-self::home[position()=1]//";
        public static string baseXpath = "HOME_NODE_PLACEHOLDER//"; // using homepageNodeId is a workaround for $ancestorOrSelf not working

        public static string xpath4CampaignDetailHolder = string.Format("{0}{1}/{2}", baseXpath, AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManager, AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManagerCampaignDetail) + "[{0}]";

        public static string xpath4DefaultPhoneManagerSettings = string.Format("{0}{1}", baseXpath, AppConstants.UmbracoDocTypeAliases.PhoneManagerModel.PhoneManager);

    }
}