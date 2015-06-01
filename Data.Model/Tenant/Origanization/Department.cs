using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Model.Base;

namespace Data.Model.Tenant.Origanization
{
    //公司部门
    [Table("Department")]
    public class Department : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DepartmentId { get; set; }

        public DepartmentType DepartmentType { get; set; }

        //部门名称
        [MaxLength(100)]
        public string DepartmentName { get; set; }

        //部门级别，1级最高，只有高级别部门才能做为低级别部门的上级部门
        public int DepartmentLevelId { get; set; }
        //上级部门Id
        public Guid? ParentDepartmentId { get; set; }
        //部门下直属员工
        public virtual ICollection<Employee> Employees { get; set; }
    }

    public enum DepartmentType
    {
        //Customer和Supplier都是系统自动创建的
        Trade,
        Customer,
        Supplier
    }
}
