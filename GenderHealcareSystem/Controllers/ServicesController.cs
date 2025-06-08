using BusinessAccess.Services.Interfaces;
using GenderHealcareSystem.CustomActionFilters;
using GenderHealcareSystem.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenderHealcareSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _service;

        public ServicesController(IServiceService service)
        {
            _service = service;
        }

        //GET ALL SERVICES
        [HttpGet]
        public async Task<IActionResult> GetAdd()
        {
            //Get services from DB - domain model
            var servicesDomain = await _service.GetAllAsync();

            // Convert domain models to DTOs
            var serviceDtos = new List<ServiceDto>();
            foreach (var service in servicesDomain)
            {
                var serviceDto = new ServiceDto
                {
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    Description = service.Description,
                    Price = service.Price,
                    IsActive = service.IsActive,
                };
                serviceDtos.Add(serviceDto);
            }

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
            var serviceDto = new ServiceDto
            {
                ServiceId = serviceDomain.ServiceId,
                ServiceName = serviceDomain.ServiceName,
                Description = serviceDomain.Description,
                Price = serviceDomain.Price,
                IsActive = serviceDomain.IsActive,
            };

            return Ok(serviceDto);
        }

        //CREATE NEW SERVICE
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddServiceRequestDto dto)
        {
            //Convert Dto to domain model
            var serviceDomain = new Service
            {
                ServiceName = dto.ServiceName,
                Description = dto.Description,
                Price = dto.Price,
                IsActive = dto.IsActive,
            };

            //Add domain model to DB
            serviceDomain = await _service.CreateAsync(serviceDomain);

            //Convert domain model to Dto

            var serviceDto = new ServiceDto
            {
                ServiceId = serviceDomain.ServiceId,
                ServiceName = serviceDomain.ServiceName,
                Description = dto.Description,
                Price = dto.Price,
                IsActive = dto.IsActive,
            };

            return CreatedAtAction(nameof(GetById), new { id = serviceDto.ServiceId }, serviceDto);
        }

        //UPDATE EXIST SERVICE
        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateServiceRequestDto dto)
        {
            //Convert Dto to domain model
            var serviceDomain = new Service
            {
                ServiceName = dto.ServiceName,
                Description = dto.Description,
                Price = dto.Price,
                IsActive = dto.IsActive,
            };

            //Update service in DB
            serviceDomain = await _service.UpdateAsync(id, serviceDomain);

            if (serviceDomain == null)
                return NotFound();

            //Convert Domain model to Dto
            var serviceDto = new ServiceDto
            {
                ServiceId = serviceDomain.ServiceId,
                ServiceName = serviceDomain.ServiceName,
                Description = dto.Description,
                Price = dto.Price,
                IsActive = dto.IsActive,
            };

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
