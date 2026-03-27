using FlightManager.Data.Validation;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Passengers
{
    public class PassengerEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Собственото ime е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Собственото ime трябва да е между 2 и 50 символа.")]
        [Display(Name = "Собствено ime на пътника")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Бащиното ime е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Бащиното ime трябва да е между 2 и 50 символа.")]
        [Display(Name = "Бащино ime на пътника")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилното ime е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилното ime трябва да е между 2 и 50 символа.")]
        [Display(Name = "Фамилното ime на пътника")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "ЕГН-то е задължително.")]
        [EgnValidation]
        [Display(Name = "ЕГН")]
        public string EGN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        [StringLength(20)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Националността на пътника е задължителна.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Националността на пътника трябва да е между 2 и 60 символа.")]
        [Display(Name = "Националността на пътника")]
        public string Nationality { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Резервация №")]
        public int ReservationId { get; set; }
    }
}