namespace GenderHealcareSystem.DTO.Request
{
	public class CreateBookingRequest
	{
		public Guid PatientId { get; set; }
		public Guid MedicalHistoryId { get; set; }
		public DateTime BookingDate { get; set; }
		public string? Note { get; set; }
		public List<Guid> ServiceIds { get; set; } = new();
	}
}
