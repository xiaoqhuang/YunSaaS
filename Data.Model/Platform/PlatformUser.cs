using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Model.Base;

namespace Data.Model.Platform
{
    [Table("PlatformUser")]
    public class PlatformUser : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        public UserType UserType { get; set; }

        [MaxLength(100)]
        public string UserName { get; set; }

        [MaxLength(400)]
        public string EncryptedPassword { get; set; }

        [MaxLength(20)]
        public string RealName { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(20)]
        public string MobilePhone { get; set; }

        [MaxLength(50)]
        public string CompanyName { get; set; }

        public virtual ICollection<UserLoginLog> UserLoginLogs { get; set; }
    }

    [Table("UserLoginLog")]
    public class UserLoginLog : ReadonlyBaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LoginLogId { get; set; }

        public Guid UserId { get; set; }
        public bool IsLoginSuccessfully { get; set; }
        public PlatformUser PlatformUser { get; set; }
    }

    public enum UserType
    {
        Admin = 1,
        Trader = 2,
        Customer = 3,
        Supplier = 4
    }
}