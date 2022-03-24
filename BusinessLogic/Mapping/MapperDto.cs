using AutoMapper;

namespace BusinessLogic.Mapping
{
    public static class MapperDto
    {
        public static IMapper Service = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()));
    }
}