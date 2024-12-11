using LifePlanner.Server.Models;
using LifePlanner.Server.Repositories.Interfaces;
using LifePlanner.Server.Services.Interfaces;

namespace LifePlanner.Server.Services
{
    public class GoalService : GenericService<Goal>, IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        public GoalService(IGoalRepository goalRepository) : base(goalRepository)
        {
            _goalRepository = goalRepository;
        }

        public async Task<IEnumerable<Goal>> GetGoalsByUserId(int userId)
        {
            return await _goalRepository.GetGoalsByUserID(userId);
        }

    }
}
