namespace Hostel_Management_System.DTO
{
    public class FeesDto
    {
        public int StudentId { get; set; }

        public string? Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string? PaymentStatus { get; set; }
    }
}
