using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model.Virtual
{
    public class ExerciseHistoryLine
    {
        public Guid UserId { get; set; }
        public Guid ExerciseId { get; set; }
        public Guid WorkoutId { get; set; }
        public DateTime EntryDateUTC { get; set; }
        public decimal? BodyWeightKg { get; set; }
        public decimal? BodyWeightLbs { get; set; }
        public decimal? WeightKg { get; set; }
        public decimal? WeightLbs { get; set; }
        public int? Reps { get; set; }
        public int? TargetReps { get; set; }
        public int? RPE { get; set; }
    }
}
