using AutoMapper;
using BusinessAccess.Services.Interfaces;
using GenderHealcareSystem.DTO;
using GenderHealcareSystem.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultController : ControllerBase
    {
        private readonly ITestResultService _service;
        private readonly IMapper _mapper;

        public TestResultController(ITestResultService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResult()
        {
            // Get all from DB
            var resultDomain = await _service.GetAllTestResultAsync();

            // Convert domain to Dto
            var resultDto = _mapper.Map<IEnumerable<TestResultDto>>(resultDomain);

            return Ok(resultDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetResultById([FromRoute] Guid id)
        {
            // Get all from DB
            var resultDomain = await _service.GetTestResultByIdAsync(id);

            if (resultDomain == null)
                return NotFound();

            // Convert domain to Dto
            var resultDto = _mapper.Map<TestResultDto>(resultDomain);

            return Ok(resultDto);
        }

        [HttpGet("/user")]
        public async Task<IActionResult> GetResultByUserId([FromQuery] Guid userId)
        {
            // Get all from DB
            var resultDomain = await _service.GetTestResultsByUserIdAsync(userId);

            if (resultDomain == null)
                return NotFound();

            // Convert domain to Dto
            var resultDto = _mapper.Map<TestResultDto>(resultDomain);

            return Ok(resultDto);
        }

        [HttpGet("/booking")]
        public async Task<IActionResult> GetResultByBookingId([FromQuery] Guid bookingId)
        {
            // Get all from DB
            var resultDomain = await _service.GetTestResultsByBookingIdAsync(bookingId);

            if (resultDomain == null)
                return NotFound();

            // Convert domain to Dto
            var resultDto = _mapper.Map<TestResultDto>(resultDomain);

            return Ok(resultDto);
        }

        [HttpGet("/status")]
        public async Task<IActionResult> GetResultByStatus([FromQuery] string status)
        {
            // Get all from DB
            var resultDomain = await _service.GetTestResultsByStatusAsync(status);

            if (resultDomain == null)
                return NotFound();

            // Convert domain to Dto
            var resultDto = _mapper.Map<TestResultDto>(resultDomain);

            return Ok(resultDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddResult([FromBody] AddResultDto dto)
        {
            // COnvert DTo to domain
            var resultDomain = _mapper.Map<TestResult>(dto);
            resultDomain.TestDate = DateTime.Now;
            resultDomain.Status = false;
            resultDomain.CreatedAt = DateTime.Now;
            resultDomain.UpdatedAt = DateTime.Now;

            // Add result in DB
            resultDomain = await _service.CreateTestResultAsync(resultDomain);

            //Convert domain to Dto
            var resultDto = _mapper.Map<TestResultDto>(resultDomain);
            resultDto.TestBookingService = _mapper.Map<TestBookingServiceDto>(resultDto.TestBookingService);

            return CreatedAtAction(nameof(GetResultById), new { id = resultDomain.TestResultId }, resultDomain);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTestResult([FromRoute] Guid id, UpdateResultDto dto)
        {
            //COnvert dto to domain
            var resultDomain = _mapper.Map<TestResult>(dto);
            resultDomain.UpdatedAt = DateTime.Now;

            // Update in DB
            resultDomain = await _service.UpdateTestResultAsync(id, resultDomain);

            if (resultDomain == null) return NotFound();

            // Map domain to Dto
            var resultDto = _mapper.Map<TestResultDto>(resultDomain);

            return Ok(resultDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteResult([FromRoute] Guid id)
        {
            // Delete in DB
            var isDeleted = await _service.DeleteTestResultAsync(id);

            if (!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
