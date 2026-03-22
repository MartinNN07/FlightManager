using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Airplane
{
    public class AirplaneViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int EconomyClassSeats { get; set; }
        public int BusinessClassSeats { get; set; }
        public int TotalSeats => EconomyClassSeats + BusinessClassSeats;
    }
}
