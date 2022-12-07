using GamesApi.Models;

namespace GamesApi.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(User user, string password);
        Task<bool> UserExists(string username);
    }
}
