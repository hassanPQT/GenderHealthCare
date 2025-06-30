using AutoMapper;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using GenderHealcareSystem.DTO;
using GenderHealcareSystem.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffScheduleController : ControllerBase
    {
        private readonly IStaffScheduleService _service;
        private readonly IMapper _mapper;

        public StaffScheduleController(IStaffScheduleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedulesAsync([FromQuery] Guid? staffId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] TimeSpan? fromHour, [FromQuery] TimeSpan? toHour)
        {
            // Get all schedules from DB
            var scheduleDomains = await _service.GetAllAsync(staffId, fromDate, toDate, fromHour, toHour);

            // Convert domains to dto
            var scheduleDtos = _mapper.Map<IEnumerable<StaffScheduleDto>>(scheduleDomains);
            foreach (var scheduleDomainDto in scheduleDtos)
                scheduleDomainDto.Consultant = _mapper.Map<UserDto>(scheduleDomainDto.Consultant);

            return Ok(scheduleDtos);
        }


        [HttpGet("{id:guid}/{staffId:guid}")]
        public async Task<IActionResult> GetScheduleById([FromRoute] Guid id, [FromRoute] Guid staffId)
        {
            // Get schedule from DB
            var schedule = await _service.GetByIdAsync(id, staffId);

            if (schedule == null)
                return BadRequest("Staff schedule not existed!");

            var scheduleDto = _mapper.Map<StaffScheduleDto>(schedule);
            scheduleDto.Consultant = _mapper.Map<UserDto>(scheduleDto.Consultant);
            
            return Ok(scheduleDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateScheduleAsync(AddScheduleRequest dto)
        {
            // Convert Dto to domain
            var scheduleDomain = _mapper.Map<StaffSchedule>(dto);

            // Create initial value
            scheduleDomain.Status = "Pending";
            scheduleDomain.CreatedAt = DateTime.Now;
            scheduleDomain.UpdatedAt = DateTime.Now;

            // Add domain to DB
            scheduleDomain = await _service.CreateAsync(scheduleDomain);

            // Convert domain to DB
            var scheduleDto = _mapper.Map<StaffScheduleDto>(scheduleDomain);
            scheduleDto.Consultant = _mapper.Map<UserDto>(scheduleDto.Consultant);

            return CreatedAtAction(nameof(GetScheduleById),
            new { id = scheduleDto.StaffScheduleId, staffId = scheduleDto.ConsultantId },
            scheduleDto);
        }

        [HttpPut("{id:guid}/{staffId:guid}")]
        public async Task<IActionResult> UpdateScheduleAsync([FromRoute] Guid
            id, [FromRoute] Guid staffId, UpdateScheduleRequest dto)
        {
            // Convert Dto to domain
            var scheduleDomain = _mapper.Map<StaffSchedule>(dto);
            scheduleDomain.UpdatedAt = DateTime.Now;

            // Update domain in DB
            scheduleDomain = await _service.UpdateAsync(id, scheduleDomain, staffId);

            if (scheduleDomain == null)
                return BadRequest("Staff Schedule is not existed!");

            // Convert domain to Dto
            var scheduleDto = _mapper.Map<StaffScheduleDto>(scheduleDomain);
            scheduleDto.Consultant = _mapper.Map<UserDto>(scheduleDto.Consultant);
            return Ok(scheduleDto);
        }

        [HttpDelete("{id:guid}/{staffId:guid}")]
        public async Task<IActionResult> DeleteScheduleAsync([FromRoute] Guid
            id, [FromRoute] Guid staffId)
        {
            // Delete schedule in DB
            var isDeleted = await _service.DeleteAsync(id, staffId);

            if (!isDeleted)
                return BadRequest("Staff Schedule is not existed!");

            return NoContent();
        }
    }
}
