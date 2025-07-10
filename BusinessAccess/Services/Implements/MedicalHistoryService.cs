using BusinessAccess.Services.Interfaces;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implements
{
	public class MedicalHistoryService : IMedicalHistoryService
	{
		private readonly IMedicalHistoryRepository _medicalHistoryRepository;

		public MedicalHistoryService(IMedicalHistoryRepository repository)
		{
			_medicalHistoryRepository = repository;
		}
		public async Task<MedicalHistory> CreateAsync(MedicalHistory history)
		{
			return await _medicalHistoryRepository.CreateAsync(history);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await _medicalHistoryRepository.DeleteAsync(id);
		}

		public async Task<List<MedicalHistory>> GetAllAsync()
		{
			return await _medicalHistoryRepository.GetAllAsync();
		}

		public Task<MedicalHistory?> GetByIdAsync(Guid id)
		{
			return _medicalHistoryRepository.GetByIdAsync(id);
		}

		public Task<MedicalHistory?> GetByUserIdAsync(Guid userId)
		{
			return _medicalHistoryRepository.GetByUserIdAsync(userId);
		}

		public Task<MedicalHistory> UpdateAsync(MedicalHistory history)
		{
			return _medicalHistoryRepository.UpdateAsync(history);
		}
	}
}
