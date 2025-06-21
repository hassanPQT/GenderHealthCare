using DataAccess.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;

namespace BusinessAccess.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> HandleGoogleLoginAsync(AuthenticateResult authenticateResult, IConfiguration configuration);
        string GenerateJwtToken(User user, string secretKey);
    }
}
