using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    [Table("Breaks")]
    public class Break
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime BreakStart { get; set; }
        public DateTime BreakEnd { get; set; }
        [Required]
        public int WorkDayID { get; set; }


        [ForeignKey("WorkDayID")]
        public WorkDay WorkDay { get; set; }
    }
}
