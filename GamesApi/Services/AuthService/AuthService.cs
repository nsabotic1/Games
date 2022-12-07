using GamesApi.Data;
using GamesApi.Helpers;
using GamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesApi.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IPasswordHash _passwordHash;

        public AuthService(DataContext context, IPasswordHash passwordHash)
        {
            _context = context;
            _passwordHash = passwordHash;
        }
        public Task<ServiceResponse<string>> Login(User user, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceResponse<int>();
            if(await UserExists(user.Username))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User already exists!";
                return serviceResponse;
            }
            _passwordHash.createPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            serviceResponse.Data=user.Id;
            return serviceResponse;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }
    }
}
