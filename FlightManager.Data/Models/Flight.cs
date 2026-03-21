using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlightManager.Data.Models
{
    public class Flight
    {
        [Required]
        [ForeignKey(nameof(DepartureAirport))]
        public required string DepartureAirportIataCode { get; set; }
        public Airport? DepartureAirport { get; set; }

        [Required]
        [ForeignKey(nameof(LandingAirport))]
        public required string LandingAirportIataCode { get; set; }
        public Airport? LandingAirport { get; set; }

        [Required]
        [ForeignKey(nameof(Airplane))]
        public Airplane? Airplane { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Името на пилота трябва да бъде минимум два сивмола.")]
        public required string PilotName { get; set; }

        [Required]
        public required DateTime DepartureTime { get; set; }

        [Required]
        public required DateTime LandingTime { get; set; }
    }
}
