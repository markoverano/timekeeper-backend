using TimeKeeper.Core.Entities;

namespace TimeKeeper.Core.Interface.Repositories
{
    public interface IUserDetailsRepository
    {
        Task AddUserAsync(UserDetail user);
        Task<UserDetail> GetUserByIdAsync(Guid userId);
    }
}
