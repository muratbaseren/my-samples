using AutoMapper;
using JupiterApp.Data;

namespace JupiterApp
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Album, AlbumRow>().ReverseMap();
        }
    }
}
