using System.Web;
using Data.Model.Platform;

namespace PlatformSite.App_Data
{
    public class SessionContext
    {
        private string Key_PlatformUser = "PlatformUser";
        private string Key_ValidateCode = "ValidateCode";

        public PlatformUser PlatformUser
        {
            get { return GetSession<PlatformUser>(Key_PlatformUser); }
            set { SetSession(Key_PlatformUser, value); }
        }

        public string ValidateCode
        {
            get { return GetSession<string>(Key_ValidateCode); }
            set { SetSession(Key_ValidateCode, value); }
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