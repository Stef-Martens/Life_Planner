using LifePlanner.Server.Models;

namespace LifePlanner.Server.Services.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        Task<User> GetByAuth0Id(string auth0Id);
    }
}
