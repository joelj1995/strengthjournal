using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model
{
    [Index(nameof(ExternalId))]
    public class User
    {
        public Guid Id { get; set; }
        [MaxLength(255)]
        public string ExternalId { get; set; }
    }
}
