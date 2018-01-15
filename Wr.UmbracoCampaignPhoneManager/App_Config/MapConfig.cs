using AutoMapper;
using Umbraco.Core.Models;
using Wr.UmbracoCampaignPhoneManager.Models;
using static Wr.UmbracoCampaignPhoneManager.Plugins.UmbracoPersonalisationGroups.CampaignPhoneManagerPersonalisationGroupAjaxController;

namespace Wr.UmbracoCampaignPhoneManager.App_Config
{
    public static class MapConfig
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<CampaignDetail, AjaxCampaignDetailModel>()
                    .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                    .ForMember(x => x.Name, o => o.MapFrom(s => s.NodeName))
                    .ForMember(x => x.Telephone, o => o.MapFrom(s => s.TelephoneNumber))
                    .ForMember(x => x.CampaignCode, o => o.MapFrom(s => s.CampaignCode));

            /*Mapper.CreateMap<IPublishedContent, CampaignPhoneManagerModel>()
                .ForMember(x => x.DefaultCampaignQueryStringKey, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultCampaignQueryStringKey).Value))
                .ForMember(x => x.DefaultPhoneNumber, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultPhoneNumber).Value))
                .ForMember(x => x.DefaultPersistDurationInDays, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.DefaultPersistDurationInDays).Value))
                .ForMember(x => x.GlobalDisableOverwritePersistingItems, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel.GlobalDisableOverwritePersistingItems).Value));

            Mapper.CreateMap<IPublishedContent, CampaignDetail>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .ForMember(x => x.TelephoneNumber, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.TelephoneNumber).Value))
                .ForMember(x => x.EntryPage, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.EntryPage).Value))
                .ForMember(x => x.Referrer, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.Referrer).Value))
                .ForMember(x => x.UseAltCampaignQueryStringKey, o => o.MapFrom(s => s.GetProperty(AppConstants.UmbracoDocTypeAliases.CampaignPhoneManagerModel_CampaignDetail.UseAltCampaignQueryStringKey).Value));*/
        }

        private static int MapperValueToInt(string value)
        {
            if(int.TryParse(value, out int result))
            {
                return result;
            }
            return 0;
        }

        private static bool MapperValueToBool(string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                return result;
            }
            return false;
        }
    }
}