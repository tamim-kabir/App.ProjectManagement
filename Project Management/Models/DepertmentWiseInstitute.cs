using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Management.Models
{
    public class DepertmentWiseInstitute : BaseModel
    {
        public int Id { get; set; }
        public int? InstituteID { get; set; }
        [ForeignKey("InstituteID")]
        public Institute Institute { get; set; }
        public int? DepertmentId { get; set; }
        [ForeignKey("DepertmentId")]
        public Depertment Depertment { get; set; }
    }
}
