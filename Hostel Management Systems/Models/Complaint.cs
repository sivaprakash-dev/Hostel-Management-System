using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_Systems.Models
{
    public class Complaint
    {
        [Key]
        public int ComplaintId { get; set; }

        public int StudentId { get; set; }

        public string? Subject { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
