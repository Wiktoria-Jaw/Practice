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
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Permission { get; set; }
        [Required]
        public int Is_LogIn { get; set; }
        [Required]
        public int Is_Active { get; set; }

        [Required]
        public int Employee_ID { get; set; }
        public Employee Employee { get; set; }
    }
}
