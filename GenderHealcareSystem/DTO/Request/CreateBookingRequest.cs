namespace GenderHealcareSystem.DTO.Request
{
	public class CreateBookingRequest
	{
		public Guid? MedicalHistoryId { get; set; }
		public DateTime BookingDate { get; set; }
		public string? Note { get; set; }
		public List<Guid> ServiceIds { get; set; } = new();
	}
}
