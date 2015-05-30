using System.Web;

namespace WebLogger
{
    public class LoggerFactory
    {
        private const string ThreadLogging = "THREAD_LOGGING";

        public static ThreadLogger GetThreadLogger()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items[ThreadLogging] == null)
                {
                    HttpContext.Current.Items[ThreadLogging] = new ThreadLogger();
                }
                return (ThreadLogger)HttpContext.Current.Items[ThreadLogging];
            }
            return null;
        }
    }
}