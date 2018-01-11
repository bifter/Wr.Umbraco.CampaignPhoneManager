using Newtonsoft.Json;
using System;
using Umbraco.Core;
using Umbraco.Web;
using Wr.UmbracoCampaignPhoneManager.Providers;
using Zone.UmbracoPersonalisationGroups.Criteria;

namespace Wr.UmbracoCampaignPhoneManager.Plugins.UmbracoPersonalisationGroups
{
    public class CampaignPhoneManagerPersonalisationGroupCriteria : IPersonalisationGroupCriteria
    {
        private readonly ISessionProvider _sessionProvider;

        public CampaignPhoneManagerPersonalisationGroupCriteria()
        {
            _sessionProvider = new SessionProvider();
        }

        public CampaignPhoneManagerPersonalisationGroupCriteria(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public string Name => "Campaign Phone Manager";

        public string Alias => "campaignPhoneManager";

        public string Description => "Matches campaign visitor from the Campaign Phone Manager";

        public bool MatchesVisitor(string definition)
        {
            Mandate.ParameterNotNullOrEmpty(definition, "definition");

            CampaignPhoneManagerCriteriaSetting criteriaSetting;
            try
            {
                criteriaSetting = JsonConvert.DeserializeObject<CampaignPhoneManagerCriteriaSetting>(definition);
            }
            catch (JsonReaderException)
            {
                throw new ArgumentException($"Provided definition is not valid JSON: {definition}");
            }

            if (!string.IsNullOrEmpty(criteriaSetting?.DocumentId))
            {
                // get the actual campaignDetail document Umbraco >= 7.6 method
                if (!Udi.TryParse(criteriaSetting.DocumentId,  out Udi theId))
                {
                    return false;
                }

                var campaignDetailSelectednUmbracoPersonalisationGroups = new UmbracoHelper(UmbracoContext.Current).TypedContent(theId);
                if (campaignDetailSelectednUmbracoPersonalisationGroups != null)
                {
                    var campaignSession = _sessionProvider.GetSession(); // check if there is a campaign phone manager session
                    if (!string.IsNullOrEmpty(campaignSession?.Id))
                    {
                        if (campaignSession.Id == campaignDetailSelectednUmbracoPersonalisationGroups.Id.ToString()) // does the campaign session match the selected UmbracoPersonalisationGroups criteria
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                //throw new ArgumentNullException("key", "Campaign Phone Manager item not selected");
            }

            return false;
        }
    }
}