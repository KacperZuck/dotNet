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
            // Reguła: Z Encji do DTO (to co robiliśmy wcześniej)
            CreateMap<User, UserDto>().ReverseMap();

            // Reguła: Z DTO do Encji (Reverse Mapping)
            // To jest właśnie to miejsce, o które pytasz
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.id, opt => opt.Ignore());
            // Ignorujemy Id, żeby AutoMapper nie próbował nadpisać 
            // klucza głównego w bazie danych wartością z DTO.
        }
    }
}