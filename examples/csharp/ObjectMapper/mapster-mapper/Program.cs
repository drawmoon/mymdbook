using Mapster;

// Entity to DTO
var user = new User
{
    UserName = "admin",
    Email = "admin@admin.com"
};

var dto = user.Adapt<UserDto>();
Console.WriteLine("Name: {0}, Email: {1}", dto.UserName, dto.Email);

// Entity Collection to DTO Collection
var pagedList = new PagedList<User>
{
    PageIndex = 1,
    PageSize = 10,
    TotalPages = 1,
    TotalCount = 10,
    Items = new List<User> { user }
};

var dtoPagedList = pagedList.Adapt<PagedList<UserDto>>();
Console.WriteLine("Name: {0}, Email: {1}", dtoPagedList.Items.First().UserName, dtoPagedList.Items.First().Email);

// Map of type without default constructor
var config = new TypeAdapterConfig();
config.ForType<Role, RoleDto>().MapToConstructor(true);

var role = new Role("administrator");
var roleDto = role.Adapt<RoleDto>(config);
Console.WriteLine("Name: {0}", roleDto.RoleName);

// Entities
public class BaseUser<TKey>
{
    public TKey Id { get; set; }
}

public class User: BaseUser<int>
{
    public string UserName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}

// DTOs
public class BaseUserDto<TKey>
{
    public TKey Id { get; set; }
}

public class UserDto : BaseUserDto<int>
{
    public string UserName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }
}

// Collection
public class PagedList<TItem>
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<TItem> Items { get; set; }
}

// Class of no default constructor
public class Role
{
    public Role(string roleName)
    {
        RoleName = roleName;
    }

    public string RoleName { get; set; }
}

public class RoleDto
{
    public RoleDto(string roleName)
    {
        RoleName = roleName;
    }

    public string RoleName { get; set; }
}