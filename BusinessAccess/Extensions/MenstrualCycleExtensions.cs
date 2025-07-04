using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Extensions
{
	public class MenstrualCycleExtensions
	{
		public static CycleDayType GetDayType(DateOnly date, MenstrualCycle cycle)
		{
			if (cycle.StartDate == default || cycle.EndDate == default)
				return CycleDayType.Unknown;

			int cycleLength = cycle.CycleLength ?? 28;
			int periodLength = cycle.PeriodLength ?? 5;

			DateOnly ovulation = cycle.OvulationDate ?? cycle.StartDate.AddDays(cycleLength - 14);
			DateOnly fertileStart = ovulation.AddDays(-6);
			DateOnly highFertilityStart = ovulation.AddDays(-2);
			DateOnly fertileEnd = ovulation.AddDays(5);

			//take pill reminder check
			if (cycle.PillReminder != null && date == cycle.PillReminder)
				return CycleDayType.TakePill;
			// 1. Ngoài khoảng chu kỳ => Unknown
			if (date < cycle.StartDate || date > cycle.EndDate)
				return CycleDayType.Unknown;

			// 2. Menstruation
			if (date >= cycle.StartDate && date < cycle.StartDate.AddDays(periodLength))
				return CycleDayType.Menstruation;

			// 3. Fertile Start
			if (date == fertileStart)
				return CycleDayType.FertileStart;

			// 4. Fertile
			if (date > fertileStart && date < highFertilityStart)
				return CycleDayType.Fertile;

			// 5. High Fertility
			if (date >= highFertilityStart && date < ovulation)
				return CycleDayType.HighFertility;

			// 6. Ovulation
			if (date == ovulation)
				return CycleDayType.Ovulation;

			// 7. Relative Safe (sau rụng trứng nhưng còn trong vùng thụ thai)
			if (date > ovulation && date <= fertileEnd)
				return CycleDayType.RelativeSafe;

			// 8. Absolute Safe: trước FertileStart hoặc sau FertileEnd
			if (date < fertileStart || date > fertileEnd)
				return CycleDayType.AbsoluteSafe;

			// Fallback
			return CycleDayType.Unknown;
		}
	}
}
