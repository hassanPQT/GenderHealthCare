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
	public class MenstrualCycleRespository : IMenstrualCycleRespository
	{
		private readonly AppDbContext _context;

		public MenstrualCycleRespository(AppDbContext context)
		{
			_context = context;
		}

		public async Task AddAsync(MenstrualCycle menstrualCycle)
		{
			_context.MenstrualCycles.Add(menstrualCycle);
			await _context.SaveChangesAsync();
		}

		public async Task<List<MenstrualCycle>> GetByUserIdAsync(Guid userId)
		{
			return await _context.MenstrualCycles
			.Where(c => c.UserId == userId)
			.OrderByDescending(c => c.StartDate)
			.ToListAsync();
		}

		public async Task<MenstrualCycle?> GetLatestCycleAsync(Guid userId)
		{
			return await _context.MenstrualCycles
			.Where(c => c.UserId == userId)
			.OrderByDescending(c => c.StartDate)
			.FirstOrDefaultAsync();
		}
	}
}
