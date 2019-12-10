using System;
using System.Linq;
using AutoMapper;
using Newtonsoft.Json;
using WebApps.Domain.Models;
using WebApps.Resources;

namespace WebApps.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>()
                .ForMember(u => u.Roles, opt => opt.MapFrom(u => u.UserRoles.Select(ur => ur.Role.Name)))
                .ForMember(u => u.Types, opt => opt.MapFrom(u => u.UserTypes.Select(ur => ur.Type.Name)))
                //.ForMember(u => u.Url, opt => opt.MapFrom(u => u.UserTypes.Select(ur => ur.Type.Url)))
                .ForPath(x => x.Password, x => x.Ignore());

            CreateMap<Domain.Models.Type, TypeResource>();
            CreateMap<Role, RoleResource>();
        }


    }
}
