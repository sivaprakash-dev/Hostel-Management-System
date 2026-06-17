using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_Systems.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        public string? Name { get; set; }

        public string? Gender { get; set; }

        public int Age { get; set; }
        public string? Mobile { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? Photo { get; set; }

        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
