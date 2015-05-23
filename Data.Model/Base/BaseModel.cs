using System;
using System.ComponentModel.DataAnnotations;

namespace Data.Model.Base
{
    public class BaseModel : ReadonlyBaseModel
    {
        [MaxLength(100)]
        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}