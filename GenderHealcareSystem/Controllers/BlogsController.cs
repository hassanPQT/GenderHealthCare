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
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _service;
        private readonly IMapper _mapper;

        public BlogsController(IBlogService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogsDomain = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BlogDto>>(blogsDomain));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var blogDomain = await _service.GetByIdAsync(id);
            if (blogDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogDto>(blogDomain));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddBlogRequest addBlogRequestDto)
        {
            var blogDomain = _mapper.Map<Blog>(addBlogRequestDto);
            if (blogDomain.UserId != Guid.Empty && await _service.GetByIdAsync(addBlogRequestDto.UserId) == null)
            {
                return BadRequest(new { error = "The UserId does not exist." });
            }
            blogDomain = await _service.CreateAsync(blogDomain);
            var blogDto = _mapper.Map<BlogDto>(blogDomain);
            return CreatedAtAction(nameof(GetById), new { id = blogDto.Author.UserId }, blogDto);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBlogRequest updateBlogRequest)
        {
            var blogDomain = _mapper.Map<Blog>(updateBlogRequest);
            blogDomain = await _service.UpdateAsync(id, blogDomain);
            if (blogDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogDto>(blogDomain));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var blogDomain = await _service.DeleteAsync(id);
            if (blogDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BlogDto>(blogDomain));
        }
    }
}
