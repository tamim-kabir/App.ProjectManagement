using System;

namespace Project_Management.Models
{
    public class BaseModel
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        public bool Status { get; set; }
    }
}
