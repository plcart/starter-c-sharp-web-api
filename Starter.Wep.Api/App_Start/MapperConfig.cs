using AutoMapper;
using Starter.Domain.Entities;
using Starter.Web.Api.Models;

namespace Starter.Web.Api
{
    public class MapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PageTitle, PageTitleModel>();
                cfg.CreateMap<PageTitleModel,PageTitle>();
                cfg.CreateMap<PageHighlight, PageHighlightModel>();
                cfg.CreateMap<PageHighlightModel, PageHighlight>();
                    //.ForMember(dest => dest.application_id, opt => opt.MapFrom(src => src.Id))
            });
        }
    }
}