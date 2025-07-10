namespace GenderHealcareSystem.DTO.Request
{
	public class UpdateBookingRequest
	{
		public Guid BookingId { get; set; }
		public DateTime BookingDate { get; set; }
		public string? Note { get; set; }
		public List<Guid> ServiceIds { get; set; } = new();
	}
}
