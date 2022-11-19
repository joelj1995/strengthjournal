using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model.Virtual
{
    [NotMapped]
    public class WeeklySBDTonnageReportLine
    {
        public DateTime WeekStart { get; set; }
        public int SquatTonnage { get; set; }
        public int BenchTonnage { get; set; }
        public int DeadliftTonnage { get; set; }
    }
}
