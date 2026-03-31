using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Airports
{
    public class AirportDetailsViewModel
    {
        [Display(Name = "IATA код")]
        public string IataCode { get; set; } = string.Empty;

        [Display(Name = "Държава")]
        public string Country { get; set; } = string.Empty;

        [Display(Name = "Град")]
        public string City { get; set; } = string.Empty;

        [Display(Name = "Летище")]
        public string AirportName { get; set; } = string.Empty;

        [Display(Name = "Заминаващи полети")]
        public int DepartingFlightsCount { get; set; }

        [Display(Name = "Пристигащи полети")]
        public int ArrivingFlightsCount { get; set; }
    }
}