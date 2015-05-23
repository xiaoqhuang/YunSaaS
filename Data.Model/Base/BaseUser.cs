using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model.Base
{
    public class BaseUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        public string UserName { get; set; }
        public string EncryptedPassword { get; set; }
        public virtual ICollection<UserLoginLog> UserLoginLogs { get; set; }
    }

    public class UserLoginLog : ReadonlyBaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LoginLogId { get; set; }

        public Guid UserId { get; set; }
        public bool IsLoginSuccessfully { get; set; }
        public BaseUser User { get; set; }
    }
}