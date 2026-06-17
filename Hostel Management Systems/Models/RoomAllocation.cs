using System.ComponentModel.DataAnnotations;

namespace Hostel_Management_Systems.Models
{
    public class RoomAllocation
    {
        [Key]
        public int AllocationId { get; set; }

        public int StudentId { get; set; }

        public int RoomId { get; set; }

        public DateTime AllocationDate { get; set; }

        public string? Status { get; set; }
    }
}
