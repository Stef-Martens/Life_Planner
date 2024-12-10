using LifePlanner.Server.Models;

namespace LifePlanner.Server.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByAuth0Id(string auth0Id);
    }
}
