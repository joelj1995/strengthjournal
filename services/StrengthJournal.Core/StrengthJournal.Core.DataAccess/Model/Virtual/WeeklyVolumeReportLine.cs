using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.Core.DataAccess.Model.Virtual
{
    [NotMapped]
    public class WeeklyVolumeReportLine
    {
        public DateTime WeekStart { get; set; }
        public int NumberOfSets { get; set; }
    }
}
