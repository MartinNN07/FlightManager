using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Airplane
{
    /// <summary>
    /// Represents the data required to edit an airplane, including its identifier, model, and seat configuration.
    /// </summary>
    public class AirplaneEditViewModel
    {
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
