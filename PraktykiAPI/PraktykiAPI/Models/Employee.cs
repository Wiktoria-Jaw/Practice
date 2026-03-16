using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set;}

        public ICollection<WorkDay> WorkDays { get; set; }
        //public User User { get; set; } //relacja 1:1 a nie 1:wielu
        public ICollection<DayOff> DaysOff { get; set; }
    }
}
