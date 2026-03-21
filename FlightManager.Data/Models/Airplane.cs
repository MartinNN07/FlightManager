using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Data.Models
{
    public class Airplane
    {
        [Key]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Идентификаторът на самолета трябва да е между 2 и 20 символа.")]
        public required string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Моделът на самолета трябва да е между 2 и 50 символа.")]
        public required string Model { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Капацитетът на икономичната класа трябва да бъде положително число.")]
        public required int EconomyClassSeats { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Капацитетът на бизнес класата трябва да бъде положително число.")]
        public required int BuisnessClassSeats { get; set; }
    }
}
