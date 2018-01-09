namespace Wr.Umbraco.CampaignPhoneManager.Providers.Storage
{
    public abstract class XPathBaseSettings
    {
        public static string baseXpath = "//";//"$ancestorOrSelf/ancestor-or-self::home[position()=1]//";

        public static string xpath4CampaignDetailHolder = string.Format("{0}{1}/{2}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignDetail) + "[{0}]";

        public static string xpath4DefaultCampaignPhoneManagerSettings = string.Format("{0}{1}", baseXpath, AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.CampaignPhoneManager);

    }
}