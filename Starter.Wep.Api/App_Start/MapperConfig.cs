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
                cfg.CreateMap<PageTitleModel,PageTitle>()
                    .ForMember(dest=>dest.Created,opt=>opt.Ignore())
                    .ForMember(dest => dest.Id, opt => opt.Ignore());

                cfg.CreateMap<PageHighlight, PageHighlightModel>();
                cfg.CreateMap<PageHighlightModel, PageHighlight>()
                    .ForMember(dest => dest.Created, opt => opt.Ignore());

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