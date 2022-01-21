namespace Project_Management.Models
{
    public class LabUseItem : BaseModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int DppQty { get; set; }
        public int OperationalQty { get; set; }
        public int ScrapQty { get; set; }
        public int TotalStock { get; set; }
        public int RequiredQty { get; set; }

    }
}
