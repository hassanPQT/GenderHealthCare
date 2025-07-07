using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Interfaces
{
	public interface IMenstrualCycleRespository
	{
		Task AddAsync(MenstrualCycle menstrualCycle);
		Task<List<MenstrualCycle>> GetByUserIdAsync(Guid userId);

		Task<MenstrualCycle?> GetLatestCycleAsync(Guid userId);
	}
}
