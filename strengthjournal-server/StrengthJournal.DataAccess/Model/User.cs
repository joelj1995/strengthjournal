using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model
{
    public class User
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Handle { get; set; }
    }
}
