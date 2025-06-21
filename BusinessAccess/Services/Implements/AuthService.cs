using BusinessAccess.Helpers;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace BusinessAccess.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<User> HandleGoogleLoginAsync(AuthenticateResult authenticateResult, IConfiguration configuration)
        {
            if (!authenticateResult.Succeeded)
            {
                throw new InvalidOperationException("Google authentication failed.");
            }

            var email = authenticateResult.Principal.FindFirst(ClaimTypes.Email)?.Value;
            var name = authenticateResult.Principal.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email not provided by Google.");
            }

            var existingUser = await _userService.FindAccountByEmail(email);
            User user;

            if (existingUser == null)
            {
                user = new User
                {
                    Username = name ?? email.Split('@')[0],
                    Email = email,
                    Password = null,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _userService.AddUser(user);
            }
            else
            {
                user = existingUser;
            }

            return user;
        }

        public string GenerateJwtToken(User user, string secretKey)
        {
            return JwtTokenHelper.GenerateJwtToken(user, secretKey);
        }
    }
}
