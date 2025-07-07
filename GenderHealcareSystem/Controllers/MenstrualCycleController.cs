using BusinessAccess.Helpers;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using GenderHealcareSystem.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GenderHealcareSystem.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class MenstrualCycleController : Controller
	{
		private readonly IMenstrualCycleService _menstrualCycleService;
		

		public MenstrualCycleController(IMenstrualCycleService menstrualCycleService)
		{
			_menstrualCycleService = menstrualCycleService;
			
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateCycle([FromBody] CycleDto model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userIdClaim == null) return Unauthorized();
			var userId = Guid.Parse(userIdClaim);

			if (model.CycleType == "regular")
			{
				if (model.RegularCycle == null)
					return BadRequest("Dữ liệu chu kỳ đều bị thiếu.");

				var cycle = model.RegularCycle;

				var menstrualCycle = new MenstrualCycle
				{
					UserId = userId,
					StartDate = DateOnly.FromDateTime(cycle.StartDate),
					EndDate = DateOnly.FromDateTime(cycle.EndDate),
					PeriodLength = cycle.PeriodLength,
					Note = "Regular"
				};

				_menstrualCycleService.CalculateCycleInfo(menstrualCycle);
				await _menstrualCycleService.AddAsync(menstrualCycle);

				return Ok(new { message = "Thêm chu kỳ đều thành công." });
			}
			else if (model.CycleType == "irregular")
			{
				if (model.IrregularCycle == null)
					return BadRequest("Dữ liệu chu kỳ không đều bị thiếu.");

				var cycle = model.IrregularCycle;

				var menstrualCycle = new MenstrualCycle
				{
					UserId = userId,
					StartDate = DateOnly.FromDateTime(cycle.StartDate),
					PeriodLength = cycle.PeriodLength,
					Note = "irregular"
				};

				_menstrualCycleService.CalculateIrregularCycleInfo(
					menstrualCycle,
					cycle.ShortestCycleLength,
					cycle.LongestCycleLength
				);
				await _menstrualCycleService.AddAsync(menstrualCycle);

				return Ok(new { message = "Thêm chu kỳ không đều thành công." });
			}
			else
			{
				return BadRequest("Loại chu kỳ không hợp lệ.");
			}
		}

		[HttpGet("latest")]
		public async Task<IActionResult> GetLatestCycle()
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (userIdClaim == null) return Unauthorized();
			var userId = Guid.Parse(userIdClaim);

			var latestCycle = await _menstrualCycleService.GetLatestCycleAsync(userId);
			if (latestCycle == null)
			{
				return NotFound("Không có dữ liệu chu kỳ.");
			}

			return Ok(latestCycle);
		}
	}
}

