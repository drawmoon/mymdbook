using DDDExample.Admin.Service;
using DDDExample.Admin.Usecases.User;
using Microsoft.AspNetCore.Mvc;

namespace DDDExample.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService accountService;

    public AccountController(IAccountService accountService)
    {
        this.accountService = accountService;
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserCase.Response>> Register([FromBody] CreateUserCase.Request request)
    {
        return await accountService.Register(request);
    }
}
