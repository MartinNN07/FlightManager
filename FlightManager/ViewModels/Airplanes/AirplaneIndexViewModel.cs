using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Airplane
{
    /// <summary>
    /// Used to display a row in the airplane list (Index view).
    /// Only exposes the data needed for the list — no need to load everything.
    /// </summary>
    public class AirplaneIndexViewModel
    {
        [Display(Name = "Идентификационен номер")]
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Модел на самолета")]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "Места — Икономична класа")]
        public int EconomyClassSeats { get; set; }

        [Display(Name = "Места — Бизнес класа")]
        public int BusinessClassSeats { get; set; }

        [Display(Name = "Общ брой места")]
        public int TotalSeats => EconomyClassSeats + BusinessClassSeats;

    }
}
