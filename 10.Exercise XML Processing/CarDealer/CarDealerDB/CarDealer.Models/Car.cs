﻿using System;
using System.Collections.Generic;

namespace CarDealer.Models
{
    public class Car
    {

        public Car()
        {
            this.PartCars = new List<PartCar>();
            this.Sales = new List<Sale>();
        }

        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public ICollection<PartCar> PartCars { get; set; }

        public ICollection<Sale> Sales { get; set; } 
    }
}
