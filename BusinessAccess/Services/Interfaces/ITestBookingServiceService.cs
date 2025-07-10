
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
	public interface ITestBookingServiceService
	{
		Task<TestBookingService> AddBookingServiceAsync(TestBookingService bookingService);
		Task<TestBookingService> UpdateAsync(TestBookingService tbs);
		Task<bool> DeleteAsync(Guid id);
		Task DeleteByBookingIdAsync(Guid bookingId);

		Task<IEnumerable<Service>> GetServicesByBookingIdAsync(Guid bookingId);
	}
}
