using LifePlanner.Server.Models;
using LifePlanner.Server.Repositories.Interfaces;
using LifePlanner.Server.Services.Interfaces;

namespace LifePlanner.Server.Services
{
    public class UserService : GenericService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetByAuth0Id(string auth0Id)
        {
            return await _userRepository.GetByAuth0Id(auth0Id);
        }
    }
}
