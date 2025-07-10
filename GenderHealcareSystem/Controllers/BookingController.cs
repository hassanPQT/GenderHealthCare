
using BusinessAccess.Services.Implements;
using BusinessAccess.Services.Interfaces;
using DataAccess.Repositories.Implements;
using DataAccess.Repositories.Interfaces;
using GenderHealcareSystem.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace GenderHealcareSystem.Controllers
{

	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : Controller
	{
		private readonly ITestBookingService _testBookingService;
	    private readonly IServiceService _serviceService;
		private readonly ITestResultService _testResultService;
		private readonly IMedicalHistoryService _medicalHistoryService;
		private readonly IUserService _userService;
		private readonly ITestBookingServiceService _testBookingServiceService;

		public BookingController(ITestBookingService testBookingService, IServiceService serviceService, ITestResultService testResultService, IMedicalHistoryService medicalHistoryService, IUserService userService, ITestBookingServiceService testBookingServiceService)
		{
			_testBookingService = testBookingService;
			_serviceService = serviceService;
			_testResultService = testResultService;
			_medicalHistoryService = medicalHistoryService;
			_userService = userService;
			_testBookingServiceService = testBookingServiceService;
		}
		[HttpGet("services")]
		public async Task<IActionResult> GetAvailableServices()
		{
			var services = await _serviceService.GetAllAsync();
			return Ok(services);
		}

		[HttpPost("bookCreate")]
		public async Task<IActionResult> CreateBooking([FromBody] CreateBookingRequest request)
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null) return Unauthorized();

			if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
				return BadRequest("Invalid user ID in token");

			// Step 1: Kiểm tra medical history
			var existingMedicalHistory = await _medicalHistoryService.GetByUserIdAsync(userId);
			Guid medicalHistoryId;

			if (existingMedicalHistory != null)
			{
				medicalHistoryId = existingMedicalHistory.MedicalHistoryId;
			}
			else
			{
				var newMedicalHistory = new MedicalHistory
				{
					MedicalHistoryId = Guid.NewGuid(),
					UserId = userId,
					StartDate = DateTime.UtcNow,
				};

				await _medicalHistoryService.CreateAsync(newMedicalHistory);
				medicalHistoryId = newMedicalHistory.MedicalHistoryId;
			}

			// Step 2: Tính tổng tiền dịch vụ
			double totalServiceCost = 0;
			if (request.ServiceIds != null && request.ServiceIds.Any())
			{
				foreach (var serviceId in request.ServiceIds)
				{
					var service = await _serviceService.GetByIdAsync(serviceId);
					if (service != null && service.IsActive)
					{
						totalServiceCost += service.Price;
					}
				}
			}

			// Step 3: Tạo booking trước
			var booking = new TestBooking
			{
				TestBookingId = Guid.NewGuid(),
				UserId = userId,
				MedicalHistoryId = medicalHistoryId,
				BookingDate = request.BookingDate,
				Note = request.Note,
				Status = "PENDING",
				CreatedAt = DateTime.UtcNow
			};

			var createdBooking = await _testBookingService.AddBookingAsync(booking);

			// Step 4: Add dịch vụ đã chọn
			if (request.ServiceIds != null && request.ServiceIds.Any())
			{
				foreach (var serviceId in request.ServiceIds)
				{
					var bookingService = new DataAccess.Entities.TestBookingService
					{
						Id = Guid.NewGuid(),
						TestBookingId = createdBooking.TestBookingId,
						ServiceId = serviceId
					};

					await _testBookingServiceService.AddBookingServiceAsync(bookingService);
				}
			}
			var selectedServices = new List<object>();

			if (request.ServiceIds != null && request.ServiceIds.Any())
			{
				foreach (var serviceId in request.ServiceIds)
				{
					var service = await _serviceService.GetByIdAsync(serviceId);
					if (service != null && service.IsActive)
					{
						selectedServices.Add(new
						{
							service.ServiceId,
							service.ServiceName,
							service.Price,
							service.Description
						});
					}
				}
			}

			return Ok(new
			{
				Message = "Booking created successfully",
				BookingId = createdBooking.TestBookingId,
				BookingDate = createdBooking.BookingDate,
				SelectedServices = selectedServices,
				TotalPrice = totalServiceCost
				
			});
		}

		[HttpPut("cancel/{bookingId}")]
		public async Task<IActionResult> CancelBooking(Guid bookingId)
		{
			var booking = await _testBookingService.GetTestBookingByIdAsync(bookingId);
			if (booking == null)
				return NotFound("Booking not found");

			if (booking.Status != "PENDING")
				return BadRequest("Only PENDING bookings can be cancelled");

			booking.Status = "DELETED";
			await _testBookingService.UpdateTestBookingAsync(booking);

			return Ok(new
			{
				Message = "Booking cancelled successfully",
				BookingId = booking.TestBookingId
			});
		}

		[HttpPut("complete/{bookingId}")]
		public async Task<IActionResult> CompleteBooking(Guid bookingId)
		{
			var booking = await _testBookingService.GetTestBookingByIdAsync(bookingId);
			if (booking == null)
				return NotFound("Booking not found");

			if (booking.Status != "PENDING")
				return BadRequest("Only PENDING bookings can be Complete");

			booking.Status = "COMPLETE";
			await _testBookingService.UpdateTestBookingAsync(booking);

			return Ok(new
			{
				Message = "Booking cancelled successfully",
				BookingId = booking.TestBookingId
			});
		}

		[HttpPut("update")]
		public async Task<IActionResult> UpdateBooking([FromBody] UpdateBookingRequest request)
		{
			var booking = await _testBookingService.GetTestBookingByIdAsync(request.BookingId);
			if (booking == null)
				return NotFound("Booking not found");

			if (booking.Status != "PENDING")
				return BadRequest("Only PENDING bookings can be updated");

			// Cập nhật thông tin booking
			booking.BookingDate = request.BookingDate;
			booking.Note = request.Note;

			await _testBookingService.UpdateTestBookingAsync(booking);

			// Xóa toàn bộ dịch vụ cũ
			await _testBookingServiceService.DeleteByBookingIdAsync(booking.TestBookingId);

			// Thêm lại dịch vụ mới
			if (request.ServiceIds != null && request.ServiceIds.Any())
			{
				foreach (var serviceId in request.ServiceIds)
				{
					var bookingService = new DataAccess.Entities.TestBookingService
					{
						Id = Guid.NewGuid(),
						TestBookingId = booking.TestBookingId,
						ServiceId = serviceId
					};
					await _testBookingServiceService.AddBookingServiceAsync(bookingService);
				}
			}

			return Ok(new
			{
				Message = "Booking updated successfully",
				BookingId = booking.TestBookingId,
				BookingDate = booking.BookingDate,
				TotalServiceCost = booking.TestBookingServices.Sum(ts => ts.Service.Price)
			});
		}

		[HttpGet("my-bookings")]
		[Authorize]
		public async Task<IActionResult> GetMyBookings()
		{
			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized();

			if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
				return BadRequest("Invalid user ID in token");

			var bookings = await _testBookingService.GetBookingsByUserIdAsync(userId);

			var bookingSummaries = new List<object>();

			foreach (var booking in bookings)
			{
				// Lấy danh sách dịch vụ đã đặt trong booking này
				var services = await _testBookingServiceService.GetServicesByBookingIdAsync(booking.TestBookingId);
				double totalPrice = services.Sum(s => s.Price);
				int serviceCount = services.Count();

				bookingSummaries.Add(new
				{
					BookingId = booking.TestBookingId,
					BookingDate = booking.BookingDate,
					Status = booking.Status,
					TotalPrice = totalPrice,
					ServiceCount = serviceCount
				});
			}

			return Ok(bookingSummaries);
		}

		[HttpGet("my-bookings/{id}")]
		[Authorize]
		public async Task<IActionResult> GetBookingDetail(Guid id)
		{
			var booking = await _testBookingService.GetTestBookingWithDetailsAsync(id);
			if (booking == null)
				return NotFound("Booking not found");

			var services = booking.TestBookingServices.Select(tbs => new
			{
				ServiceId = tbs.ServiceId,
				ServiceName = tbs.Service.ServiceName,
				Price = tbs.Service.Price,
				Description = tbs.Service.Description
			});

			double totalPrice = services.Sum(s => s.Price);

			return Ok(new
			{
				BookingId = booking.TestBookingId,
				BookingDate = booking.BookingDate,
				Status = booking.Status,
				Note = booking.Note,
				Services = services,
				TotalPrice = totalPrice
			});
		}

		[HttpGet("bookings-by-status")]
		[Authorize]
		public async Task<IActionResult> GetBookingsByStatus([FromQuery] string status)
		{
			if (string.IsNullOrWhiteSpace(status))
				return BadRequest("Status is required.");

			var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
				return Unauthorized();

			if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
				return BadRequest("Invalid user ID in token");

			// Gọi service để lấy danh sách booking theo status
			var bookings = await _testBookingService.GetBookingsByStatusAsync(userId, status.ToUpper());

			var response = bookings.Select(booking => new
			{
				BookingId = booking.TestBookingId,
				BookingDate = booking.BookingDate,
				Status = booking.Status,
				Note = booking.Note,
				ServiceCount = booking.TestBookingServices?.Count ?? 0,
				TotalPrice = booking.TestBookingServices?.Sum(tbs => tbs.Service?.Price ?? 0) ?? 0
			});

			return Ok(response);
		}
	}
}
