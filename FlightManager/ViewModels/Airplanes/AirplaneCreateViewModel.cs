using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Airplane
{
    /// <summary>
    /// Represents the data required to create a new airplane, including identification, model, and seating capacities
    /// for different classes.
    /// </summary>
    /// <remarks>This view model is typically used in scenarios where user input is collected to register a
    /// new airplane in the system. All properties are required and include validation attributes to ensure data
    /// integrity.</remarks>
    public class AirplaneCreateViewModel
    {
        [Required(ErrorMessage = "Идентификаторът е задължителен.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Идентификаторът трябва да е между 2 и 20 символа.")]
        [Display(Name = "Идентификационен номер")]
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моделът е задължителен.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Моделът трябва да е между 2 и 50 символа.")]
        [Display(Name = "Модел на самолета")]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Капацитетът на икономичната класа е задължителен.")]
        [Range(1, int.MaxValue, ErrorMessage = "Трябва да има поне 1 място в икономичната класа.")]
        [Display(Name = "Места — Икономична класа")]
        public int EconomyClassSeats { get; set; }

        [Required(ErrorMessage = "Капацитетът на бизнес класата е задължителен.")]
        [Range(0, int.MaxValue, ErrorMessage = "Броят на бизнес местата не може да е отрицателен.")]
        [Display(Name = "Места — Бизнес класа")]
        public int BusinessClassSeats { get; set; }
    }
}
