using DDDExample.Domain.User.Model;
using DDDExample.Domain.User.Repository;

namespace DDDExample.Domain.User.Factory;

public interface IUserFactory
{
    Task<IdentityUser> CreateUser(Username username, Email email);
}

public class UserFactory : IUserFactory
{
    private readonly IUserRepository userRepository;

    public UserFactory(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<IdentityUser> CreateUser(Username username, Email email)
    {
        var userByUsername = await userRepository.Get(username);
        var userByEmail = await userRepository.Get(email);

        if (userByUsername != null || userByEmail != null)
        {
            throw new ArgumentException("Account already exists.");
        }

        return new IdentityUser
        {
            Name = username,
            Email = email
        };
    }
}
