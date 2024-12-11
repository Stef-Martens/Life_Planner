using LifePlanner.Server.Data;
using LifePlanner.Server.Models;
using LifePlanner.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LifePlanner.Server.Repositories
{
    public class GoalRepository : GenericRepository<Goal>, IGoalRepository
    {
        public GoalRepository(LifePlannerServerContext context) : base(context)
        {
        }

        public async Task<Goal[]> GetGoalsByUserID(int userId)
        {
            return await _context.Goals.Where(g => g.UserId == userId).ToArrayAsync();
        }
    }
}
