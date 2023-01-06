using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.Core.DataAccess.Model
{
    public class WorkoutLogEntrySet
    {
        public Guid Id { get; set; }
        public WorkoutLogEntry WorkoutLogEntry { get; set; }
        public int Sequence { get; set; }
        public Exercise Exercise { get; set; }
        public int? Reps { get; set; }
        public int? TargetReps { get; set; }
        public decimal? Weight { get; set; }
        public WeightUnit? WeightUnit { get; set; }
        public int? RPE { get; set; }
        public string WeightUnitAbbreviation { get => WeightUnit?.Abbreviation ?? ""; }
    }
}
