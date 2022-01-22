using Project_Management.Areas.Identity.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models
{
    public class Institute : BaseModel
    {
        [Key]
        public int InstituteId { get; set; }
        public string InstituteName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<ProjectManagementUser> ProjectManagementUser { get; set; }
        public List<DepertmentWiseInstitute> DepertmentWiseInstitute { get; set; }
    }
}
