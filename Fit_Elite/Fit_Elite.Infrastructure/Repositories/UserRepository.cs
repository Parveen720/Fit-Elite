using Fit_Elite.Application.Interfaces;
using Fit_Elite.Domain.Entities;
using Fit_Elite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Fit_Elite.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        
        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users
        .Include(u => u.Role) 
        .FirstOrDefaultAsync(u => u.Id == id);
        }

        
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<Role?> GetRoleByNameAsync(string roleName)
        {
            return await _context.Roles
                
                .FirstOrDefaultAsync(r => r.Name.ToLower() == roleName.ToLower());
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}