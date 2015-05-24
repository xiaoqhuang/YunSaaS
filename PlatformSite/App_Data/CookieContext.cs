using System;
using System.Web;
using Helper;

namespace PlatformSite.App_Data
{
    public class CookieContext
    {
        private string Key_UserName = "userName";
        private string Key_EncryptedPassword = "encryptedPassword";

        public string UserName
        {
            get { return GetCookie(Key_UserName); }
            set { SetCookie(Key_UserName, value); }
        }

        //用对称加密进行二次加密
        public string EncryptedPassword
        {
            get { return EncryptHelper.Decrypt(GetCookie(Key_EncryptedPassword)); }
            set { SetCookie(Key_EncryptedPassword, EncryptHelper.Encrypt(value)); }
        }

        private void SetCookie(string cookieKey, string value)
        {
            HttpCookie httpCookie = new HttpCookie(cookieKey, value);
            httpCookie.Expires = DateTime.Now.AddDays(30); //默认30天Cookie
            httpCookie.Path = "/";
            HttpContext.Current.Response.Cookies.Add(httpCookie);
        }

        private string GetCookie(string cookieKey)
        {
            var httpCookie = HttpContext.Current.Request.Cookies.Get(cookieKey);
            if (httpCookie != null)
                return httpCookie.Value;
            return null;
        }
    }
}