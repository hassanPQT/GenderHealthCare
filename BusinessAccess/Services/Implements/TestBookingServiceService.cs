using BusinessAccess.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implements
{
	public class TestBookingServiceService : ITestBookingServiceService
	{
		private readonly ITestBookingServiceRespository _TestBookingRespository;

		public TestBookingServiceService(ITestBookingServiceRespository TestBookingService)
		{
			_TestBookingRespository = TestBookingService;
		}

		public async Task<DataAccess.Entities.TestBookingService> AddBookingServiceAsync(DataAccess.Entities.TestBookingService bookingService)
		{
			return await _TestBookingRespository.AddBookingServiceAsync(bookingService);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await _TestBookingRespository.DeleteAsync(id);
		}

		public async Task DeleteByBookingIdAsync(Guid bookingId)
		{
			 await _TestBookingRespository.DeleteByBookingIdAsync(bookingId);
		}

		public async Task<IEnumerable<Service>> GetServicesByBookingIdAsync(Guid bookingId)
		{
			return await _TestBookingRespository.GetServicesByBookingIdAsync(bookingId);
		}

		public async Task<DataAccess.Entities.TestBookingService> UpdateAsync(DataAccess.Entities.TestBookingService tbs)
		{
			return await _TestBookingRespository.UpdateAsync(tbs);
		}
	}
}
