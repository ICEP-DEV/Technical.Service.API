using Microsoft.EntityFrameworkCore;
using TechTrackers.Data.Model;

namespace TechTrackers.Data
{
    public class TeckTrackersDbContext: DbContext
    {
        public TeckTrackersDbContext(DbContextOptions<TeckTrackersDbContext> options) : base(options) { }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Service_level_agreement> Service_Level_Agreements { get; set; }
        public DbSet<Feedback> Feed_back { get; set; }
        public DbSet<Escalation> Escalations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Log_chat> Log_chats { get; set; }
        public DbSet<Log_status_history> Log_status_histor { get; set; }
        public DbSet<User_Role> User_Roles { get; set; }
    }
}
