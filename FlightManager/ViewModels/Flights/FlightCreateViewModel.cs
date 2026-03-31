using FlightManager.Data.Validation;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Flights
{
    public class FlightCreateViewModel
    {
        [Required(ErrorMessage = "Номерът на полета е задължителен.")]
        [RegularExpression(@"^[A-Z]{2}\d{3,4}$", ErrorMessage = "Форматът трябва да е две главни букви + 3-4 цифри (напр. FB101).")]
        [Display(Name = "Номер на полета")]
        public string FlightNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Летището на излитане е задължително.")]
        [Display(Name = "Летище на излитане (IATA)")]
        public string DepartureAirportIataCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Летището на кацане е задължително.")]
        [Display(Name = "Летище на кацане (IATA)")]
        public string LandingAirportIataCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Самолетът е задължителен.")]
        [Display(Name = "Самолет")]
        public string AirplaneId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Името на пилота е задължително.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Името на пилота трябва да е между 2 и 100 символа.")]
        [Display(Name = "Пилот")]
        public string PilotName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Датата на излитане е задължителна.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Датата на излитане не може да бъде в миналото.")]
        [Display(Name = "Дата и час на излитане")]
        public DateTime DepartureTime { get; set; } = DateTime.Now.AddHours(1);

        [Required(ErrorMessage = "Датата на кацане е задължителна.")]
        [DataType(DataType.DateTime)]
        [LandingAfterDeparture(nameof(DepartureTime), ErrorMessage = "Датата на кацане трябва да бъде след датата на излитане.")]
        [Display(Name = "Дата и час на кацане")]
        public DateTime LandingTime { get; set; } = DateTime.Now.AddHours(2);
    }
}
