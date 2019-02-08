namespace CarDealer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public class Car
    {
        public Car()
        {
            this.PartsCar = new HashSet<PartCar>();
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public ICollection<PartCar> PartsCar { get; set; }

        public ICollection<Sale> Sales { get; set; }

        [NotMapped]
        public decimal Price => this.PartsCar.Select(pc => pc.Part.Price).Sum();
    }
}