using Microsoft.EntityFrameworkCore;
using TechTrackers.Data.Model;

namespace TechTrackers.Data
{
    public class TechTrackersDbContext: DbContext
    {
        public TechTrackersDbContext(DbContextOptions<TechTrackersDbContext> options) : base(options) { }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SLA> Service_Level_Agreements { get; set; }
        public DbSet<Feedback> Feed_back { get; set; }
        public DbSet<Escalation> Escalations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Log_chat> Log_chats { get; set; }
        public DbSet<Log_status_history> Log_status_histor { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
        public DbSet<UserOtp> UserOtps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configuring user to department relationship
            modelBuilder.Entity<User>()
                .HasOne(user => user.Department)
                .WithMany()
                .HasForeignKey(user => user.Department_ID)
                .OnDelete(DeleteBehavior.Restrict);

            //Log assigned by a User
            modelBuilder.Entity<Log>()
                .HasOne(log => log.AssignedByUser)
                .WithMany()
                .HasForeignKey(log => log.Assigned_By)
                .OnDelete(DeleteBehavior.Restrict);

            //Log category
            modelBuilder.Entity<Log>()
                .HasOne(log => log.Category)
                .WithMany()
                .HasForeignKey(log => log.Category_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Log>()
                .HasOne(log => log.Staff) // Assuming `Staff` is the user who reported the log
                .WithMany() // Assuming there isn't a collection on the `User` side, otherwise specify the collection
                .HasForeignKey(log => log.Staff_ID) // Assuming `Staff_ID` is the foreign key column
                .OnDelete(DeleteBehavior.Restrict);

            // Technician relationship configuration
            modelBuilder.Entity<Log>()
                .HasOne(log => log.Technician)
                .WithMany()
                .HasForeignKey(log => log.Technician_ID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Log>()
               .HasOne(log => log.AssignedByUser) // Assuming AssignedByUser is the correct navigation property
               .WithMany()
               .HasForeignKey(log => log.Assigned_By) // Use correct foreign key (Assigned_By or similar)
               .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
