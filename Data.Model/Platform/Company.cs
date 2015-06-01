using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Model.Base;

namespace Data.Model.Platform
{
    [Table("Company")]
    public class Company : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CompanyId { get; set; }

        //公司全名
        [MaxLength(200)]
        public string FullName { get; set; }

        //公司名简称
        [MaxLength(12)]
        public string ShortName { get; set; }

        //公司地址
        [MaxLength(200)]
        public string Address { get; set; }

        //公司邮编
        [MaxLength(15)]
        public string Postcode { get; set; }

        //电话
        [MaxLength(20)]
        public string Phone { get; set; }

        //传真
        [MaxLength(20)]
        public string Fax { get; set; }

        //联系人邮箱
        [MaxLength(100)]
        public string Email { get; set; }

        //公司网站
        [MaxLength(100)]
        public string Website { get; set; }

        //法人
        [MaxLength(30)]
        public string Corporation { get; set; }

        //联系人
        [MaxLength(30)]
        public string Contacter { get; set; }

        //联系人电话
        [MaxLength(30)]
        public string ContacterMobilePhone { get; set; }

        //营业执照扫描件路径
        [MaxLength(200)]
        public string BusinessLicenseImagePath { get; set; }

        //开户银行
        [MaxLength(100)]
        public string CompanyBank { get; set; }

        //开户银行帐号
        [MaxLength(50)]
        public string BankAccount { get; set; }

        //公司Logo图像路径
        [MaxLength(200)]
        public string CompanyLogoImagePath { get; set; }
    }
}