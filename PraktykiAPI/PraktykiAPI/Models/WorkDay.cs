using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    [Table("WorkSchedule")]
    public class WorkDay
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateTime WorkStart { get; set; }
        public DateTime WorkEnd { get; set; }
        [Required]
        public int EmployeeID { get; set; }
        

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        public ICollection<Break> Breaks { get; set; }
    }
}
