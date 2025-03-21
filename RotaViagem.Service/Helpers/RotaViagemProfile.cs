using System;
using AutoMapper;
using RotaViagem.Service.Dtos;
using RotaViagem.Domain;
using RotaViagem.Domain.Identity;

namespace RotaViagem.API.Helpers
{
    public class RotaViagemProfile : Profile
    {
        public RotaViagemProfile()
        {
            CreateMap<Local, LocalDto>().ReverseMap();
            CreateMap<Local, LocalAddDto>().ReverseMap();
            CreateMap<Local, LocalUpdateDto>().ReverseMap();
            CreateMap<Rota, RotaDto>().ReverseMap();
            CreateMap<Rota, RotaAddDto>().ReverseMap();
            CreateMap<Rota, RotaUpdateDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();
        }
    }
}