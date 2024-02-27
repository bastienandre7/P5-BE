using System.ComponentModel.DataAnnotations;

namespace ExpressVoitureApp.Models
{
    public class Repair
    {
        [Key]
        public int Id { get; set; }
        public string RepairsName { get; set; } = string.Empty;
        public string RepairsCost { get; set; } = string.Empty;
        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
    }
}
