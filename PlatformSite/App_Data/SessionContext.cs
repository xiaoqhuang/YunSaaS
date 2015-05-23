using System.Web;
using Data.Model.Platform;

namespace PlatformSite.App_Data
{
    public class SessionContext
    {
        public PlatformUser PlatformUser
        {
            get { return GetSession<PlatformUser>("PlatformUser"); }
            set { SetSession("PlatformUser", value); }
        }

        private T GetSession<T>(string sessionKey)
        {
            object sessionObject = HttpContext.Current.Session[sessionKey];
            if (sessionObject != null)
                return (T) sessionObject;
            return default(T);
        }

        private void SetSession(string sessionKey, object sessionObject)
        {
            HttpContext.Current.Session[sessionKey] = sessionObject;
        }
    }
}