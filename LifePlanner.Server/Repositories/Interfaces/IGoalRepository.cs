using LifePlanner.Server.Models;

namespace LifePlanner.Server.Repositories.Interfaces
{
    public interface IGoalRepository : IGenericRepository<Goal>
    {
        Task<Goal[]> GetGoalsByUserID(int userId);
    }
}
