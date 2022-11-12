using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthJournal.DataAccess.Model
{
    public class AppError
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ApiErrorTraceId { get; set; }
        public DateTime DateCreated { get; set; }
        public string Message { get; set; } = String.Empty;
    }
}
