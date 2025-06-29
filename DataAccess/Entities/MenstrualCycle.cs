﻿using System;
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

        [Required]
        public Guid UserId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime OvulationDate { get; set; }
        public DateTime FertilityWindowStart { get; set; }
        public DateTime FertilityWindowEnd { get; set; }
        public DateTime PillReminder { get; set; }

        [MaxLength(500)]
        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
