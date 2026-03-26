using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    [Table("DaysOff")]
    public class DayOff
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateOnly StartDate { get; set; }
        [Required]
        public DateOnly EndDate { get; set; }
        [Required]
        [MaxLength(10)]
        public string AcceptStatus { get; set; } = "pending";
        [Required]
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
