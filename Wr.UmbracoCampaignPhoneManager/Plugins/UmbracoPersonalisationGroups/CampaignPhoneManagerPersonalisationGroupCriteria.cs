using Newtonsoft.Json;
using System;
using System.Diagnostics;
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

            if (!string.IsNullOrEmpty(criteriaSetting?.NodeId))
            {
                // get the actual campaignDetail document
                Debug.WriteLine("Criteria NodeId: " + criteriaSetting.NodeId);
                if (int.TryParse(criteriaSetting.NodeId, out int theId))
                {
                    var campaignDetailSelectednUmbracoPersonalisationGroups = new UmbracoHelper(UmbracoContext.Current).TypedContent(theId);
                    if (campaignDetailSelectednUmbracoPersonalisationGroups != null)
                    {
                        var campaignSession = _sessionProvider.GetSession(); // check if there is a campaign phone manager session
                        if (!string.IsNullOrEmpty(campaignSession?.Id))
                        {
                            Debug.WriteLine("Criteria sessionId: " + campaignSession.Id);
                            if (campaignSession.Id == campaignDetailSelectednUmbracoPersonalisationGroups.Id.ToString()) // does the campaign session match the selected UmbracoPersonalisationGroups criteria
                            {
                                Debug.WriteLine("Criteria true!");
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                //throw new ArgumentNullException("key", "Campaign Phone Manager item not selected");
            }
            Debug.WriteLine("Criteria failed");
            return false;
        }
    }
}