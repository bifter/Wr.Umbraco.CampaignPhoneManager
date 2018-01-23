using AutoMapper;
using Wr.UmbracoPhoneManager.Models;

namespace Wr.UmbracoPhoneManager.Personalisation
{
    public static class MapConfig
    {
        public static void ConfigureAutoMapper()
        {
            Mapper.CreateMap<PhoneManagerCampaignDetail, PhoneManagerCriteriaSetting>()
                    .ForMember(x => x.NodeId, o => o.MapFrom(s => s.Id))
                    .ForMember(x => x.Title, o => o.MapFrom(s => s.NodeName + " : " + s.TelephoneNumber));
        }
    }
}