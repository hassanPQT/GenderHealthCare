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
		public async Task<TestResult> UpdateTestResultAsync(Guid testResultId, TestResult testResult)
		{
			return await _testResultRepository.UpdateTestResultAsync(testResultId, testResult);
		}
		public async Task<bool> DeleteTestResultAsync(Guid id)
		{
			return await _testResultRepository.DeleteTestResultAsync(id);
		}

        public async Task<List<TestResult>> GetTestResultsByUserIdAsync(Guid userId)
        {
			return await _testResultRepository.GetTestResultsByUserIdAsync(userId);
        }

        async Task<List<TestResult>> ITestResultService.GetTestResultsByBookingIdAsync(Guid bookingId)
        {
			return await _testResultRepository.GetTestResultsByBookingIdAsync(bookingId);
        }

        public async Task<List<TestResult>> GetAllTestResultAsync()
        {
            return await _testResultRepository.GetAllTestResultAsync();
        }

        public async Task<List<TestResult>> GetTestResultsByStatusAsync(string status)
        {
			return await _testResultRepository.GetTestResultsByStatusAsync(status);
        }
    }
}
