using FlightManager.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Reservations
{
    public class ReservationCreateViewModel
    {
        public ReservationCreateViewModel()
        {
            Passengers = new List<PassengerInputModel>();
        }

        [Required(ErrorMessage = "Номерът на полета е задължителен.")]
        [Display(Name = "Полет №")]
        public string FlightId { get; set; } = string.Empty;

        [Display(Name = "Град на излитане")]
        public string? DepartureCity { get; set; }

        [Display(Name = "Град на кацане")]
        public string? ArrivalCity { get; set; }

        [Display(Name = "Дата на полета")]
        public DateTime? FlightDate { get; set; }

        [Required(ErrorMessage = "Класът на седалката е задължителен.")]
        [Display(Name = "Клас на седалка")]
        public SeatingClass SeatClass { get; set; } = SeatingClass.Economy;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        [Display(Name = "Имейл за контакт")]
        public string ContactEmail { get; set; } = string.Empty;

        public List<PassengerInputModel> Passengers { get; set; } = new List<PassengerInputModel> { new PassengerInputModel() };
    }

    public class PassengerInputModel
    {
        [Required(ErrorMessage = "Собственото име е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Собственото име трябва да е между 2 и 50 символа.")]
        [Display(Name = "Собствено име")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Бащиното име е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Бащиното име трябва да е между 2 и 50 символа.")]
        [Display(Name = "Бащино име")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилното име е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилното име трябва да е между 2 и 50 символа.")]
        [Display(Name = "Фамилно име")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ЕГН-то е задължително.")]
        [Display(Name = "ЕГН")]
        public string EGN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        [StringLength(20)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Националността е задължителна.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Националността трябва да е между 2 и 60 символа.")]
        [Display(Name = "Националност")]
        public string Nationality { get; set; } = string.Empty;
    }
}