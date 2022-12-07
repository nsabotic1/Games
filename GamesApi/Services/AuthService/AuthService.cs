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
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var serviceResponse = new ServiceResponse<string>();
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower().Equals(username.ToLower()));
            if(user== null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found!";
            }
            else if (!_passwordHash.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Wrong password!";
            }
            else
            {
                serviceResponse.Data = _passwordHash.CreateToken(user);
            }

            return serviceResponse;
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
            _passwordHash.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
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
