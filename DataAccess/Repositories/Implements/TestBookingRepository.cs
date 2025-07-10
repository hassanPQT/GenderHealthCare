using DataAccess.DBContext;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implements
{
	public class TestBookingRepository : ITestBookingRepository
	{
		private readonly AppDbContext _context;

		public TestBookingRepository(AppDbContext Context)
		{
			_context = Context;
		}
		public async Task<TestBooking> AddBookingAsync(TestBooking booking)
		{

			try
			{
				_context.TestBookings.Add(booking);
				await _context.SaveChangesAsync();
				return booking;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error creating test booking: {ex.Message}");
			}
		}

		public async Task<bool> DeleteTestBookingAsync(Guid testBookingId)
		{
			try
			{
				var testBooking = await _context.TestBookings.FindAsync(testBookingId);
				if (testBooking == null)
					return false;

				_context.TestBookings.Remove(testBooking);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error deleting test booking: {ex.Message}");
			}
		}

		public async Task<IEnumerable<TestBooking>> GetAllTestBookingsAsync()
		{
			try
			{
				return await _context.TestBookings
					.Include(tb => tb.User)
					.Include(tb => tb.BookingStaff)
					.Include(tb => tb.TestBookingServices)
					   .ThenInclude(tr => tr.TestResults)
					.OrderByDescending(tb => tb.BookingDate)
					.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving all test bookings: {ex.Message}");
			}
		}

		public async Task<TestBooking?> GetBookingByUserIdAndDateAsync(Guid userId, DateTime bookingDate)
		{
			return await _context.TestBookings
						.Where(tb => tb.UserId == userId &&
									 tb.BookingDate == bookingDate) 
						.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<TestBooking>> GetBookingsByStatusAsync(Guid userId, string status)
		{
			return await _context.TestBookings
				.Include(tb => tb.TestBookingServices)
					.ThenInclude(tbs => tbs.Service)
				.Where(tb => tb.UserId == userId && tb.Status == status)
				.ToListAsync();
		}

		public async Task<IEnumerable<TestBooking>> GetBookingsByUserIdAsync(Guid userId)
		{
			return await _context.TestBookings
				.Where(tb => tb.UserId == userId)
				.OrderByDescending(tb => tb.BookingDate)
				.ToListAsync();
		}

		public async Task<MedicalHistory?> GetMedicalHistoryByBookingIdAsync(Guid testBookingId)
		{
			try
			{
				var booking = await _context.TestBookings
					.Include(tb => tb.MedicalHistory)
					.FirstOrDefaultAsync(tb => tb.TestBookingId == testBookingId);

				// Trả về medical history nếu có booking
				return booking?.MedicalHistory;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving medical history for booking ID {testBookingId}: {ex.Message}");
			}
		}

		public async Task<MedicalHistory?> GetMedicalHistoryByUserIdAsync(Guid userId)
		{
			try
			{
				return await _context.MedicalHistories
					.Include(mh => mh.User)
					.FirstOrDefaultAsync(mh => mh.UserId == userId);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving medical history for user: {ex.Message}");
			}
		}

		public async Task<TestBooking> GetTestBookingByIdAsync(Guid testBookingId)
		{
			try
			{
				return await _context.TestBookings
					.Include(tb => tb.User)
					.Include(tb => tb.BookingStaff)
					.Include(tb => tb.TestBookingServices)
						.ThenInclude(tbs => tbs.TestResults) 
					.FirstOrDefaultAsync(tb => tb.TestBookingId == testBookingId);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving test booking: {ex.Message}");
			}
		}

		public async Task<TestBooking> GetTestBookingWithDetailsAsync(Guid testBookingId)
		{
			try
			{
				return await _context.TestBookings
					.Include(tb => tb.User)
					.Include(tb => tb.BookingStaff)
					.Include(tb => tb.TestBookingServices)
						.ThenInclude(tbs => tbs.Service) // ✅ Bổ sung Service
					.Include(tb => tb.TestBookingServices)
						.ThenInclude(tbs => tbs.TestResults)
					.FirstOrDefaultAsync(tb => tb.TestBookingId == testBookingId);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving test booking with details: {ex.Message}");
			}
		}

		public async Task<TestBooking> UpdateTestBookingAsync(TestBooking booking)
		{
			try
			{
				_context.TestBookings.Update(booking);
				await _context.SaveChangesAsync();
				return booking;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error updating test booking: {ex.Message}");
			}
		}
	}
}
