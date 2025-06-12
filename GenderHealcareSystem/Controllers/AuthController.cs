using DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using GenderHealcareSystem.DTO.Request;
using BusinessAccess.Helpers;
using GenderHealcareSystem.DTO.Response;
using BusinessAccess.Services.Interfaces;
namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid registration request.");
            }


            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Password and Confirm Password do not match.");
            }

            var existingUser = await _userService.FindAccountByUserName(request.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };

            await _userService.AddUser(newUser);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Invalid login request.");
            }

            var user = await _userService.FindAccountByUserName(request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized("Invalid username or password.");
            }
            if (user.IsDeleted)
            {
                return Unauthorized("User account is deleted.");
            }
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var expiresInMinutes = int.Parse(_configuration["JwtSettings:TokenValidityMins"]);
            var refreshTokenValidityDays = int.Parse(_configuration["JwtSettings:RefreshTokenValidityDays"]);
            var refreshTokenExpires = DateTime.UtcNow.AddDays(refreshTokenValidityDays);
            // Generate token
            var token = JwtTokenHelper.GenerateJwtToken(user, secretKey);
            var refreshToken = JwtTokenHelper.GenerateRefreshToken();

            var response = new LoginResponse
            {
                Username = user.Username,
                AccessToken = token,
                ExpiresIn = expiresInMinutes * 60,
                RefreshToken  = refreshToken            
            };

            return Ok(response);

        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            // Với kiểu không lưu DB, bạn chỉ có thể trust token cũ là còn hợp lệ
            if (string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrEmpty(request.Username))
                return BadRequest("Missing refresh token or username.");

            var user = await _userService.FindAccountByUserName(request.Username);
            if (user == null || user.IsDeleted)
                return Unauthorized("Invalid user.");

            // Không kiểm tra refresh token (vì không lưu DB)
            var secretKey = _configuration["JwtSettings:SecretKey"];
            var expiresInMinutes = int.Parse(_configuration["JwtSettings:TokenValidityMins"]);
            var newAccessToken = JwtTokenHelper.GenerateJwtToken(user, secretKey);

            return Ok(new
            {
                accessToken = newAccessToken,
                expiresIn = expiresInMinutes * 60
            });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
           
            return Ok("User logged out successfully.");
        }

        [HttpGet("debug-claims")]
        public IActionResult DebugClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return Ok(claims);
        }

    }
}
