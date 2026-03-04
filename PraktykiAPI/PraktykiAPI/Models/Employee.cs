using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    //[Table("Employees")]
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address {get; set;}
        [Required]
        public string Phone_Number { get; set;}

        public ICollection<WorkDay> WorkDays { get; set; }
        public User User { get; set; } //relacja 1:1 a nie 1:wielu
        public ICollection<Day_Off> Days_Off { get; set; }
    }
}
