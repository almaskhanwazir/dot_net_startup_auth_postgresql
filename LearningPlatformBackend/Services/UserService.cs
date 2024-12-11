using LearningPlatformBackend.Entities;
using LearningPlatformBackend.Repositories.Interfaces;
using LearningPlatformBackend.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LearningPlatformBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();

        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        // Create a user with hashed password
        public async Task<bool> CreateUserAsync(User user)
        {
            if (user == null) return false;

            // Hash the password securely
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

            await _userRepository.AddAsync(user);
            return true;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            if (user == null) return false;

            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
            return true;
        }
    }
}
