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
            cfg.AddProfile<SongProfile>();
        });

        Mapper = config.CreateMapper();
    }
    public class SongProfile : Profile
    {
        public SongProfile()
        {
            CreateMap<Song, SongDto>();
        }
    }
}