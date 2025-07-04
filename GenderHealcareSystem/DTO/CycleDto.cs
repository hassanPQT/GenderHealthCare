namespace GenderHealcareSystem.DTO
{
	public class CycleDto
	{
		public string CycleType { get; set; } = "regular";

		public MenstrualCyclesModel? RegularCycle { get; set; }

		public IrregularCycleModel? IrregularCycle { get; set; }
	}
}
