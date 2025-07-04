using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interfaces
{
	public interface IMenstrualCycleService
	{
		Task AddAsync(MenstrualCycle menstrualCycle);
		Task<List<MenstrualCycle>> GetByUserIdAsync(Guid userId);
		List<(DateOnly Date, CycleDayType DayType)> GetExtendedCycleDayTypes(MenstrualCycle latestCycle, DateTime startRange, DateTime endRange);
		void CalculateCycleInfo(MenstrualCycle cycle);
		Task<MenstrualCycle?> GetLatestCycleAsync(Guid userId);
		List<(DateOnly Date, CycleDayType DayType, string Description)> GetIrregularCycleCalendar(MenstrualCycle latestCycle, DateTime startRange, DateTime endRange);
		void CalculateIrregularCycleInfo(MenstrualCycle cycle, int shortestCycleLength, int longestCycleLength);
	}
}
