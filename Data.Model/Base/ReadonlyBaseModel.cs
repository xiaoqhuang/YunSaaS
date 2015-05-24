using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Model.Base
{
    public abstract class ReadonlyBaseModel
    {
        public ReadonlyBaseModel()
        {
            CreateTime = DateTime.Now;
        }

        [MaxLength(100)]
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; } 
    }
}