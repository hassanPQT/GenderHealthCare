using AutoMapper;
using BusinessAccess.Helpers;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using GenderHealcareSystem.CustomActionFilters;
using GenderHealcareSystem.DTO;
using GenderHealcareSystem.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffConsultantController : ControllerBase
    {
        private readonly IStaffConsultantService _service;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public StaffConsultantController(IStaffConsultantService service, IConfiguration configuration, IMapper mapper)
        {
            _service = service;
            _configuration = configuration;
            _mapper = mapper;
        }

        //GETALL StaffConsultant
        [HttpGet("{roleId:guid}")]
        public async Task<IActionResult> GetAll([FromRoute] Guid roleId)
        {
            //Get user from DB - domain model
            var StaffConsultantDomain = await _service.GetAllAsync(roleId);

            //Convert Domain model to Dto
            var StaffConsultantDtos = _mapper.Map<IEnumerable<StaffConsultantDto>>(StaffConsultantDomain);

            return Ok(StaffConsultantDtos);
        }

        //GET SERVICE BY ID
        [HttpGet("{id:guid}/{roleId:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, [FromRoute] Guid roleId)
        {
            //Get user from database - domain model
            var userDomain = await _service.GetByIdAsync(id, roleId);

            if (userDomain == null)
                return NotFound();

            //Covert domain model to dto
            var StaffConsultantDto = _mapper.Map<StaffConsultantDto>(userDomain);
            StaffConsultantDto.Role = _mapper.Map<RoleDto>(StaffConsultantDto.Role);

            return Ok(StaffConsultantDto);
        }

        //CREATE NEW StaffConsultant
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddStaffConsultantRequest dto)
        {
            //Convert Dto to domain model
            var userDomain = _mapper.Map<User>(dto);

            // Config staff
            var guid = Guid.NewGuid();
            string[] split = guid.ToString().Split('-');
            var password = split[0];

            userDomain.Password = BCrypt.Net.BCrypt.HashPassword(password);
            userDomain.Email = $"{userDomain.Username}@gender.com";
            userDomain.IsActive = true;
            userDomain.CreatedAt = DateTime.Now;
            userDomain.UpdatedAt = DateTime.Now;

            //Create user in DB
            var StaffConsultantDomain = await _service.CreateAsync(userDomain);

            // Send email
            var emailService = new EmailHelper(_configuration);
            await emailService.SendStaffAccountInfoEmail(dto.PersonalEmail, userDomain.Username, userDomain.FullName, password);

            //Convert Domain model to Dto
            var StaffConsultantDto = _mapper.Map<StaffConsultantDto>(StaffConsultantDomain);
            StaffConsultantDto.Role = _mapper.Map<RoleDto>(StaffConsultantDto.Role);

            return CreatedAtAction(nameof(GetById), new { id = StaffConsultantDto.UserId, roleId = StaffConsultantDto.RoleId }, StaffConsultantDto);
        }

        //UPDATE EXIST StaffConsultant
        [HttpPut("{id:guid}/{roleId:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStaffConsultantRequest dto, [FromRoute] Guid roleId)
        {
            //Convert Dto to domain model
            var StaffConsultantDomain = _mapper.Map<User>(dto);

            //Config staff
            StaffConsultantDomain.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            StaffConsultantDomain.UpdatedAt = DateTime.Now;

            //Update user in DB
            StaffConsultantDomain = await _service.UpdateAsync(id, StaffConsultantDomain, roleId);

            if (StaffConsultantDomain == null)
                return NotFound();

            //Convert Domain model to Dto
            var StaffConsultantDto = _mapper.Map<StaffConsultantDto>(StaffConsultantDomain);
            StaffConsultantDto.Role = _mapper.Map<RoleDto>(StaffConsultantDto.Role);

            return Ok(StaffConsultantDto);
        }

        //DELETE EXIST StaffConsultant
        [HttpDelete("{id:guid}/{roleId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromRoute] Guid roleId)
        {
            // Delete from DB by Id
            var isDeleted = await _service.DeleteAsync(id, roleId);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }

        // REVIVE EXIST StaffConsultant
        [HttpPut("{id:guid}/{roleId:guid}/revive")]
        public async Task<IActionResult> Revive([FromRoute] Guid id, [FromRoute] Guid roleId)
        {
            // Revive from DB by Id
            var isRevived = await _service.ReviveAsync(id, roleId);

            if (!isRevived)
                return NotFound();

            return Ok();
        }
    }
}
