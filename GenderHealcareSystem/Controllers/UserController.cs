using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using GenderHealcareSystem.DTO.Request;
using GenderHealcareSystem.DTO.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GenderHealcareSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                return BadRequest("Invalid user ID in token");

            var user = await _userService.FindAccountById(userId);
            if (user == null) return NotFound();

            var response = new UserResponse
            {
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                DateOfBirth = user.Dob,
                Gender = user.Gender
            };

            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(Guid id, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userUpdate = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Gender = request.Gender,
                Dob = request.DateOfBirth
            };

            userUpdate = await _userService.UpdateAsync(id, userUpdate);

            return Ok(new { message = "Profile updated successfully." });
        }
    }
}
