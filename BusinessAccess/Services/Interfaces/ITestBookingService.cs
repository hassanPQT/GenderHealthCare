using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
	public interface ITestBookingService
	{
		Task<TestBooking> AddBookingAsync(TestBooking booking);
		Task<TestBooking> GetTestBookingByIdAsync(Guid testBookingId);
		Task<IEnumerable<TestBooking>> GetAllTestBookingsAsync();
		Task<TestBooking> GetTestBookingWithDetailsAsync(Guid testBookingId);
		Task<TestBooking> UpdateTestBookingAsync(TestBooking booking);
		Task<bool> DeleteTestBookingAsync(Guid testBookingId);
		Task<MedicalHistory?> GetMedicalHistoryByBookingIdAsync(Guid testBookingId);
		Task<IEnumerable<TestBooking>> GetBookingsByUserIdAsync(Guid userId);
		Task<MedicalHistory?> GetMedicalHistoryByUserIdAsync(Guid userId);
		Task<TestBooking?> GetBookingByUserIdAndDateAsync(Guid userId, DateTime bookingDate);
		Task<IEnumerable<TestBooking>> GetBookingsByStatusAsync(Guid userId, string status);
	}
}
