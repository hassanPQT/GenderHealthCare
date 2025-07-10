using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
	public interface IMedicalHistoryService
	{
		Task<List<MedicalHistory>> GetAllAsync();
		Task<MedicalHistory?> GetByIdAsync(Guid id);
		Task<MedicalHistory?> GetByUserIdAsync(Guid userId);
		Task<MedicalHistory> CreateAsync(MedicalHistory history);
		Task<MedicalHistory> UpdateAsync(MedicalHistory history);
		Task<bool> DeleteAsync(Guid id);
	}
}
