using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PraktykiAPI.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [MaxLength(10)]
        public string Permission { get; set; }
        public int? IsLogIn { get; set; }
        [Required]
        [MaxLength(1)]
        public int IsActive { get; set; }
        [Required]
        public int EmployeeID { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
    }
}
