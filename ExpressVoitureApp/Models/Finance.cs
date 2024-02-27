using System.ComponentModel.DataAnnotations;

namespace ExpressVoitureApp.Models
{
    public class Finance
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public int PurchaseAmount { get; set; }
        public int Price { get; set; }
        public DateTimeOffset SaleDate { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
    }
}
