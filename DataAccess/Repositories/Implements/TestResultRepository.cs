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
	public class TestResultRepository : ITestResultRepository
	{
		private readonly AppDbContext _context;

		public TestResultRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<TestResult> CreateTestResultAsync(TestResult testResult)
		{
			try
			{
				_context.TestResults.Add(testResult);
				await _context.SaveChangesAsync();
				return testResult;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error creating test booking: {ex.Message}");
			}
		}

		public async Task<bool> DeleteTestResultAsync(Guid testResultId)
		{
			try
			{
				var testResult = await _context.TestResults.FindAsync(testResultId);
				if (testResult == null)
					return false;

				_context.TestResults.Remove(testResult);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error deleting test result: {ex.Message}");
			}
		}

		public async Task<List<TestResult>> GetAllTestResultAsync()
		{
			try
			{
				return await _context.TestResults
					.Include(tr => tr.TestBookingService)
						.ThenInclude(tbs => tbs.TestBooking)
					.Include(tr => tr.TestBookingService)
						.ThenInclude(tbs => tbs.Service)
					.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving all test results: {ex.Message}");
			}
		}

		public  async Task<TestResult?> GetTestResultByIdAsync(Guid testResultId)
		{
			try
			{
				return await _context.TestResults
					.Include(tr => tr.TestBookingService)
						.ThenInclude(tbs => tbs.TestBooking)
					.FirstOrDefaultAsync(tr => tr.TestResultId == testResultId);
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving test result by ID: {ex.Message}");
			}
		}

		public async Task<List<TestResult>> GetTestResultsByBookingIdAsync(Guid bookingId)
		{
			try
			{
				return await _context.TestResults
					.Include(tr => tr.TestBookingService)
						.ThenInclude(tbs => tbs.TestBooking)
					.Where(tr => tr.TestBookingService.TestBooking.TestBookingId == bookingId)
					.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving test results by booking ID: {ex.Message}");
			}
		}

		public  async Task<List<TestResult>> GetTestResultsByStatusAsync(string status)
		{
			try
			{
				bool statusBool = status.ToLower() == "completed" || status.ToLower() == "true";

				return await _context.TestResults
					.Include(tr => tr.TestBookingService)
						.ThenInclude(tbs => tbs.TestBooking)
					.Include(tr => tr.TestBookingService)
						.ThenInclude(tbs => tbs.Service)
					.Where(tr => tr.Status == statusBool)
					.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception($"Error retrieving test results by status: {ex.Message}");
			}
		}

		public async Task<List<TestResult>> GetTestResultsByUserIdAsync(Guid userId)
		{
			try
			{
				return await _context.TestResults
					.Include(tr => tr.TestBookingService)
						.ThenInclude(tbs => tbs.TestBooking)
					.Where(tr => tr.TestBookingService.TestBooking.UserId == userId)
					.ToListAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		public async Task<TestResult> UpdateTestResultAsync(TestResult testResult)
		{
			try
			{
				_context.TestResults.Update(testResult);
				await _context.SaveChangesAsync();
				return testResult;
			}
			catch (Exception ex)
			{
				throw new Exception($"Error updating test booking: {ex.Message}");
			}
		}
	}
}
