using FlightManager.Data.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightManager.Data.Models
{
    public class Passenger
    {
        [Key]
        [Required(ErrorMessage = "ЕГН-то е задължително.")]
        [EgnValidation]
        [Display(Name = "ЕГН")]
        public string EGN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Собственото име е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Собственото име трябва да е между 2 и 50 символа.")]
        [Display(Name = "Собствено име на пътника")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Бащиното име е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Бащиното име трябва да е между 2 и 50 символа.")]
        [Display(Name = "Бащино име на пътника")]
        public string MiddleName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилното име е задължително.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилното име трябва да е между 2 и 50 символа.")]
        [Display(Name = "Фамилното име на пътника")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефонният номер е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        [StringLength(20)]
        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Националността на пътника е задължителна.")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Националността на пътника трябва да е между 2 и 60 символа.")]
        [Display(Name = "Националността на пътника")]
        public string Nationality { get; set; } = string.Empty;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
