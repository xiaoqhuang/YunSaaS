using System.Data;
using System.Data.Entity;
using Data.Model.Base;
using Data.Model.Platform;

namespace Data.Context.Platform
{
    public class PlatformUserContext : PlatformBaseContext
    {
        public DbSet<PlatformUser> PlatformUsers { get; set; }
        public DbSet<UserLoginLog> UserLoginLogs { get; set; }
    }
}