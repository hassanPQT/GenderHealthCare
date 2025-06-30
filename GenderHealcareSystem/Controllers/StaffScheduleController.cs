using AutoMapper;
using BusinessAccess.Services.Interfaces;
using GenderHealcareSystem.DTO;
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
        public async Task<IActionResult> GetAllServicesAsync([FromRoute] Guid? staffId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] TimeSpan? fromHour, [FromQuery] TimeSpan? toHour)
        {
            // Get all schedules from DB
            var scheduleDomains = await _service.GetAllAsync(staffId, fromDate, toDate, fromHour, toHour);

            // Convert domains to dto
            var scheduleDtos = _mapper.Map<IEnumerable<StaffScheduleDto>>(scheduleDomains);
            foreach (var scheduleDomainDto in scheduleDtos)
                scheduleDomainDto.Consultant = _mapper.Map<UserDto>(scheduleDomainDto.Consultant);

            return Ok(scheduleDomains);
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


        
    }
}
