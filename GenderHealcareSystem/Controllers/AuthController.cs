using BusinessAccess.Helpers;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using GenderHealcareSystem.DTO.Request;
using GenderHealcareSystem.DTO.Response;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        public AuthController(IUserService userService, IAuthService authService, IConfiguration configuration)
        {
            _userService = userService;
            _authService = authService;
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
            Guid customerRole = Guid.Parse("CB923E1C-ED85-45A8-BC2F-8B78C60B7E28");
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                RoleId = customerRole,
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
            if (!user.IsActive)
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
                RefreshToken = refreshToken
            };

            return Ok(response);

        }

        [HttpGet("google/login")]
        public IActionResult GoogleLogin([FromQuery] string returnUrl = "/")
        {
            try
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(GoogleCallback)),
                    Items =
                    {
                        { "returnUrl", returnUrl }
                    }
                };

                return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Failed to initiate Google login" });
            }
        }

        [HttpGet("googlecallback")]
        public async Task<IActionResult> GoogleCallback()
        {
            try
            {
                var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

                if (!authenticateResult.Succeeded)
                {
                    return BadRequest("Google authentication failed.");
                }

                var user = await _authService.HandleGoogleLoginAsync(authenticateResult, _configuration);

                var secretKey = _configuration["JwtSettings:SecretKey"];
                int expiresInMinutes = int.Parse(_configuration["JwtSettings:TokenValidityMins"]);
                var token = _authService.GenerateJwtToken(user, secretKey);

                var returnUrl = authenticateResult.Properties.Items["returnUrl"] ?? "/";

                return Ok(new
                {
                    accessToken = token,
                    expiresIn = expiresInMinutes * 60,
                    returnUrl = returnUrl
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Error processing Google callback", details = ex.Message });
            }
        }

        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.FindAccountByEmail(request.Email);
            if (user == null || !user.IsActive)
            {
                return NotFound("User not found or account is inactive.");
            }

            var resetToken = Guid.NewGuid().ToString();
            var expiry = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ResetTokenValidityMins"]));

            var emailService = new EmailHelper(_configuration);
            await emailService.SendResetPasswordEmail(request.Email, resetToken);


            return Ok(new
            {
                message = "Password reset link has been sent to your email.",
                resetToken,
                expiry
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingUser = await _userService.FindAccountByEmail(request.Email);
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            await _userService.UpdateAsync(existingUser.UserId, existingUser);

            return Ok("Password has been reset successfully.");
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            // Với kiểu không lưu DB, bạn chỉ có thể trust token cũ là còn hợp lệ
            if (string.IsNullOrEmpty(request.RefreshToken) || string.IsNullOrEmpty(request.Username))
                return BadRequest("Missing refresh token or username.");

            var user = await _userService.FindAccountByUserName(request.Username);
            if (user == null || !user.IsActive)
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
