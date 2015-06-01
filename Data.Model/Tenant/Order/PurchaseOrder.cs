using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Model.Base;

namespace Data.Model.Tenant.Order
{
    //客户订单(客户->贸易商)
    [Table("PurchaseOrder")]
    public class PurchaseOrder : BaseModel
    {
        //系统自动算成的订单Id
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PurchaseOrderId { get; set; }

        //用户输入的订单编号
        [MaxLength(50)]
        public Guid OrderNumber { get; set; }

        //客户Id
        public Guid CustomerId { get; set; }
        //客户公司Id
        public Guid CustomerCompanyId { get; set; }
        //装船港
        [MaxLength(100)]
        public string LoadingPort { get; set; }

        //目的港
        [MaxLength(100)]

        //船期
        public DateTime? DeliveryDate { get; set; }

        public string DestinationPort { get; set; }
        public OrderStatus OrderStatus { get; set; }
        //预留字段1
        [MaxLength(1000)]
        public string Filed1 { get; set; }

        //预留字段2
        [MaxLength(1000)]
        public string Filed2 { get; set; }

        //预留字段3
        [MaxLength(1000)]
        public string Filed3 { get; set; }

        //预留字段4
        [MaxLength(1000)]
        public string Filed4 { get; set; }

        //预留字段5
        [MaxLength(1000)]
        public string Filed5 { get; set; }

        //预留字段6
        [MaxLength(1000)]
        public string Filed6 { get; set; }

        //预留字段7
        [MaxLength(1000)]
        public string Filed7 { get; set; }

        //预留字段8
        [MaxLength(1000)]
        public string Filed8 { get; set; }

        public ICollection<PurchaseOrderItem> PurchaseOrderDetails { get; set; } 
    }

    //客户订单详情
    [Table("PurchaseOrderItem")]
    public class PurchaseOrderItem : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PurchaseOrderItemId { get; set; }

        public Guid PurchaseOrderId { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }

        //编号
        [MaxLength(30)]
        public string ItemNo { get; set; }

        //物品名称
        [MaxLength(100)]
        public string ItemName { get; set; }

        //物品各类
        [MaxLength(50)]
        public string ItemType { get; set; }

        //规格
        [MaxLength(1000)]
        public string Specification { get; set; }

        //数量
        public int Qty { get; set; }

        //单位
        [MaxLength(20)]
        public string Unit { get; set; }

        //单价
        public decimal UnitPrice { get; set; }

        //总金额
        public decimal Amount { get; set; }

        //备注
        [MaxLength(4000)]
        public string Comments { get; set; }

        //预留字段1
        [MaxLength(1000)]
        public string Filed1 { get; set; }

        //预留字段2
        [MaxLength(1000)]
        public string Filed2 { get; set; }

        //预留字段3
        [MaxLength(1000)]
        public string Filed3 { get; set; }

        //预留字段4
        [MaxLength(1000)]
        public string Filed4 { get; set; }

        //预留字段5
        [MaxLength(1000)]
        public string Filed5 { get; set; }

        //预留字段6
        [MaxLength(1000)]
        public string Filed6 { get; set; }

        //预留字段7
        [MaxLength(1000)]
        public string Filed7 { get; set; }

        //预留字段8
        [MaxLength(1000)]
        public string Filed8 { get; set; }
    }

    public enum OrderStatus
    {
        未审核
    }
}