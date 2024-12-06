using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeCURDCore.Models
{
    public class Employee
    {
        private const string V = "First Name";

        [Key]
        public int ID { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DOB { get; set; }
        [DisplayName("Email Address")]
        [Required]
        public string Email { get; set; }
        [DisplayName("Salary")]
        [Required]
        public double Salary { get; set; }

        [NotMapped]
        public string FullName
        {
            get {  return FirstName + " " + LastName; }
        }
    }
}
