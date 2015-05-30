using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Web.SessionState;
using log4net;

namespace WebLogger
{
    public class WebLoggerModule : IHttpModule
    {
        private bool isWriteError;

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
            context.Error += Error;
        }

        private void BeginRequest(object sender, EventArgs e)
        {
            try
            {
                HttpContext context = ((HttpApplication) sender).Context;
                isWriteError = "XiaoQDbg".Equals(context.Request.QueryString["Debug"], StringComparison.OrdinalIgnoreCase);
                ILog logger = LoggerFactory.GetThreadLogger().Logger;
                logger.Info("==========Begin Request==========");
                logger.Info("*****Request Info*****");
                logger.Info("Host=" + Environment.MachineName);
                logger.Info("Request Time=" + DateTime.Now);
                logger.Info("Request URL=" + context.Request.RawUrl);
                logger.Info("Request Action=" + context.Request.HttpMethod);
                logger.Info("Request Header= [");
                NameValueCollection headers = context.Request.Headers;
                foreach (string header in headers)
                {
                    logger.Info("name=" + header + ", value=" + headers[header]);
                }
                logger.Info("]");
            }
            catch
            {
                // ignored
            }
        }

        private void Error(object sender, EventArgs e)
        {
            ThreadLogger threadLogger = LoggerFactory.GetThreadLogger();
            try
            {
                threadLogger.Stopwatch.Stop();
                threadLogger.Logger.Info("==========Request Error==========");
                threadLogger.Logger.Info("Page total execution time = " + threadLogger.Stopwatch.ElapsedMilliseconds +" ms");
                HttpApplication httpApplication = (HttpApplication) sender;
                HttpContext context = httpApplication.Context;
                Exception exception = httpApplication.Server.GetLastError();
                threadLogger.Logger.Fatal("Request Error", exception);
                PrintRequestInfo(threadLogger.Logger, context.Request, context.Session);
                SaveExceptionLog(threadLogger.GetAllLogs());

                if (isWriteError)
                {
                    WriteLogs(context.Response, threadLogger);
                    context.Response.End();
                }
            }
            catch
            {
                // ignored
            }
        }

        private void SaveExceptionLog(string logs)
        {
            string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs",
                DateTime.Now.ToString("yyyy-MM"), DateTime.Now.ToString("dd"));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            File.AppendAllText(Path.Combine(folder, Guid.NewGuid().ToString()), logs, Encoding.UTF8);
        }

        private void PrintRequestInfo(ILog logger, HttpRequest request, HttpSessionState session)
        {
            if (request != null)
            {
                string[] cookies = request.Cookies.AllKeys;
                logger.Info("Request Cookies= [");
                foreach (string cookie in cookies)
                {
                    logger.Info("name=" + cookie + ", value=" + request.Cookies[cookie]);
                }
                logger.Info("]");

                NameValueCollection queryString = request.QueryString;
                logger.Info("Request QueryString= [");
                foreach (string param in queryString)
                {
                    logger.Info("name=" + param + ", value=" + queryString[param]);
                }
                logger.Info("]");

                NameValueCollection form = request.Form;
                logger.Info("Request Form= [");
                foreach (string param in form)
                {
                    logger.Info("name=" + param + ", value=" + queryString[param]);
                }
                logger.Info("]");

                if (session != null)
                {
                    logger.Info("Request Session= [");
                    foreach (string key in session.Keys)
                    {
                        logger.Info("Name=" + key + ", Value=" + session[key]);
                    }
                    logger.Info("]");
                }
            }
        }

        private void EndRequest(object sender, EventArgs e)
        {
            try
            {
                ThreadLogger threadLogger = LoggerFactory.GetThreadLogger();
                HttpApplication httpApplication = (HttpApplication) sender;
                HttpContext context = httpApplication.Context;

                PrintRequestInfo(threadLogger.Logger, context.Request, null);

                threadLogger.Logger.Info("==========End Request==========");
                threadLogger.Stopwatch.Stop();
                threadLogger.Logger.Info("Page total execution time = " + threadLogger.Stopwatch.ElapsedMilliseconds +
                                         " ms");

                WriteLogs(context.Response, threadLogger);
            }
            catch
            {
                //ignore
            }
        }

        private void WriteLogs(HttpResponse response, ThreadLogger logs)
        {
            if (isWriteError)
            {
                response.Write("<div id='debugLog'>");
                response.Write(logs.GetAllLogs());
                response.Write("</div>");
            }
        }


        public void Dispose()
        {
        }
    }
}
