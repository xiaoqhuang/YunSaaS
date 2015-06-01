using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Model.Base;

namespace Data.Model.Tenant.Origanization
{
    //员工表
    [Table("Employee")]
    public class Employee : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EmployeeId { get; set; }

        public EmployeeType EmployeeType { get; set; }
        //邮箱
        [MaxLength(30)]
        public string Email { get; set; }

        //姓名
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(400)]
        public string EncryptedPassword { get; set; }

        //电话
        [MaxLength(20)]
        public string Phone { get; set; }

        [MaxLength(50)]
        public string QQ { get; set; }

        [MaxLength(50)]
        public string Skype { get; set; }

        //手机
        [MaxLength(20)]
        public string MobilePhone { get; set; }

        //性别
        public Gender? Gender { get; set; }
        //头像路径
        [MaxLength(300)]
        public string HeadImagePath { get; set; }

        //级别
        public int EmployeeLevelId { get; set; }
        //职务
        [MaxLength(30)]
        public string Position { get; set; }

        //生日
        public DateTime? Birthday { get; set; }
        //直属上线
        public Guid? SuperiorId { get; set; }
        //直属部门
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
    }

    public enum EmployeeType
    {
        //Customer和Supplier都是系统自动创建的
        Trade,
        Customer,
        Supplier
    }

    public enum Gender
    {
        男,
        女
    }
}