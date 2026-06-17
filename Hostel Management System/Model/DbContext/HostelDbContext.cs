using Microsoft.EntityFrameworkCore;

namespace Hostel_Management_System.Model.DbContext
{

    public class HostelDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public HostelDbContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Register> Registers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Hostel> Hostels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<RoomAllocation> RoomAllocations { get; set; }

        public DbSet<Fees> Fees { get; set; }

        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }

    }

}

