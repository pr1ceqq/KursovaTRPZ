using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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
            optionsBuilder.UseSqlServer("Server=DESKTOP-LK2DBBD;Database=SmartEnv;Integrated Security=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedNever();
            
            modelBuilder.Entity<Engineer>()
                .HasMany(e => e.Sensors)
                .WithOne(s => s.Engineer)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Administrator>()
                .HasMany(a => a.EventLogs)
                .WithOne(el => el.AdminNavigation)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Administrator>()
                .HasMany(a => a.Engineers)  
                .WithOne(e => e.Administrator)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Sensor>()
                .HasMany(s => s.EventLogs)
                .WithOne(el => el.Sensor)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}