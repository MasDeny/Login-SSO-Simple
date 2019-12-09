using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebApps.Domain.Models;
using WebApps.Resources;

namespace VisMan.Api.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<UserResource, User>();
        }
    }
}
