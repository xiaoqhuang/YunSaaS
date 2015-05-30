using System.Data.Entity;
using Data.Model.Platform;
using Microsoft.Practices.Unity;

namespace Data.Context.Platform
{
    public class PlatformUserContext : PlatformBaseContext
    {
        [Dependency("CurrentSchema")]
        internal string CurrentSchema { get; set; }
        public DbSet<PlatformUser> PlatformUsers { get; set; }
        public DbSet<UserLoginLog> UserLoginLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(CurrentSchema);
            base.OnModelCreating(modelBuilder);
        }
    }
}