using DDDExample.Domain.User.Model;

namespace DDDExample.Domain.User.Repository;

public interface IUserRepository
{
    Task<IdentityUser?> Get(Username username);

    Task<IdentityUser?> Get(Email email);

    Task<IdentityUser> Add(IdentityUser user);
}