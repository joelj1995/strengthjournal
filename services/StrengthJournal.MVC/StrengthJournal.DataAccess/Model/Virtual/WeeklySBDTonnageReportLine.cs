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
        public decimal SquatTonnage { get; set; }
        public decimal BenchTonnage { get; set; }
        public decimal DeadliftTonnage { get; set; }
    }
}
