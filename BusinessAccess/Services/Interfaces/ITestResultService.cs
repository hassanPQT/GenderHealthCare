using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
	public interface ITestResultService
	{
		Task<IEnumerable<TestResult>> GetTestResultsByBookingIdAsync(Guid bookingId);
		Task<TestResult?> GetTestResultByIdAsync(Guid id);
		Task<TestResult> CreateTestResultAsync(TestResult testResult);
		Task<TestResult> UpdateTestResultAsync(TestResult testResult);
		Task<bool> DeleteTestResultAsync(Guid id);
	}
}
