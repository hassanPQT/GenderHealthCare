using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
	public interface ITestResultRepository
	{

		Task<TestResult> CreateTestResultAsync(TestResult testResult);
		Task<TestResult?> GetTestResultByIdAsync(Guid testResultId);
		Task<List<TestResult>> GetTestResultsByUserIdAsync(Guid userId);
		Task<List<TestResult>> GetTestResultsByBookingIdAsync(Guid bookingId);
		Task<List<TestResult>> GetAllTestResultAsync();
		Task<TestResult> UpdateTestResultAsync(Guid testResultId, TestResult testResult);
		Task<bool> DeleteTestResultAsync(Guid testResultId);
		Task<List<TestResult>> GetTestResultsByStatusAsync(string status);
	}
}
