using AutoMapper;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.DTO;

namespace MagicVilla_Web.Mappers
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();

            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();


            CreateMap<VillaNumberDto, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberUpdateDto>().ReverseMap();



        }
    }
}
