using AutoMapper;
using Magic_Villa.Models;
using Magic_Villa.Models.Data;

namespace Magic_Villa
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

            CreateMap<VillaNoCreateDto,VillaNumber>();
            CreateMap<VillaNumber, VillaNoDto>();
            CreateMap<VillaNoUpdateDto,VillaNumber>();
        }
    }
}
