using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PraktykiAPI.Models
{
    [Table("WorkSettings")]
    public class WorkSettings
    {
        [Key]
        public int ID { get; set; }
        public int? MinWorkdayLengthInMinutes { get; set; }
        public int? AutoEndWorkdayLengthInMinutes { get; set; }
        public int? MinBreakBetweenWorkdaysInMinutes { get; set; }
        public int? MinWorkdayLengthForBreakInMinutes { get; set; }
        public int? MinBreakLengthInMinutes { get; set; }
    }
}
