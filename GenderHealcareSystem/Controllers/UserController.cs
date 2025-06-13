using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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


        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                return BadRequest("Invalid user ID in token");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userUpdate = _mapper.Map<User>(request);

            var result = await _userService.UpdateAsync(userId, userUpdate);

            if (result == null)
                return NotFound();

            var response = new UserUpdateResponse
            {
                FullName = result.FullName,
                Email = result.Email,
                PhoneNumber = result.PhoneNumber,
                Address = result.Address,
                DateOfBirth = result.Dob,
                Gender = result.Gender
            };

            return Ok(response);
        }
    }
}