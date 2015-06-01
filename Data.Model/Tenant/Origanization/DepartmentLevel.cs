//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using Data.Model.Base;
//
//namespace Data.Model.Tenant.Origanization
//{
//    //部门级别，1-5级
//    [Table("DepartmentLevel")]
//    public class DepartmentLevel : BaseModel
//    {
//        //级别Id
//        [Key]
//        public int DepartmentLevelId { get; set; }
//
//        //
//        [MaxLength(30)]
//        public string DepartmentLevelName { get; set; }
//
//        [MaxLength(200)]
//        public string Description { get; set; }
//    }
//}