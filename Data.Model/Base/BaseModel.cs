using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Model.Base
{
    public abstract class BaseModel : ReadonlyBaseModel
    {
        [MaxLength(100)]
        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}