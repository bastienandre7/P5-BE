using System.ComponentModel.DataAnnotations;

namespace ExpressVoitureApp.Models
{
    public class CarViewModel
    {
        [Key]
        public int Id { get; set; }
        public int VINCode { get; set; }
        public int Year { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Finishing { get; set; } = string.Empty;
        public bool Availability { get; set; } = true;
        public DateTimeOffset AvailabilityDate { get; set; }
        public IFormFile? Photo { get; set; }
        public string? Description { get; set; }
        public string RepairsName { get; set; } = string.Empty;
        public string RepairsCost { get; set; } = string.Empty;
        public DateTimeOffset PurchaseDate { get; set; }
        public int PurchaseAmount { get; set; }
        public int Price { get; set; }
        public DateTimeOffset SaleDate { get; set; }
    }
}
