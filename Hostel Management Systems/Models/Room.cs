using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_Systems.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        public string? RoomNumber { get; set; }

        public int HostelId { get; set; }

        public int Capacity { get; set; }

        public int OccupiedBeds { get; set; }

        public string? Fees { get; set; }

        public string? Status { get; set; }
    }
}
