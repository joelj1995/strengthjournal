using System.ComponentModel.DataAnnotations;

namespace StrengthJournal.Server.ApiModels
{
    public class AppErrorDto
    {
        public Guid Id { get; set; }
        [MaxLength(4048)]
        public string? ErrorMessage { get; set; }
        public Guid? ApiTraceId { get; set; }
    }
}
