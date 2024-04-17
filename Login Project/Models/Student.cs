using System.ComponentModel.DataAnnotations;

namespace Login_Project.Models
{
    public class Student
    {
        [Key]
        public string Name { get; set; }
        
        public string Department { get; set; }
        public int Age { get; set; }


    }
}
