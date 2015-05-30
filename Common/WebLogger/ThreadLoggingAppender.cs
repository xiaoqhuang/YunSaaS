using log4net.Appender;
using log4net.Core;

namespace WebLogger
{
    public class ThreadLoggingAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            ThreadLogger threadLogger = LoggerFactory.GetThreadLogger();
            threadLogger.AppendLog(RenderLoggingEvent(loggingEvent));
        }
    }
}