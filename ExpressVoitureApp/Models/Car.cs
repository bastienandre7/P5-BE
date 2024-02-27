using System.ComponentModel.DataAnnotations;

namespace ExpressVoitureApp.Models
{
    public class Car
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
        public string? Image { get; set; } 
        public string? Description { get; set; }
        public virtual Repair? Repair { get; set; }
        public virtual Finance? Finance { get; set; }
    }
}
