using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model
{
    public class WorkoutLogEntry
    {
        public Guid Id { get; set; }
        public DateTime EntryDateUTC { get; set; }
        public User User { get; set; }
        public ICollection<WorkoutLogEntrySet> Sets { get; set; } = new List<WorkoutLogEntrySet>();
    }
}
