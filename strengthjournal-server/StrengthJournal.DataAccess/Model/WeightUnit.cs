using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model
{
    public class WeightUnit
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string FullName { get; set; }
        [MaxLength(20)]
        public string Abbreviation { get; set; }
    }
}
