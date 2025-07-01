using AutoMapper;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using GenderHealcareSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService service, IMapper mapper)
        {
            _service = service;
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
    }
}
