using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management.Models
{
    public class Institute : BaseModel
    {
        [Key]
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserId { get; set; }
        public List<Depertment> Depertment { get; set; }
    }
}
