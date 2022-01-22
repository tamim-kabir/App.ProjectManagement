using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Management.Models
{
    public class Depertment : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Items> Items { get; set; }
        public List<DepertmentWiseInstitute> DepertmentWiseInstitute { get; set; }
    }
}
