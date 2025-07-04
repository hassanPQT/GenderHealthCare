using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class MenstrualCycle
    {
		[Key]
		public Guid MenstrualCycleId { get; set; }

		public Guid UserId { get; set; }

		public DateOnly StartDate { get; set; }
		public DateOnly? EndDate { get; set; }
		public int? PeriodLength { get; set; }
		public int? CycleLength { get; set; }
		public DateOnly? OvulationDate { get; set; }
		public DateOnly? FertilityWindowStart { get; set; }
		public DateOnly? FertilityWindowEnd { get; set; }
		public DateOnly? PillReminder { get; set; }

		[MaxLength(100)]
		public string? Note { get; set; }

		[ForeignKey("UserId")]
		public User User { get; set; }
	}
}
