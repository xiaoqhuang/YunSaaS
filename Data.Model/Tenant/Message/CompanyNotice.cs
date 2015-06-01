using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Model.Base;

namespace Data.Model.Tenant.Message
{
    //公司内部通知
    [Table("CompanyNotice")]
    public class CompanyNotice : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompanyNoticeId { get; set; }

        //标题
        [MaxLength(200)]
        public string Title { get; set; }

        //正文
        [MaxLength(2000)]
        public string Content { get; set; }

        public NoticeType NoticeType { get; set; }

        //接收方部门Ids
        [MaxLength(2000)]
        public string SendToDepartmentIds { get; set; }

        //接收方员工Ids
        [MaxLength(4000)]
        public string SendToEmployeeIds { get; set; }

        //发送者Id
        public Guid SenderId { get; set; }
        //发送部门Id
        public Guid SenderDepartmentId { get; set; }

        //是否已删除
        public bool? IsDeleted { get; set; }
    }

    public enum NoticeType
    {
        通,
        急,
        奖,
        罚
    }
}