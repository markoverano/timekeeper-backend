using Microsoft.EntityFrameworkCore;
using TimeKeeper.Core.Entities;
using TimeKeeper.Core.Interface.Repositories;
using TimeKeeper.Infrastructure.Data;

namespace TimeKeeper.Infrastructure.Repositories
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly ApplicationDbContext _context;

        public UserDetailsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(UserDetail user)
        {
            await _context.UserDetails.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserDetail> GetUserByIdAsync(Guid userId)
        {
            return await _context.UserDetails.FirstOrDefaultAsync(u => u.UserId == userId);
        }
    }
}
