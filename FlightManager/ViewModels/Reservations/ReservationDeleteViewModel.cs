using FlightManager.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Reservations
{
    public class ReservationDeleteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Имейл за контакт")]
        public string ContactEmail { get; set; } = string.Empty;

        [Display(Name = "Дата на резервация")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Полет №")]
        public string FlightId { get; set; } = string.Empty;

        [Display(Name = "Клас на седалка")]
        public SeatingClass SeatClass { get; set; }

        [Display(Name = "Брой пътници")]
        public int PassengerCount { get; set; }
    }
}