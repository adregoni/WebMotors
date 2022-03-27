using AutoMapper;
using WebMotors.Domain.Entities;
using WebMotors.Domain.Models;

namespace WebMotors.Domain.AutoMapper.Profiles
{
    public class AnuncioProfile : Profile
    {
        public AnuncioProfile()
        {
            CreateMap<Anuncio, AnuncioRequest>().ReverseMap();

            CreateMap<AnuncioResponse, Anuncio>().ReverseMap();
        }
    }
}
