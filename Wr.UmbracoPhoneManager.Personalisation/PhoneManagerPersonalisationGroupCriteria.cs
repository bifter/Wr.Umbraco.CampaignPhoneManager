using Newtonsoft.Json;
using System;
using System.Diagnostics;
using Umbraco.Core;
using Umbraco.Web;
using Wr.UmbracoPhoneManager.Providers;
using Zone.UmbracoPersonalisationGroups.Criteria;

namespace Wr.UmbracoPhoneManager.Personalisation
{
    public class PhoneManagerPersonalisationGroupCriteria : IPersonalisationGroupCriteria
    {
        private readonly ISessionProvider _sessionProvider;

        public PhoneManagerPersonalisationGroupCriteria()
        {
            _sessionProvider = new SessionProvider();
        }

        public PhoneManagerPersonalisationGroupCriteria(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public string Name => "Phone Manager";

        public string Alias => "phoneManager";

        public string Description => "Matches campaign visitor from the Phone Manager";

        public bool MatchesVisitor(string definition)
        {
            Mandate.ParameterNotNullOrEmpty(definition, "definition");

            PhoneManagerCriteriaSetting criteriaSetting;
            try
            {
                criteriaSetting = JsonConvert.DeserializeObject<PhoneManagerCriteriaSetting>(definition);
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