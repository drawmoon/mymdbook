using ObjectMapper.Dto;
using ObjectMapper.Entities;
using ObjectMapper.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGet("/", () =>
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

    return Mapster.Mapper.Map<UserDto[]>(users);
});

app.Run();
