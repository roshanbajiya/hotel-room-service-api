using Microsoft.EntityFrameworkCore;
using room_service.Models;

namespace room_service.Data
{
    public class RoomDbContext : DbContext
    {
        public RoomDbContext(DbContextOptions<RoomDbContext> options)
        : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasIndex(r => r.RoomNumber)
                .IsUnique();
        }


        public DbSet<Room>? Rooms { get; set; }

       
    }
}
