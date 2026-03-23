using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PraktykiAPI.Models
{
    [Table("WorkSettings")]
    public class WorkSettings
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public int? MinWorkdayLengthInMinutes { get; set; }
        [MaxLength(50)]
        public int? AutoEndWorkdayLengthInMinutes { get; set; }
        [MaxLength(50)]
        public int? MinBreakBetweenWorkdaysInMinutes { get; set; }
        [MaxLength(50)]
        public int? MinWorkdayLengthForBreakInMinutes { get; set; }
        [MaxLength(50)]
        public int? MinBreakLengthInMinutes { get; set; }
    }
}
