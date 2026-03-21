using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Data.Models
{
    public class Airport
    {
        [Key]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "IATA кодът трябва да бъде точно 3 големи букви.")]
        public required string IataCode { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Името на държавата трябва да е между 2 и 30 символа.")]
        public required string Country { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Името на града трябва да бъде между 2 и 30 символа.")]
        public required string City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Името на летището трябва да е между 2 и 100 символа.")]
        public required string AirportName { get; set; }

        public ICollection<Flight> DepartingFlights { get; set; } = new List<Flight>();
        public ICollection<Flight> ArrivingFlights { get; set; } = new List<Flight>();

        public override string ToString()
        {
            return $"{IataCode} - {AirportName} ({City}, {Country})";
        }
    }
}
