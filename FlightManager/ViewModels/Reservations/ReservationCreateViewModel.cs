using FlightManager.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Reservations
{
    public class ReservationCreateViewModel
    {
        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        [Display(Name = "Имейл за контакт")]
        public string ContactEmail { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Полет №")]
        public string FlightId { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Клас на седалка")]
        public SeatingClass SeatClass { get; set; } = SeatingClass.Economy;
    }
}