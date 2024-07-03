using TimeKeeper.Core.DTO;

namespace TimeKeeper.Core.Interface.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> Authenticate(string? email, string? password);
    }
}