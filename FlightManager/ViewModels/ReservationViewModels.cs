using System.ComponentModel.DataAnnotations;
using FlightManager.Data.Models;
using FlightManager.Data.Validation;

namespace FlightManager.Web.ViewModels
{
    public class ReservationViewModels
    {
        public class ReservationCreateViewModel
        {
            public int FlightId { get; set; }
            public Flight? Flight { get; set; }

            [Required(ErrorMessage = "Имейлът е задължителен.")]
            [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
            [Display(Name = "Имейл за контакт")]
            public string ContactEmail { get; set; } = string.Empty;

            [Required]
            [MinLength(1, ErrorMessage = "Трябва да има поне един пътник.")]
            public List<PassengerInputModel> Passengers { get; set; } = new();
        }

        public class PassengerInputModel
        {
            [Required(ErrorMessage = "Собственото име е задължително.")]
            [StringLength(50)]
            [Display(Name = "Собствено име")]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Бащиното име е задължително.")]
            [StringLength(50)]
            [Display(Name = "Бащино име")]
            public string MiddleName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Фамилното име е задължително.")]
            [StringLength(50)]
            [Display(Name = "Фамилно име")]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "ЕГН-то е задължително.")]
            [EgnValidation]
            [Display(Name = "ЕГН")]
            public string EGN { get; set; } = string.Empty;

            [Required(ErrorMessage = "Телефонният номер е задължителен.")]
            [Phone(ErrorMessage = "Невалиден телефонен номер.")]
            [StringLength(20)]
            [Display(Name = "Телефон")]
            public string PhoneNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "Националността е задължителна.")]
            [StringLength(60)]
            [Display(Name = "Националност")]
            public string Nationality { get; set; } = string.Empty;

            [Required]
            [Display(Name = "Тип билет")]
            public TicketType TicketType { get; set; }
        }
    }
}
