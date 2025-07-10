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
	public class MedicalHistoryRepository : IMedicalHistoryRepository
	{
		private readonly AppDbContext _context;

		public MedicalHistoryRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<MedicalHistory> CreateAsync(MedicalHistory history)
		{
			history.MedicalHistoryId = Guid.NewGuid();
			_context.MedicalHistories.Add(history);
			await _context.SaveChangesAsync();
			return history;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var history = await _context.MedicalHistories.FindAsync(id);
			if (history == null) return false;
			_context.MedicalHistories.Remove(history);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<List<MedicalHistory>> GetAllAsync()
		{
			return await _context.MedicalHistories.Include(m => m.User).ToListAsync();
		}

		public async Task<MedicalHistory?> GetByIdAsync(Guid id)
		{
			return await _context.MedicalHistories.Include(m => m.User)
			.FirstOrDefaultAsync(m => m.MedicalHistoryId == id);
		}

		public async Task<MedicalHistory?> GetByUserIdAsync(Guid userId)
		{
			return await _context.MedicalHistories
			.FirstOrDefaultAsync(m => m.UserId == userId);
		}

		public async Task<MedicalHistory> UpdateAsync(MedicalHistory history)
		{
			_context.MedicalHistories.Update(history);
			await _context.SaveChangesAsync();
			return history;
		}
	}
}
