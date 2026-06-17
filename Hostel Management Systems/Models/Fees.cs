using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_Systems.Models
{
    public class Fees
    {
        [Key]
        public int FeeId { get; set; }

        public int StudentId { get; set; }

        public string? Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? PaymentStatus { get; set; }
    }
}
