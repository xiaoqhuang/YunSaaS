using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Model.Base;

namespace Data.Model.Tenant.Message
{
    [Table("BusinessMessage")]
    public class BusinessMessage : ReadonlyBaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid BusinessMessageId { get; set; }

        [MaxLength(4000)]
        public string Message { get; set; }

        [MaxLength(100)]
        public string OrderId { get; set; }

        public OrderType OrderType { get; set; }

        public MessageType MessageType { get; set; }

        //接收者
        public Guid SendToEmployeeId { get; set; }

        //发送者
        public Guid SenderId { get; set; }

        //是否是回复的消息
        public bool IsReply { get; set; }

        //是否允许回复
        public bool IsCanReply
        {
            get { return MessageType == MessageType.Personal; }
        }
    }

    public enum OrderType
    {
        客户订单, //客户->贸易商
        采购订单, //贸易商->工厂
    }

    public enum MessageType
    {
        System, //系统自动发送的消息
        Personal, //个人消息
    }
}