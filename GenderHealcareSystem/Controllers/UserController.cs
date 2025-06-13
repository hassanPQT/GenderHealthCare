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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(Guid id, [FromBody] UpdateUserRequest request)
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

            var response = new UserResponse
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
