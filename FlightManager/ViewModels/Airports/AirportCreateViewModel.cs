using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Airports
{
    public class AirportCreateViewModel
    {
        [Required(ErrorMessage = "IATA кодът е задължителен.")]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "IATA кодът трябва да бъде точно 3 главни букви.")]
        [Display(Name = "IATA код")]
        public string IataCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Държавата е задължителна.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Името на държавата трябва да е между 2 и 30 символа.")]
        [Display(Name = "Държава")]
        public string Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Градът е задължителен.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Името на града трябва да е между 2 и 30 символа.")]
        [Display(Name = "Град")]
        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Името на летището е задължително.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Името на летището трябва да е между 2 и 100 символа.")]
        [Display(Name = "Летище")]
        public string AirportName { get; set; } = string.Empty;
    }
}