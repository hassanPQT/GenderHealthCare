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
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public BlogsController(IBlogService blogService, IUserService userService, IMapper mapper)
        {
            _blogService = blogService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogsDomain = await _blogService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BlogDto>>(blogsDomain));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var blogDomain = await _blogService.GetByIdAsync(id);
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
            blogDomain.BlogId = Guid.NewGuid();
            if (blogDomain.UserId != Guid.Empty && await _userService.FindAccountById(addBlogRequestDto.UserId) == null)
            {
                return BadRequest(new { error = "The UserId does not exist." });
            }
            blogDomain = await _blogService.CreateAsync(blogDomain);

            var blogDto = new BlogDto
            {
                BlogId = blogDomain.BlogId,
                Tittle = blogDomain.Tittle,
                Content = blogDomain.Content,
                PublistDate = blogDomain.PublistDate,
                Author = _mapper.Map<UserDto>(blogDomain.User)
            };

            return CreatedAtAction(nameof(GetById), new { id = blogDto.Author.UserId }, blogDto);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBlogRequest updateBlogRequest)
        {
            var blogDomain = _mapper.Map<Blog>(updateBlogRequest);
            blogDomain = await _blogService.UpdateAsync(id, blogDomain);
            if (blogDomain == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BlogDto>(blogDomain));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var blogDomain = await _blogService.DeleteAsync(id);
            if (blogDomain == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BlogDto>(blogDomain));
        }
    }
}
