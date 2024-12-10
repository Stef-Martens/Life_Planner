using LifePlanner.Server.Data;
using LifePlanner.Server.Models;
using LifePlanner.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifePlanner.Server.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LifePlannerServerContext context) : base(context)
        {
        }
        public async Task<User> GetByAuth0Id(string auth0Id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Auth0Id == auth0Id);
        }
    }
}
