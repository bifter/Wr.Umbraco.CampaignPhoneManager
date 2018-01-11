using Newtonsoft.Json;
using System;
using Umbraco.Core;
using Wr.Umbraco.CampaignPhoneManager.Providers;
using Zone.UmbracoPersonalisationGroups.Criteria;

namespace Wr.Umbraco.CampaignPhoneManager.Plugins.UmbracoPersonalisationGroups
{
    public class UmbracoPersonalisationGroups_Criteria : PersonalisationGroupCriteriaBase, IPersonalisationGroupCriteria
    {
        private readonly ISessionProvider _sessionProvider;

        public UmbracoPersonalisationGroups_Criteria()
        {
            _sessionProvider = new SessionProvider();
        }

        public UmbracoPersonalisationGroups_Criteria(ISessionProvider sessionProvider)
        {
            _sessionProvider = sessionProvider;
        }

        public string Name => "Campaign Phone Manager";

        public string Alias => "campaignphonemanagercriteria";

        public string Description => "Matches visitor from the Campaign Phone Manager";

        public bool MatchesVisitor(string definition)
        {
            Mandate.ParameterNotNullOrEmpty(definition, "definition");

            CriteriaSetting criteriaSetting;
            try
            {
                criteriaSetting = JsonConvert.DeserializeObject<CriteriaSetting>(definition);
            }
            catch (JsonReaderException)
            {
                throw new ArgumentException($"Provided definition is not valid JSON: {definition}");
            }

            if (!string.IsNullOrEmpty(criteriaSetting.DocumentId))
            {
                // get the actual document
                //var document = Umbraco.TypedContent();
            }
            else
            {
                throw new ArgumentNullException("key", "Campaign Phone Manager item not selected");
            }

            var foundSession = _sessionProvider.GetSession();
            var value = string.Empty;
            if (foundSession != null)
            {
                value = foundSession.Id;
            }

            /*switch (sessionSetting.Match)
            {
                case SessionSettingMatch.Exists:
                    return keyExists;
                case SessionSettingMatch.DoesNotExist:
                    return !keyExists;
                case SessionSettingMatch.MatchesValue:
                    return keyExists && MatchesValue(value, sessionSetting.Value);
                case SessionSettingMatch.ContainsValue:
                    return keyExists && ContainsValue(value, sessionSetting.Value);
                case SessionSettingMatch.GreaterThanValue:
                case SessionSettingMatch.GreaterThanOrEqualToValue:
                case SessionSettingMatch.LessThanValue:
                case SessionSettingMatch.LessThanOrEqualToValue:
                    return keyExists &&
                        CompareValues(value, sessionSetting.Value, GetComparison(sessionSetting.Match));
                case SessionSettingMatch.MatchesRegex:
                    return keyExists && MatchesRegex(value, sessionSetting.Value);
                case SessionSettingMatch.DoesNotMatchRegex:
                    return keyExists && !MatchesRegex(value, sessionSetting.Value);

                default:
                    return false;
            }*/
            return false;
        }
    }
}