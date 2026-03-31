namespace FlightManager.Web.ViewModels.Flights
{
    public class FlightDeleteViewModel
    {
        public string FlightNumber { get; set; } = string.Empty;
        public string DepartureCity { get; set; } = string.Empty;
        public string LandingCity { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public string AirplaneModel { get; set; } = string.Empty;
        public int ReservationCount { get; set; }
    }
}
