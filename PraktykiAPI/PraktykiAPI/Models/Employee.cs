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
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string? MiddleName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(25)]
        public string? PhoneNumber { get; set;}

        public ICollection<WorkDay> WorkDays { get; set; }
        public User User { get; set; }
        public ICollection<DayOff> DaysOff { get; set; }
    }
}
