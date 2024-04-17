using System.ComponentModel.DataAnnotations;

namespace Login_Project.Models
{
    public class Logins
    {
        //public int Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Key]
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
    }
}
