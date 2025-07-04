using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO
{
	public class IrregularCycleModel : IValidatableObject
	{
		[Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu chu kỳ gần nhất.")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn độ dài chu kỳ ngắn nhất.")]
		public int ShortestCycleLength { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn độ dài chu kỳ dài nhất.")]
		public int LongestCycleLength { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn số ngày hành kinh.")]
		public int PeriodLength { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (LongestCycleLength < ShortestCycleLength)
			{
				yield return new ValidationResult(
					"Độ dài chu kỳ dài nhất phải lớn hơn hoặc bằng độ dài chu kỳ ngắn nhất.",
					new[] { nameof(LongestCycleLength), nameof(ShortestCycleLength) });
			}

			if (PeriodLength > ShortestCycleLength)
			{
				yield return new ValidationResult(
					"Số ngày hành kinh không thể dài hơn độ dài chu kỳ ngắn nhất.",
					new[] { nameof(PeriodLength) });
			}
		}
	}
}
