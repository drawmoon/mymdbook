using DDDExample.Domain.User.Factory;
using DDDExample.Domain.User.Model;
using DDDExample.Domain.User.Repository;

namespace DDDExample.Domain.User.Service;

public interface IUserService
{
    Task<IdentityUser> Create(string name, string email);
}

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUserFactory userFactory;

    public UserService(IUserRepository userRepository, IUserFactory userFactory)
    {
        this.userRepository = userRepository;
        this.userFactory = userFactory;
    }

    public async Task<IdentityUser> Create(string name, string email)
    {
        var user = await userFactory.CreateUser(name, email);

        return await userRepository.Add(user);
    }
}