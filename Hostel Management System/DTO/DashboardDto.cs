namespace Hostel_Management_System.DTO
{
    public class DashboardDto
    {
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
