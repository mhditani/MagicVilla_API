using AutoMapper;
using MagicVilla_villaAPI.Models;
using MagicVilla_villaAPI.Models.DTO;

namespace MagicVilla_villaAPI.Mappers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();

            CreateMap<Villa, VillaUpdateDto>().ReverseMap();


        }
    }
}
