using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    [Table("Work_Timetable")]
    public class WorkDay
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public int Employee_Id { get; set; }
        [Required]
        public TimeOnly Work_Start_Hour { get; set; }
        public TimeOnly? Work_End_Hour { get; set; }
        

        [ForeignKey("Employee_Id")]
        public Employee Employee { get; set; }

        public ICollection<Break> Breaks { get; set; }
    }
}
