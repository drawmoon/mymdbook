using DDDExample.Admin.Usecases.User;
using DDDExample.Domain.User.Service;
using Mapster;

namespace DDDExample.Admin.Service;

public interface IAccountService
{
    Task<CreateUserCase.Response> Register(CreateUserCase.Request request);
}

public class AccountService : IAccountService
{
    private readonly IUserService userService;

    public AccountService(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<CreateUserCase.Response> Register(CreateUserCase.Request request)
    {
        var user = await userService.Create(request.Name, request.Email);

        return user.Adapt<CreateUserCase.Response>();
    }
}