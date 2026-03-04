using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    //[Table("Days_Off")]
    public class Day_Off
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public DateOnly Start_Date { get; set; }
        [Required]
        public DateOnly End_Date { get; set; }
        public string Status { get; set; } = "pending";

        [Required]
        public int Employee_ID { get; set; }
        public Employee Employee { get; set; }
    }
}
