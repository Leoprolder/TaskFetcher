using System.ComponentModel.DataAnnotations;
using TaskFetcher.Models.Enums;

namespace TaskFetcher.Models.Entity
{
    public class TimeTask
    {
        [Key]
        public Guid Guid { get; set; }
        public Status Status { get; set; }
        public DateTime Created { get; set; }
    }
}
