﻿using System.ComponentModel.DataAnnotations;

namespace PetClinic.DataProcessor.Dto.Import
{
    public class animalAidDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(typeof(decimal),  "0.01", "79228162514264337593543950335")]
        public decimal Price { get; set; }
    }
}