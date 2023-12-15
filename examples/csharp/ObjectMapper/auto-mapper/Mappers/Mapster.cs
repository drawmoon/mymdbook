using AutoMapper;

namespace ObjectMapper.Mappers
{
    public static class Mapster
    {
        public static readonly IMapper Mapper;

        static Mapster()
        {
            var mapperConfiguration = new MapperConfiguration(configure =>
            {
                configure.AddProfile<UserMapperProfile>();
            });
            
            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}