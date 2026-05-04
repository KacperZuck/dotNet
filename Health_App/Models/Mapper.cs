using AutoMapper;
using Health_App.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Health_App.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.id, opt => opt.Ignore());
        }
    }
}