using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
	public interface ITestBookingServiceRespository
	{
		Task<TestBookingService> AddBookingServiceAsync(TestBookingService bookingService);
		Task<TestBookingService> UpdateAsync(TestBookingService tbs);
		Task<bool> DeleteAsync(Guid id);
		Task<IEnumerable<Service>> GetServicesByBookingIdAsync(Guid bookingId);
		Task DeleteByBookingIdAsync(Guid bookingId);
	}
}
