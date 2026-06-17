using Hostel_Management_System.Model;
using Hostel_Management_System.Model.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hostel_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomAllocationController : ControllerBase
    {
        private readonly HostelDbContext _context;

        public RoomAllocationController(HostelDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public async Task<IActionResult> GetAllocations()
        {
            var data = await _context.RoomAllocations
                .ToListAsync();

            return Ok(data);
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> AllocateRoom(
            RoomAllocation allocation)
        {
            var student = await _context.Students
                .FindAsync(allocation.StudentId);

            if (student == null)
            {
                return NotFound("Student Not Found");
            }

            var room = await _context.Rooms
                .FindAsync(allocation.RoomId);

            if (room == null)
            {
                return NotFound("Room Not Found");
            }

            if (room.OccupiedBeds >= room.Capacity)
            {
                return BadRequest("Room Full");
            }

            allocation.AllocationDate = DateTime.Now;

            allocation.Status = "Allocated";

            _context.RoomAllocations.Add(allocation);

            room.OccupiedBeds++;
            if (room.OccupiedBeds == room.Capacity)
            {
                room.Status = "Full";
            }

            await _context.SaveChangesAsync();

            return Ok("Room Allocated Successfully");
        }

        [Authorize(Roles = "Admin")]

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAllocation(
            int id)
        {
            var allocation = await _context.RoomAllocations
                .FindAsync(id);

            if (allocation == null)
            {
                return NotFound("Allocation Not Found");
            }

            var room = await _context.Rooms
                .FindAsync(allocation.RoomId);

            if (room != null)
            {
                room.OccupiedBeds--;

                room.Status = "Available";
            }

            _context.RoomAllocations.Remove(allocation);

            await _context.SaveChangesAsync();

            return Ok("Allocation Removed Successfully");
        }


        [Authorize(Roles = "Student")]
        [HttpGet("my-room")]
        public async Task<IActionResult> MyRoom()
        {
            var studentId =
                Convert.ToInt32(
                    User.FindFirst("StudentId")?.Value);

            var data =
                await (from ra in _context.RoomAllocations
                       join r in _context.Rooms
                       on ra.RoomId equals r.RoomId
                       where ra.StudentId == studentId
                       select new
                       {
                           ra.StudentId,
                           ra.RoomId,
                           r.RoomNumber,
                           r.Capacity,
                           r.Status
                       })
                       .FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound("Room Not Allocated");
            }

            return Ok(data);
        }
    }
}
