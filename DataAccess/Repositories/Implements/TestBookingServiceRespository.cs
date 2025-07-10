using DataAccess.DBContext;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implements
{
	public class TestBookingServiceRespository : ITestBookingServiceRespository
	{
		private readonly AppDbContext _context;

		public TestBookingServiceRespository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<TestBookingService> AddBookingServiceAsync(TestBookingService bookingService)
		{
			_context.TestBookingServices.Add(bookingService);
			await _context.SaveChangesAsync();
			return bookingService;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var existing = await _context.TestBookingServices.FindAsync(id);
			if (existing == null) return false;

			_context.TestBookingServices.Remove(existing);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task DeleteByBookingIdAsync(Guid bookingId)
		{
			var items = _context.TestBookingServices.Where(x => x.TestBookingId == bookingId);
			_context.TestBookingServices.RemoveRange(items);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Service>> GetServicesByBookingIdAsync(Guid bookingId)
		{
			var services = await _context.TestBookingServices
				.Where(bs => bs.TestBookingId == bookingId)
				.Join(
					_context.Services,
					bs => bs.ServiceId,
					s => s.ServiceId,
					(bs, s) => s
				)
				.ToListAsync();

					return services;
		}

		public async Task<TestBookingService> UpdateAsync(TestBookingService tbs)
		{
			_context.TestBookingServices.Update(tbs);
			await _context.SaveChangesAsync();
			return tbs;
		}
	}
}
