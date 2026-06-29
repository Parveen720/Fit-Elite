using Fit_Elite.Domain.Entities;

namespace Fit_Elite.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(long id);
        Task AddAsync(User user);
        Task<bool> EmailExistsAsync(string email);
        Task<Role?> GetRoleByNameAsync(string roleName);
        Task SaveChangesAsync();
    }
}