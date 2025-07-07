using System.ComponentModel.DataAnnotations;

namespace GenderHealcareSystem.DTO
{
	public class MenstrualCyclesModel : IValidatableObject
	{
		public Guid MenstrualCycleId { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn ngày bắt đầu.")]
		public DateTime StartDate { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn ngày kết thúc.")]
		public DateTime EndDate { get; set; }

		[Required(ErrorMessage = "Vui lòng chọn số ngày hành kinh.")]
		public int? PeriodLength { get; set; }

		[MaxLength(100)]
		public string? Note { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (EndDate < StartDate)
			{
				yield return new ValidationResult(
					"Ngày kết thúc không thể nhỏ hơn ngày bắt đầu.",
					new[] { nameof(EndDate) });
			}

			if ((EndDate - StartDate).TotalDays < 20)
			{
				yield return new ValidationResult(
					"Khoảng cách giữa ngày bắt đầu và ngày kết thúc không được dưới 20 ngày((nếu bạn gặp tình trạng trên thì đi khám bác sĩ).",
					new[] { nameof(StartDate), nameof(EndDate) });
			}
		}
	}
}
