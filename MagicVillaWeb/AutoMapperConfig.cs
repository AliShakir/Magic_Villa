using AutoMapper;
using MagicVillaWeb.Models.Data;
using MagicVillaWeb.Models.ViewModel;

namespace MagicVillaWeb
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto, VillaUpdateDto>().ReverseMap();

            CreateMap<VillaNoDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaNoDto, VillaUpdateDto>().ReverseMap();
            CreateMap<VillaNoDto, VillaNoUpdateDto>().ReverseMap();
            CreateMap<VillaNumberUpdateVM,VillaNoUpdateDto>().ReverseMap();
        }
    }
}
