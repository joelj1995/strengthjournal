using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model
{
    public class WorkoutLogEntry
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Title { get; set; } = "";
        public DateTime EntryDateUTC { get; set; }
        public User User { get; set; }
        public ICollection<WorkoutLogEntrySet> Sets { get; set; } = new List<WorkoutLogEntrySet>();
        public uint? BodyWeightPIT { get; set; }
        public string Notes { get; set; } = string.Empty;

    }
}
