using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management.Models
{
    public class Items : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int DppQty { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal DppPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int? DepertmentId { get; set; }

        [ForeignKey("DepertmentId")]
        public List<Depertment> Depertment { get; set; }
    }
}
