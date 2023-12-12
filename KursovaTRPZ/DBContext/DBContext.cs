using Microsoft.EntityFrameworkCore;

namespace KursovaTRPZ.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<Auth> Auth { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<EventLog> EventLogs { get; set; }
        public DbSet<MotionSensor> MotionSensors { get; set; }
        public DbSet<SoilSensor> SoilSensors { get; set; }
        public DbSet<WaterSensor> WaterSensors { get; set; }
        public DbSet<RadiationSensor> RadiationSensors { get; set; }
        public DbSet<Sensor> Sensors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace "your_connection_string" with your actual connection string
            optionsBuilder.UseSqlServer("Server=DESKTOP-LK2DBBD;Database=SmartEnv;Integrated Security=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship between Administrator and EventLog
            modelBuilder.Entity<EventLog>()
                .HasOne(e => e.AdminNavigation)
                .WithMany(a => a.EventLogs)
                .HasForeignKey(e => e.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}