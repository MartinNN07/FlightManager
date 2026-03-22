using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlightManager.Data.Models
{
    public class Flight
    {
        [Key]
        [RegularExpression(@"^[A-Z]{2}\d{3,4}$", ErrorMessage = "Номерът на полета трябва да бъде в правилния формат.")]
        [Display(Name = "Номер на полета")]
        public string FlightNumber { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(DepartureAirport))]
        [Display(Name = "Летище на излитане")]
        public string DepartureAirportIataCode { get; set; } = string.Empty;
        public Airport DepartureAirport { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(LandingAirport))]
        [Display(Name = "Летище на кацане")]
        public string LandingAirportIataCode { get; set; } = string.Empty;
        public Airport LandingAirport { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Airplane))]
        public string AirplaneId { get; set; } = string.Empty;
        public Airplane Airplane { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Името на пилота трябва да бъде минимум два сивмола.")]
        [Display(Name = "Име на пилота")]
        public string PilotName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата и час на излитане")]
        public DateTime DepartureTime { get; set; } = DateTime.UtcNow.AddHours(1);

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Дата и час на кацане")]
        public DateTime LandingTime { get; set; } = DateTime.UtcNow.AddHours(2);
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        public int ReservedEconomySeats => Reservations.Where(r => r.SeatClass == SeatingClass.Economy).Sum(r => r.Passengers.Count);
        public int ReservedBusinessSeats => Reservations.Where(r => r.SeatClass == SeatingClass.Business).Sum(r => r.Passengers.Count);

        public int AvailableEconomySeats => Airplane.EconomyClassSeats - ReservedEconomySeats;
        public int AvailableBusinessSeats => Airplane.BusinessClassSeats - ReservedBusinessSeats;

    }
}
