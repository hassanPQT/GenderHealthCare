using AutoMapper;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using GenderHealcareSystem.CustomActionFilters;
using GenderHealcareSystem.DTO;
using GenderHealcareSystem.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffConsultantController : ControllerBase
    {
        private readonly IStaffConsultantService _service;
        private readonly IMapper _mapper;

        public StaffConsultantController(IStaffConsultantService service, IMapper mapper)
        {
            _service = service;
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

            return Ok(StaffConsultantDto);
        }

        //CREATE NEW StaffConsultant
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddStaffConsultantRequest dto)
        {
            //Convert Dto to domain model
            var userDomain = _mapper.Map<User>(dto);

            //Create user in DB
            var StaffConsultantDomain = await _service.CreateAsync(userDomain);

            //Convert Domain model to Dto
            var StaffConsultantDto = _mapper.Map<StaffConsultantDto>(StaffConsultantDomain);

            return CreatedAtAction(nameof(GetById), new { id = StaffConsultantDto.UserId, roleId = StaffConsultantDto.RoleId }, StaffConsultantDto);
        }

        //UPDATE EXIST StaffConsultant
        [HttpPut("{id:guid}/{roleId:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStaffConsultantRequest dto, [FromRoute] Guid roleId)
        {
            //Convert Dto to domain model
            var StaffConsultantDomain = _mapper.Map<User>(dto);

            //Update user in DB
            StaffConsultantDomain = await _service.UpdateAsync(id, StaffConsultantDomain, roleId);

            if (StaffConsultantDomain == null)
                return NotFound();

            //Convert Domain model to Dto
            var StaffConsultantDto = _mapper.Map<StaffConsultantDto>(StaffConsultantDomain);

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
