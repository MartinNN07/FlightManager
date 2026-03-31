using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Flights
{
    public class FlightIndexViewModel
    {
        [Display(Name = "Номер на полета")]
        public string FlightNumber { get; set; } = string.Empty;

        [Display(Name = "От")]
        public string DepartureCity { get; set; } = string.Empty;

        [Display(Name = "Летище (От)")]
        public string DepartureIata { get; set; } = string.Empty;

        [Display(Name = "До")]
        public string LandingCity { get; set; } = string.Empty;

        [Display(Name = "Летище (До)")]
        public string LandingIata { get; set; } = string.Empty;

        [Display(Name = "Излитане")]
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Кацане")]
        public DateTime LandingTime { get; set; }

        [Display(Name = "Пилот")]
        public string PilotName { get; set; } = string.Empty;

        [Display(Name = "Самолет")]
        public string AirplaneModel { get; set; } = string.Empty;

        [Display(Name = "Свободни места в икономична класа")]
        public int AvailableEconomySeats { get; set; }

        [Display(Name = "Свободни места в бизнес класа")]
        public int AvailableBusinessSeats { get; set; }
    }
}
