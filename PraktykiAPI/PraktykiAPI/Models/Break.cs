using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    [Table("Break_Timetable")]
    public class Break
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateOnly Date {  get; set; }
        [Required]
        public TimeOnly Break_Start_Hour { get; set; }
        [Required]
        public TimeOnly Break_End_Hour { get; set; }

        [Required]
        public int WorkDay_Id { get; set; }
        public WorkDay WorkDay { get; set; }
    }
}
