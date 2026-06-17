using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_System.Model
{
    public class Hostel
    {
        [Key]
        public int HostelId { get; set; }

        public string? HostelName { get; set; }

        public string? HostelType { get; set; }

        public int TotalRooms { get; set; }
    }
}
