using System.Data.Entity;
using Data.Model.Factory;

namespace Data.Context.Platform
{
    public class FactoryUserContext:PlatformBaseContext
    {
        public DbSet<FactoryUser> PlatformUsers { get; set; }
    }
}