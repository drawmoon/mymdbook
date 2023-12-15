using DDDExample.Domain.User.Model;
using DDDExample.Domain.User.Repository;
using Microsoft.EntityFrameworkCore;

namespace DDDExample.EntityFrameworkCore.Repositories;

public class UserRepository : BaseRepository<IdentityUser>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IdentityUser?> Get(Username username)
    {
        return await Entities.FirstOrDefaultAsync(x => x.Name == username);
    }

    public async Task<IdentityUser?> Get(Email email)
    {
        return await Entities.FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<IdentityUser> Add(IdentityUser user)
    {
        await Entities.AddAsync(user);
        await SaveChangesAsync();

        return user;
    }
}