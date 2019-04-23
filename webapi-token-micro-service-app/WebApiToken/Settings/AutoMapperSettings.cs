using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiToken.DataTransferObjects;
using WebApiToken.Models;

namespace WebApiToken.Settings
{
    public class AutoMapperSettings : Profile
    {
        public AutoMapperSettings()
        {
            CreateMap<UserForRegistrationDto, User>().ReverseMap();
        }
    }
}
