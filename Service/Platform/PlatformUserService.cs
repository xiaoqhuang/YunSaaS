using System.Linq;
using Data.Context.Platform;
using Data.Model.Platform;
using Helper;
using Microsoft.Practices.Unity;

namespace Service.Platform
{
    public class PlatformUserService
    {
        [Dependency]
        internal PlatformUserContext UserContext { get; set; }

        public PlatformUser LoadByUserName(string userName)
        {
            return UserContext.PlatformUsers.FirstOrDefault(user => user.UserName == userName);
        }

        public PlatformUser Login(string userName, string password)
        {
            string encryptedPassword = EncryptHelper.MD5Hash(password);
            var platformUser = UserContext.PlatformUsers.FirstOrDefault(user => user.UserName == userName);
            if (platformUser != null)
            {
                UserContext.UserLoginLogs.Attach(new UserLoginLog
                {
                    UserId = platformUser.UserId,
                    CreateBy = platformUser.UserName,
                    IsLoginSuccessfully = platformUser.EncryptedPassword == encryptedPassword,
                });
                UserContext.SaveChanges();
            }
            return (platformUser != null && platformUser.EncryptedPassword == encryptedPassword) ? platformUser : null;
        }

        public void AddUser(PlatformUser platformUser)
        {
            UserContext.PlatformUsers.Add(platformUser);
            UserContext.SaveChanges();
        }
    }
}