namespace Hostel_Management_Systems.Models
{
    public class Dashboard
    {
        public int Id { get; set; }
        public int TotalStudents { get; set; }

        public int TotalRooms { get; set; }

        public int AvailableRooms { get; set; }

        public int FullRooms { get; set; }

        public int PendingComplaints { get; set; }

        public int ResolvedComplaints { get; set; }

        public int PendingFees { get; set; }

        public int PaidFees { get; set; }
    }
}
