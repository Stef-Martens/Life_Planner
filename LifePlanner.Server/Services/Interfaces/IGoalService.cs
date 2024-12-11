using LifePlanner.Server.Models;

namespace LifePlanner.Server.Services.Interfaces
{
    public interface IGoalService : IGenericService<Goal>
    {
        public Task<IEnumerable<Goal>> GetGoalsByUserId(int userId);
    }
}
