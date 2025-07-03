using AutoMapper;
using BusinessAccess.Helpers;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AppointmentController(IAppointmentService service, IGoogleMeetService meetService, IUserService userService, IMapper mapper, IConfiguration configuration)
        {
            _service = service;
            _meetService = meetService;
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
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
            // Check duplicate appointment
            var appointmentList = await _service.GetAllAsync(null, null, null, null, null, null);

            foreach (var appointment in appointmentList)
            {
                if (appointment.AppointmentDate.Date == dto.AppointmentDate.Date && Math.Abs((appointment.AppointmentDate -dto.AppointmentDate).TotalMinutes) < 60)
                    return BadRequest("This time of date is already booked! Please another day and time!");
            };

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


        [HttpGet("send_email")]
        public async Task<IActionResult> SendEmail([FromQuery] Guid appointmentId)
        {
            var appointment = await _service.GetByIdAsync(appointmentId);

            var user = await _userService.FindAccountById(appointment.UserId);
            var consultant = await _userService.FindAccountById(appointment.ConsultantId);

            var emailService = new EmailHelper(_configuration);
            await emailService.SendAppointmentConfirmationEmail(user.Email, user.FullName, consultant.FullName, appointment.AppointmentDate, appointment.MeetingUrl);

            return Ok("Sending email successfully");
        }
    }
}
