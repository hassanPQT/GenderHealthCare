using BusinessAccess.Services.Interfaces;
using DataAccess.Repositories.Implements;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implements
{
	public class TestResultService : ITestResultService
	{
		private readonly ITestResultRepository _testResultRepository;

		public TestResultService(ITestResultRepository testResultRepository)
		{
			_testResultRepository = testResultRepository;
		}
		public async Task<IEnumerable<TestResult>> GetTestResultsByBookingIdAsync(Guid bookingId)
		{
			return await _testResultRepository.GetTestResultsByBookingIdAsync(bookingId);
		}
		public async Task<TestResult?> GetTestResultByIdAsync(Guid id)
		{
			return await _testResultRepository.GetTestResultByIdAsync(id);
		}
		public async Task<TestResult> CreateTestResultAsync(TestResult testResult)
		{
			return await _testResultRepository.CreateTestResultAsync(testResult);
		}
		public async Task<TestResult> UpdateTestResultAsync(TestResult testResult)
		{
			return await _testResultRepository.UpdateTestResultAsync(testResult);
		}
		public async Task<bool> DeleteTestResultAsync(Guid id)
		{
			return await _testResultRepository.DeleteTestResultAsync(id);
		}
	}
}
