using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.Core.DataAccess.Model
{
    public class Exercise
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public Guid? ParentExerciseId { get; set; }
        public User? CreatedByUser { get; set; }
    }
}
