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
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly IGoogleMeetService _meetService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService service, IGoogleMeetService meetService, IMapper mapper)
        {
            _service = service;
            _meetService = meetService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAppointments([FromQuery] Guid? customerId, [FromQuery] Guid? consultantId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] TimeSpan? fromHour, [FromQuery] TimeSpan? toHour)
        {
            var appointmentDomains = await _service.GetAllAsync(customerId, consultantId, fromDate, toDate, fromHour, toHour);

            var appointmentDtos = _mapper.Map<IEnumerable<AppointmentDto>>(appointmentDomains);

            return Ok(appointmentDtos);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAppointmentById([FromRoute] Guid id)
        {
            var appointmentDomain = await _service.GetByIdAsync(id);

            if (appointmentDomain == null)
                return BadRequest("Appointment is not existed!");

            var appointmentDto = _mapper.Map<AppointmentDto>(appointmentDomain);

            return Ok(appointmentDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] AddAppointmentRequest dto)
        {
            // Create meet service
            DateTime startTime = dto.AppointmentDate;
            DateTime endTime = dto.AppointmentDate.AddMinutes(30);

            string meetUrl = await _meetService.CreateMeetingAsync(startTime, endTime);

            // Map Dto to domain
            var appointmentDomain = _mapper.Map<Appointment>(dto);
            appointmentDomain.MeetingUrl = meetUrl;
            appointmentDomain.Status = "Pending";
            appointmentDomain.CreatedAt = DateTime.Now;
            appointmentDomain.UpdatedAt = DateTime.Now;

            // Add new Appointment to DB
            appointmentDomain = await _service.CreateAsync(appointmentDomain);

            // Map domain to Dto
            var appointmentDto = _mapper.Map<AppointmentDto>(appointmentDomain);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentDto.AppointmentId }, appointmentDto);
        }

        [HttpGet("test-meet")]
        public async Task<IActionResult> TestMeet([FromServices] IGoogleMeetService googleMeetService)
        {
            var link = await googleMeetService.CreateMeetingAsync(DateTime.UtcNow.AddMinutes(10), DateTime.UtcNow.AddMinutes(40));
            return Ok(new { MeetLink = link });
        }
    }
}
