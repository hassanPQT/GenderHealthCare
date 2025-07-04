using BusinessAccess.Helpers;
using BusinessAccess.Services.Interfaces;
using DataAccess.Entities;
using DataAccess.Enums;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implements
{
	public class MenstrualCycleService : IMenstrualCycleService
	{
		private readonly IMenstrualCycleRespository _menstrualCycleRepository;
		public MenstrualCycleService(IMenstrualCycleRespository menstrualCycleRepository)
		{
			_menstrualCycleRepository = menstrualCycleRepository;
		}

		public async Task AddAsync(MenstrualCycle menstrualCycle)
		{
			await _menstrualCycleRepository.AddAsync(menstrualCycle);
		}

		public void CalculateCycleInfo(MenstrualCycle cycle)
		{
			if (cycle.EndDate == null || cycle.EndDate == default)
			{
				cycle.EndDate = cycle.StartDate.AddDays(27); // mặc định 28 ngày
			}

			cycle.CycleLength = (cycle.EndDate.Value.DayNumber - cycle.StartDate.DayNumber) + 1;

			var cycleLength = cycle.CycleLength ?? 28;

			//  EndDate nếu chưa có
			if (cycle.EndDate == default(DateOnly))
			{
				cycle.EndDate = cycle.StartDate.AddDays(cycleLength - 1);
			}

			// OvulationDate nếu chưa có
			if (cycle.OvulationDate == null)
			{
				cycle.OvulationDate = cycle.StartDate.AddDays(cycleLength - 14);
			}

			// Chỉ tính FertilityWindow nếu chưa có
			if (cycle.FertilityWindowStart == null || cycle.FertilityWindowEnd == null)
			{
				cycle.FertilityWindowStart = cycle.OvulationDate?.AddDays(-6);
				cycle.FertilityWindowEnd = cycle.OvulationDate?.AddDays(5);
			}

			// Chỉ tính PillReminder nếu chưa có
			if (cycle.PillReminder == null)
			{
				cycle.PillReminder = cycle.StartDate.AddDays(-1);
				// test mail
				//cycle.PillReminder = DateOnly.FromDateTime(DateTime.Today);
			}
		}
		public List<(DateOnly Date, CycleDayType DayType)> GetExtendedCycleDayTypes(MenstrualCycle latestCycle, DateTime startRange, DateTime endRange)
		{

			var result = new List<(DateOnly, CycleDayType)>();

			var currentDate = DateOnly.FromDateTime(startRange);
			var endDate = DateOnly.FromDateTime(endRange);

			while (currentDate <= endDate)
			{
				var dayType = GetDayTypeForDate(currentDate, latestCycle);
				result.Add((currentDate, dayType));
				currentDate = currentDate.AddDays(1);
			}

			return result;
		}
		private CycleDayType GetDayTypeForDate(DateOnly date, MenstrualCycle cycle)
		{
			if (cycle.PillReminder.HasValue && date == cycle.PillReminder.Value)
			{
				return CycleDayType.TakePill;
			}
			// Sử dụng dữ liệu thực từ database
			var periodLength = cycle.PeriodLength ?? 5;
			var ovulationDate = cycle.OvulationDate ?? cycle.StartDate.AddDays((cycle.CycleLength ?? 28) - 14);
			var fertileStart = cycle.FertilityWindowStart ?? ovulationDate.AddDays(-6);
			var fertileEnd = cycle.FertilityWindowEnd ?? ovulationDate.AddDays(5);

			// Kiểm tra ngày hiện tại thuộc chu kì nào
			if (IsDateInCurrentCycle(date, cycle))
			{
				// 1. Menstruation (trong khoảng StartDate đến StartDate + PeriodLength)
				if (date >= cycle.StartDate && date < cycle.StartDate.AddDays(periodLength))
					return CycleDayType.Menstruation;

				// 2. Fertile window
				if (date >= fertileStart && date <= fertileEnd)
				{
					var daysSinceOvulation = (date.ToDateTime(TimeOnly.MinValue) - ovulationDate.ToDateTime(TimeOnly.MinValue)).Days;

					if (date == ovulationDate)
						return CycleDayType.Ovulation;
					else if (daysSinceOvulation >= -2 && daysSinceOvulation < 0)
						return CycleDayType.HighFertility;
					else if (daysSinceOvulation < -2)
						return CycleDayType.Fertile;
					else // daysSinceOvulation > 0
						return CycleDayType.RelativeSafe;
				}

				// 3. Absolute Safe (các ngày còn lại trong chu kì)
				return CycleDayType.AbsoluteSafe;
			}
			else
			{
				// Tính toán cho các chu kì khác dựa trên chu kì gốc
				return GetDayTypeForExtendedCycle(date, cycle);
			}
		}
		private CycleDayType GetDayTypeForExtendedCycle(DateOnly date, MenstrualCycle referenceCycle)
		{
			var cycleLength = referenceCycle.CycleLength ?? 28;
			var periodLength = referenceCycle.PeriodLength ?? 5;

			// Tính toán pill reminder cho các chu kỳ tiếp theo
			if (referenceCycle.PillReminder.HasValue)
			{
				var originalPillDay = referenceCycle.PillReminder.Value;
				var daysDiffPill = (date.ToDateTime(TimeOnly.MinValue) - originalPillDay.ToDateTime(TimeOnly.MinValue)).Days;

				// Kiểm tra xem ngày này có phải là pill reminder trong chu kỳ nào đó không
				if (daysDiffPill % cycleLength == 0 && daysDiffPill >= 0)
				{
					return CycleDayType.TakePill;
				}
			}

			// Tính chu kì nào chứa ngày này
			var daysDiff = (date.ToDateTime(TimeOnly.MinValue) - referenceCycle.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
			var cycleNumber = (int)Math.Floor((double)daysDiff / cycleLength);

			// Tính StartDate của chu kì chứa ngày này
			var cycleStartDate = referenceCycle.StartDate.AddDays(cycleNumber * cycleLength);
			var dayInCycle = (date.ToDateTime(TimeOnly.MinValue) - cycleStartDate.ToDateTime(TimeOnly.MinValue)).Days;

			// Tính các ngày quan trọng trong chu kì này
			var ovulationDay = cycleLength - 14;
			var fertileStartDay = ovulationDay - 6;
			var fertileEndDay = ovulationDay + 5;
			var highFertilityStartDay = ovulationDay - 2;

			// Xác định loại ngày
			if (dayInCycle < periodLength)
				return CycleDayType.Menstruation;
			else if (dayInCycle == ovulationDay)
				return CycleDayType.Ovulation;
			else if (dayInCycle >= highFertilityStartDay && dayInCycle < ovulationDay)
				return CycleDayType.HighFertility;
			else if (dayInCycle >= fertileStartDay && dayInCycle < highFertilityStartDay)
				return CycleDayType.Fertile;
			else if (dayInCycle > ovulationDay && dayInCycle <= fertileEndDay)
				return CycleDayType.RelativeSafe;
			else
				return CycleDayType.AbsoluteSafe;
		}
		private bool IsDateInCurrentCycle(DateOnly date, MenstrualCycle cycle)
		{
			return date >= cycle.StartDate && date <= cycle.EndDate;
		}

		public async Task<List<MenstrualCycle>> GetByUserIdAsync(Guid userId)
		{
			return await _menstrualCycleRepository.GetByUserIdAsync(userId);
		}

		public async Task<MenstrualCycle?> GetLatestCycleAsync(Guid userId)
		{
			return await _menstrualCycleRepository.GetLatestCycleAsync(userId);
		}

		// Method xử lý chu kỳ không đều
		public void CalculateIrregularCycleInfo(MenstrualCycle cycle, int shortestCycleLength, int longestCycleLength)
		{
			// Sử dụng chu kỳ trung bình cho tính toán ban đầu
			var averageCycleLength = (shortestCycleLength + longestCycleLength) / 2;
			cycle.CycleLength = averageCycleLength;

			// Tính EndDate dựa trên chu kỳ trung bình
			if (cycle.EndDate == default(DateOnly))
			{
				cycle.EndDate = cycle.StartDate.AddDays(averageCycleLength - 1);
			}

			// OvulationDate cho chu kỳ không đều - sử dụng khoảng thay vì ngày cố định
			if (cycle.OvulationDate == null)
			{
				// Ovulation thường xảy ra 12-16 ngày trước kỳ kinh tiếp theo
				// Với chu kỳ không đều, ta lấy 14 ngày trước chu kỳ trung bình
				cycle.OvulationDate = cycle.StartDate.AddDays(averageCycleLength - 14);
			}

			// FertilityWindow rộng hơn cho chu kỳ không đều
			if (cycle.FertilityWindowStart == null || cycle.FertilityWindowEnd == null)
			{
				// Mở rộng cửa sổ sinh sản do tính không đều
				var earliestOvulation = cycle.StartDate.AddDays(shortestCycleLength - 16); // Sớm nhất
				var latestOvulation = cycle.StartDate.AddDays(longestCycleLength - 12);    // Muộn nhất

				cycle.FertilityWindowStart = earliestOvulation.AddDays(-6); // 6 ngày trước ovulation sớm nhất
				cycle.FertilityWindowEnd = latestOvulation.AddDays(5);      // 5 ngày sau ovulation muộn nhất
			}

			// PillReminder
			if (cycle.PillReminder == null)
			{
				cycle.PillReminder = cycle.StartDate.AddDays(-1);
			}

			// Lưu thông tin chu kỳ không đều vào Note
			if (string.IsNullOrEmpty(cycle.Note))
			{
				cycle.Note = $"irregular:{shortestCycleLength}-{longestCycleLength}";
			}
		}

		// Method dự đoán ngày kinh tiếp theo cho chu kỳ không đều
		public List<DateOnly> PredictNextIrregularPeriods(MenstrualCycle cycle, int shortestCycleLength, int longestCycleLength, int monthsAhead = 6)
		{
			var predictions = new List<DateOnly>();
			var currentDate = cycle.StartDate;

			for (int i = 0; i < monthsAhead; i++)
			{
				// Tính khoảng dự đoán cho kỳ tiếp theo
				var earliestNext = currentDate.AddDays(shortestCycleLength);
				var latestNext = currentDate.AddDays(longestCycleLength);
				var averageNext = currentDate.AddDays((shortestCycleLength + longestCycleLength) / 2);

				predictions.Add(earliestNext);   // Ngày sớm nhất có thể
				predictions.Add(averageNext);    // Ngày trung bình
				predictions.Add(latestNext);     // Ngày muộn nhất có thể

				currentDate = averageNext; // Sử dụng ngày trung bình cho lần dự đoán tiếp theo
			}

			return predictions;
		}

		// Override method GetDayTypeForDate cho chu kỳ không đều
		private CycleDayType GetDayTypeForIrregularDate(DateOnly date, MenstrualCycle cycle)
		{
			// Parse thông tin chu kỳ không đều từ Note
			var noteInfo = ParseIrregularCycleNote(cycle.Note);
			if (noteInfo == null) return CycleDayType.AbsoluteSafe;

			var (shortestCycle, longestCycle) = noteInfo.Value;
			var periodLength = cycle.PeriodLength ?? 5;

			// Kiểm tra ngày hiện tại thuộc chu kỳ nào
			if (IsDateInCurrentIrregularCycle(date, cycle, shortestCycle, longestCycle))
			{
				// 1. Menstruation
				if (date >= cycle.StartDate && date < cycle.StartDate.AddDays(periodLength))
					return CycleDayType.Menstruation;

				// 2. Fertile window (rộng hơn do tính không đều)
				var fertileStart = cycle.FertilityWindowStart ?? cycle.StartDate.AddDays(shortestCycle - 16 - 6);
				var fertileEnd = cycle.FertilityWindowEnd ?? cycle.StartDate.AddDays(longestCycle - 12 + 5);

				if (date >= fertileStart && date <= fertileEnd)
				{
					// Trong cửa sổ sinh sản rộng
					var averageOvulation = cycle.OvulationDate ?? cycle.StartDate.AddDays((shortestCycle + longestCycle) / 2 - 14);
					var daysSinceOvulation = (date.ToDateTime(TimeOnly.MinValue) - averageOvulation.ToDateTime(TimeOnly.MinValue)).Days;

					// Do tính không đều, ta mở rộng các khoảng thời gian
					if (Math.Abs(daysSinceOvulation) <= 1) // Gần ngày rụng trứng dự đoán
						return CycleDayType.Ovulation;
					else if (daysSinceOvulation >= -4 && daysSinceOvulation < 0)
						return CycleDayType.HighFertility;
					else if (date >= fertileStart && date <= fertileEnd)
						return CycleDayType.Fertile;
				}

				// 3. Relative Safe hoặc Absolute Safe
				// Do chu kỳ không đều, hầu hết các ngày ngoài kinh nguyệt và cửa sổ sinh sản là tương đối an toàn
				return CycleDayType.RelativeSafe;
			}
			else
			{
				return GetDayTypeForExtendedIrregularCycle(date, cycle, shortestCycle, longestCycle);
			}
		}

		// Helper method để parse thông tin chu kỳ không đều từ Note
		private (int shortest, int longest)? ParseIrregularCycleNote(string note)
		{
			if (string.IsNullOrEmpty(note) || !note.StartsWith("irregular:"))
				return null;

			try
			{
				var cycleInfo = note.Substring("irregular:".Length);
				var parts = cycleInfo.Split('-');
				if (parts.Length == 2 &&
					int.TryParse(parts[0], out int shortest) &&
					int.TryParse(parts[1], out int longest))
				{
					return (shortest, longest);
				}
			}
			catch
			{
				return null;
			}

			return null;
		}

		// Kiểm tra ngày có thuộc chu kỳ hiện tại không (cho chu kỳ không đều)
		private bool IsDateInCurrentIrregularCycle(DateOnly date, MenstrualCycle cycle, int shortestCycle, int longestCycle)
		{
			var daysDiff = (date.ToDateTime(TimeOnly.MinValue) - cycle.StartDate.ToDateTime(TimeOnly.MinValue)).Days;

			// Với chu kỳ không đều, chu kỳ hiện tại có thể kéo dài từ shortest đến longest
			return daysDiff >= 0 && daysDiff < longestCycle;
		}

		// Tính toán cho các chu kỳ mở rộng (chu kỳ không đều)
		private CycleDayType GetDayTypeForExtendedIrregularCycle(DateOnly date, MenstrualCycle referenceCycle, int shortestCycle, int longestCycle)
		{
			var averageCycle = (shortestCycle + longestCycle) / 2;
			var periodLength = referenceCycle.PeriodLength ?? 5;

			// Sử dụng chu kỳ trung bình để ước tính chu kỳ chứa ngày này
			var daysDiff = (date.ToDateTime(TimeOnly.MinValue) - referenceCycle.StartDate.ToDateTime(TimeOnly.MinValue)).Days;
			var estimatedCycleNumber = (int)Math.Floor((double)daysDiff / averageCycle);

			// Ước tính StartDate của chu kỳ chứa ngày này
			var estimatedCycleStart = referenceCycle.StartDate.AddDays(estimatedCycleNumber * averageCycle);
			var dayInCycle = (date.ToDateTime(TimeOnly.MinValue) - estimatedCycleStart.ToDateTime(TimeOnly.MinValue)).Days;

			// Do tính không đều, ta sử dụng khoảng rộng hơn
			var ovulationDayRange = (averageCycle - 16, averageCycle - 12); // Khoảng rụng trứng có thể
			var fertileStartDay = ovulationDayRange.Item1 - 6;
			var fertileEndDay = ovulationDayRange.Item2 + 5;

			// Xác định loại ngày với độ chính xác thấp hơn do tính không đều
			if (dayInCycle < periodLength)
				return CycleDayType.Menstruation;
			else if (dayInCycle >= ovulationDayRange.Item1 && dayInCycle <= ovulationDayRange.Item2)
				return CycleDayType.Ovulation; // Khoảng rụng trứng có thể
			else if (dayInCycle >= fertileStartDay && dayInCycle <= fertileEndDay)
				return CycleDayType.Fertile; // Cửa sổ sinh sản rộng
			else
				return CycleDayType.RelativeSafe; // Hầu hết các ngày khác là tương đối an toàn
		}

		// Method tạo lịch cho chu kỳ không đều
		public List<(DateOnly Date, CycleDayType DayType, string Description)> GetIrregularCycleCalendar(
			MenstrualCycle latestCycle, DateTime startRange, DateTime endRange)
		{
			var result = new List<(DateOnly, CycleDayType, string)>();
			var noteInfo = ParseIrregularCycleNote(latestCycle.Note);

			if (noteInfo == null)
			{
				// Fallback to regular cycle calculation
				return GetExtendedCycleDayTypes(latestCycle, startRange, endRange)
					.Select(x => (x.Date, x.DayType, GetDayTypeDescription(x.DayType)))
					.ToList();
			}

			var (shortestCycle, longestCycle) = noteInfo.Value;
			var currentDate = DateOnly.FromDateTime(startRange);
			var endDate = DateOnly.FromDateTime(endRange);

			while (currentDate <= endDate)
			{
				var dayType = GetDayTypeForIrregularDate(currentDate, latestCycle);
				var description = GetIrregularDayDescription(currentDate, latestCycle, shortestCycle, longestCycle, dayType);

				result.Add((currentDate, dayType, description));
				currentDate = currentDate.AddDays(1);
			}

			return result;
		}

		// Helper method để tạo mô tả cho ngày trong chu kỳ không đều
		private string GetIrregularDayDescription(DateOnly date, MenstrualCycle cycle, int shortestCycle, int longestCycle, CycleDayType dayType)
		{
			var daysDiff = (date.ToDateTime(TimeOnly.MinValue) - cycle.StartDate.ToDateTime(TimeOnly.MinValue)).Days + 1;

			return dayType switch
			{
				CycleDayType.Menstruation => $"Day {daysDiff} - Menstruation",
				CycleDayType.Fertile => $"Day {daysDiff} - Fertile window (irregular cycle)",
				CycleDayType.HighFertility => $"Day {daysDiff} - High fertility (irregular cycle)",
				CycleDayType.Ovulation => $"Day {daysDiff} - Possible ovulation (irregular cycle)",
				CycleDayType.RelativeSafe => $"Day {daysDiff} - Relatively safe (irregular cycle)",
				CycleDayType.AbsoluteSafe => $"Day {daysDiff} - Safe period",
				_ => $"Day {daysDiff}"
			};
		}

		private string GetDayTypeDescription(CycleDayType dayType)
		{
			return dayType switch
			{
				CycleDayType.Menstruation => "Menstruation",
				CycleDayType.Fertile => "Fertile",
				CycleDayType.HighFertility => "High Fertility",
				CycleDayType.Ovulation => "Ovulation",
				CycleDayType.RelativeSafe => "Relatively Safe",
				CycleDayType.AbsoluteSafe => "Absolutely Safe",
				_ => "Unknown"
			};
		}		
	}
}
