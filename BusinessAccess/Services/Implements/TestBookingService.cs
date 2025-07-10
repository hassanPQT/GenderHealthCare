using BusinessAccess.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implements
{
	public class TestBookingService : ITestBookingService
	{
		private readonly ITestBookingRepository _testBookingRespository;

		public TestBookingService(ITestBookingRepository testBookingRespository)
		{
			_testBookingRespository = testBookingRespository ;
		}

		public async Task<TestBooking> AddBookingAsync(TestBooking booking)
		{
			return await _testBookingRespository.AddBookingAsync(booking);
		}

		public async Task<bool> DeleteTestBookingAsync(Guid testBookingId)
		{
			return await _testBookingRespository.DeleteTestBookingAsync(testBookingId);
		}

		public async Task<IEnumerable<TestBooking>> GetAllTestBookingsAsync()
		{
			return await _testBookingRespository.GetAllTestBookingsAsync();
		}

		public async Task<TestBooking?> GetBookingByUserIdAndDateAsync(Guid userId, DateTime bookingDate)
		{
			return await _testBookingRespository.GetBookingByUserIdAndDateAsync(userId, bookingDate);
		}

		public async Task<IEnumerable<TestBooking>> GetBookingsByStatusAsync(Guid userId, string status)
		{
			return await _testBookingRespository.GetBookingsByStatusAsync(userId, status);
		}

		public async Task<IEnumerable<TestBooking>> GetBookingsByUserIdAsync(Guid userId)
		{
			return await _testBookingRespository.GetBookingsByUserIdAsync(userId);
		}

		public async Task<MedicalHistory?> GetMedicalHistoryByBookingIdAsync(Guid testBookingId)
		{
			return await _testBookingRespository.GetMedicalHistoryByBookingIdAsync(testBookingId);
		}

		public async Task<MedicalHistory?> GetMedicalHistoryByUserIdAsync(Guid userId)
		{
			return await _testBookingRespository.GetMedicalHistoryByUserIdAsync(userId);
		}

		public async Task<TestBooking> GetTestBookingByIdAsync(Guid testBookingId)
		{
			return await _testBookingRespository.GetTestBookingByIdAsync(testBookingId);
		}

		public async Task<TestBooking> GetTestBookingWithDetailsAsync(Guid testBookingId)
		{
			return await _testBookingRespository.GetTestBookingWithDetailsAsync(testBookingId);
		}

		public Task<TestBooking> UpdateTestBookingAsync(TestBooking booking)
		{
			return _testBookingRespository.UpdateTestBookingAsync(booking);
		}
	}
}
