using AutoMapper;
using Starter.Domain.Entities;
using Starter.Web.Api.Models;
using System;

namespace Starter.Web.Api
{
    public class MapperConfig
    {
        public static void RegisterMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PageTitle, PageTitleModel>()
                    .ForMember(dest => dest.Page, opt => opt.ResolveUsing(x => x.Page.ToString()))
                    .ForMember(dest => dest.MediaType, opt => opt.ResolveUsing(x => x.MediaType.ToString()))
                    .ForMember(dest => dest.Language, opt => opt.ResolveUsing(x => x.Language.ToString()))
                    .ForMember(dest => dest.Created, opt => opt.ResolveUsing(x => x.Created.ToString("dd/MM/yyyy hh:mm")));

                cfg.CreateMap<PageTitleModel, PageTitle>()
                    .ForMember(dest => dest.Created, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.PageHighlights, opt => opt.Ignore())
                    .ForMember(dest => dest.Page, opt => opt.ResolveUsing(x => Enum.Parse(typeof(Page), x.Page)))
                    .ForMember(dest => dest.MediaType, opt => opt.ResolveUsing(x => Enum.Parse(typeof(MediaType), x.MediaType)))
                    .ForMember(dest=>dest.Language,opt=>opt.ResolveUsing(x=> Enum.Parse(typeof(Language),x.Language)));

                cfg.CreateMap<PageHighlight, PageHighlightModel>()
                    .ForMember(dest => dest.MediaType, opt => opt.ResolveUsing(x => x.MediaType.ToString()))
                    .ForMember(dest => dest.Language, opt => opt.ResolveUsing(x => x.Language.ToString()));
                cfg.CreateMap<PageHighlightModel, PageHighlight>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore())
                    .ForMember(dest => dest.Created, opt => opt.Ignore())
                    .ForMember(dest => dest.MediaType, opt => opt.ResolveUsing(x => Enum.Parse(typeof(MediaType), x.MediaType)))
                    .ForMember(dest => dest.Language, opt => opt.ResolveUsing(x => Enum.Parse(typeof(Language), x.Language))); ;

                cfg.CreateMap<User, UserModel>()
                    .ForMember(dest=>dest.Password,opt=>opt.Ignore());

                cfg.CreateMap<UserModel, User>()
                    .ForMember(dest => dest.Created, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

                cfg.CreateMap<Domain.Entities.Profile, ProfileModel>();
                cfg.CreateMap<ProfileModel,Domain.Entities.Profile>()
                    .ForMember(dest => dest.Created, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

                cfg.CreateMap<Role, RoleModel>();
                cfg.CreateMap<RoleModel, Role>()
                    .ForMember(dest => dest.Created, opt => opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
            });
        }
    }
}