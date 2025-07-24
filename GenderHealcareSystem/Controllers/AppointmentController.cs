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
        public async Task<IActionResult> GetAllAppointments([FromQuery] Guid? customerId, [FromQuery] Guid? consultantId, [FromQuery] DateOnly? fromDate, [FromQuery] DateOnly? toDate, [FromQuery] int? slot)
        {
            var appointmentDomains = await _service.GetAllAsync(customerId, consultantId, fromDate, toDate, slot);

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
        public async Task<IActionResult> CreateAppointment([FromBody] AddAppointmentRequest dto)
        {
            // Check duplicate appointment
            var appointmentList = await _service.GetAllAsync(null, null, null, null, null);

            foreach (var appointment in appointmentList)
            {
                if (appointment.ConsultantId == dto.ConsultantId && appointment.AppointmentDate == dto.AppointmentDate &&
                    appointment.Slot == dto.Slot)
                    return BadRequest("This slot is already booked! Please choose another day or slot!");
            }
            ;

            // Create meet service
            DateTime startTime;
            DateTime endTime;

            switch (dto.Slot)
            {
                case 1:
                    startTime = dto.AppointmentDate.ToDateTime(new TimeOnly(7, 0));
                    endTime = dto.AppointmentDate.ToDateTime(new TimeOnly(9, 0));
                    break;
                case 2:
                    startTime = dto.AppointmentDate.ToDateTime(new TimeOnly(9, 0));
                    endTime = dto.AppointmentDate.ToDateTime(new TimeOnly(11, 0));
                    break;
                case 3:
                    startTime = dto.AppointmentDate.ToDateTime(new TimeOnly(13, 0));
                    endTime = dto.AppointmentDate.ToDateTime(new TimeOnly(15, 0));
                    break;
                case 4:
                    startTime = dto.AppointmentDate.ToDateTime(new TimeOnly(15, 0));
                    endTime = dto.AppointmentDate.ToDateTime(new TimeOnly(17, 0));
                    break;
                default:
                    return BadRequest("Invalid slot. Please choose from slot 1 to 4.");
            }

            string meetUrl = await _meetService.CreateMeetingAsync(startTime, endTime);

            // Map Dto to domain
            var appointmentDomain = _mapper.Map<Appointment>(dto);
            appointmentDomain.MeetingUrl = meetUrl;
            appointmentDomain.Status = "Pending";
            appointmentDomain.CreatedAt = DateTime.Now;
            appointmentDomain.UpdatedAt = DateTime.Now;

            // Add new Appointment to DB
            appointmentDomain = await _service.CreateAsync(appointmentDomain);

            // Send email
            await SendEmail(appointmentDomain.AppointmentId, 1);

            // Map domain to Dto
            var appointmentDto = _mapper.Map<AppointmentDto>(appointmentDomain);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointmentDto.AppointmentId }, appointmentDto);
        }


        private async Task SendEmail([FromQuery] Guid appointmentId, int type)
        {
            var appointment = await _service.GetByIdAsync(appointmentId);

            var user = await _userService.FindAccountById(appointment.UserId);
            var consultant = await _userService.FindAccountById(appointment.ConsultantId);

            var emailService = new EmailHelper(_configuration);

            switch (type)
            {
                case 1:
                    await emailService.SendAppointmentBookingEmail(user.Email, user.FullName, consultant.FullName, appointment.AppointmentDate, appointment.Slot, appointment.Status, appointment.MeetingUrl);
                    break;
                case 2:
                    await emailService.SendAppointmentUpdateEmail(user.Email, user.FullName, consultant.FullName, appointment.AppointmentDate, appointment.Slot, appointment.Status, appointment.MeetingUrl);
                    break;
                case 3:
                    await emailService.SendAppointmentCancelEmail(user.Email, user.FullName, consultant.FullName, appointment.AppointmentDate, appointment.Slot, appointment.Status);
                    break;
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAppointment([FromRoute] Guid id, [FromBody] UpdateAppointmentRequest dto)
        {
            // Map Dto to domain
            var appointmentDomain = _mapper.Map<Appointment>(dto);
            appointmentDomain.UpdatedAt = DateTime.Now;

            // Update Appointment in DB
            appointmentDomain = await _service.UpdateAsync(id, appointmentDomain);

            if (appointmentDomain == null)
                return NotFound();

            // Send email
            await SendEmail(appointmentDomain.AppointmentId, 2);

            // Map domain to Dto
            var appointmentDto = _mapper.Map<AppointmentDto>(appointmentDomain);

            return Ok(appointmentDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> CancelAppointment([FromRoute] Guid id)
        {
            // Delete appointment in DB
            var isDeleted = await _service.DeleteAsync(id);

            if (!isDeleted)
                return NotFound();

            // Send email
            await SendEmail(id, 3);

            return Ok("Appointment is canceled");
        }
    }
}
