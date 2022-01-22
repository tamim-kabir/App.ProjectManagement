using Microsoft.AspNetCore.Identity;
using Project_Management.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management.Areas.Identity.Data
{
    public class ProjectManagementUser : IdentityUser
    {
        public int? InstituteId { get; set; }

        [ForeignKey("InstituteId")]
        public Institute Institute { get; set; }
    }
}
