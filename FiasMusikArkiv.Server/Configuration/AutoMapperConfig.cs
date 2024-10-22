using AutoMapper;
using FiasMusikArkiv.Server.Data.Models;
using FiasMusikArkiv.Server.Data.DTOs;

namespace FiasMusikArkiv.Server.Configuration;
public static class AutoMapperConfig
{
    public static IMapper Mapper { get; }

    static AutoMapperConfig()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ApiProfile>();
        });

        Mapper = config.CreateMapper();
    }
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<Song, SongDto>()
                .ForMember(x=>x.Genre, opt=>opt.MapFrom(src=>src.Genre.ToString()));
        }
    }
}