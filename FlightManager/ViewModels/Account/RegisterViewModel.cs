using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Първото име е задължително.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Първо име")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [StringLength(50, MinimumLength = 2)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл адрес.")]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Паролата трябва да е поне 6 символа.")]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Потвърждението на паролата е задължително.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
        [Display(Name = "Потвърди парола")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "ЕГН-то е задължително.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ЕГН-то трябва да бъде точно 10 цифри.")]
        [Display(Name = "ЕГН")]
        public string EGN { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Невалиден телефонен номер.")]
        [Display(Name = "Телефон")]
        public string? PhoneNumber { get; set; }

        [StringLength(200, MinimumLength = 5)]
        [Display(Name = "Адрес")]
        public string? Address { get; set; }
    }
}
