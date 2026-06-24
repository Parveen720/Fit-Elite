using FitElite.Data.Models;
using FitElite.Data.Repositories;
using FitElite.DTOs;
using FitElite.Services.Interfaces;

namespace FitElite.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<Users> _userRepository;

        public UserService(IRepository<Users> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone
            }).ToList();
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };
        }

        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            var user = new Users
            {
                Name = userDto.Name,
                Email = userDto.Email.Trim().ToLowerInvariant(),
                Phone = userDto.Phone
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            userDto.Id = user.Id;

            return userDto;
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new Exception("User not found");

            user.Name = userDto.Name;
            user.Email = userDto.Email.Trim().ToLowerInvariant();
            user.Phone = userDto.Phone;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}