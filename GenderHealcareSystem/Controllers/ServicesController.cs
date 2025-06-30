using AutoMapper;
using BusinessAccess.Services.Interfaces;
using GenderHealcareSystem.CustomActionFilters;
using GenderHealcareSystem.DTO;
using GenderHealcareSystem.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;
        private readonly IMapper _mapper;

        public ServicesController(IServiceService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        //GET ALL SERVICES
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get services from DB - domain model
            var servicesDomain = await _service.GetAllAsync();

            // Convert domain models to DTOs
            var serviceDtos = _mapper.Map<IEnumerable<ServiceDto>>(servicesDomain);

            return Ok(serviceDtos);

        }

        //GET SERVICE BY ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get service from database - domain model
            var serviceDomain = await _service.GetByIdAsync(id);

            if (serviceDomain == null)
                return NotFound();

            //Covert domain model to dto
            var serviceDto = _mapper.Map<ServiceDto>(serviceDomain);

            return Ok(serviceDto);
        }

        //CREATE NEW SERVICE
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddServiceRequest dto)
        {
            //Convert Dto to domain model
            var serviceDomain = _mapper.Map<Service>(dto);

            //Add domain model to DB
            serviceDomain = await _service.CreateAsync(serviceDomain);

            //Convert domain model to Dto

            var serviceDto = _mapper.Map<ServiceDto>(serviceDomain);

            return CreatedAtAction(nameof(GetById), new { id = serviceDto.ServiceId }, serviceDto);
        }

        //UPDATE EXIST SERVICE
        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceRequest dto)
        {
            //Convert Dto to domain model
            var serviceDomain = _mapper.Map<Service>(dto);

            //Update service in DB
            serviceDomain = await _service.UpdateAsync(id, serviceDomain);

            if (serviceDomain == null)
                return NotFound();

            //Convert Domain model to Dto
            var serviceDto = _mapper.Map<ServiceDto>(serviceDomain);

            return Ok(serviceDto);
        }

        //DELETE EXIST SERVICE
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            // Delete from DB by Id
            var isDeleted = await _service.DeleteAsync(id);

            if (!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
