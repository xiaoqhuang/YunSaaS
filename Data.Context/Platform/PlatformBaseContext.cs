using System.Data.Entity;

namespace Data.Context.Platform
{
    public class PlatformBaseContext : DbContext
    {
        public PlatformBaseContext()
            : base("PlatformConnection")
        {
        }
    }
}