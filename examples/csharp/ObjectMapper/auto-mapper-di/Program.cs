using AutoMapper;
using ObjectMapper.Dto;
using ObjectMapper.Entities;
using ObjectMapper.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 使用服务容器创建 Mapper 的方式转换对象，将 IMapper 注册到容器中
builder.Services.AddSingleton<AutoMapper.IConfigurationProvider>(sp => new MapperConfiguration(config =>
{
    config.AddProfile(typeof(UserMapperProfile));
}));
builder.Services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", (IMapper mapper) =>
{
    var users = new[]
    {
        new User
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "hu",
            LastName = "hongqi",
            Sex = UserSex.Man
        },
    };

    return mapper.Map<UserDto[]>(users);
});

app.Run();
