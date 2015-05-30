using System.Diagnostics;
using System.Text;
using System.Web.SessionState;
using log4net;

namespace WebLogger
{
    public class ThreadLogger
    {
        private StringBuilder transactionLogs = new StringBuilder();

        public ThreadLogger()
        {
            Stopwatch = Stopwatch.StartNew();
            Logger = LogManager.GetLogger("Global");
        }

        public Stopwatch Stopwatch { get; set; }
        public ILog Logger { get; set; }

        public void AppendLog(string log)
        {
            transactionLogs.AppendLine(log);
        }

        public string GetAllLogs()
        {
            return transactionLogs.ToString();
        }
    }
}