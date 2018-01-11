using AutoMapper;
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
        }
    }
}