namespace FlightManager.Web.ViewModels.Flights
{
    public class FlightDetailsViewModel
    {
        public string FlightNumber { get; set; } = string.Empty;

        public string DepartureAirportName { get; set; } = string.Empty;
        public string DepartureCity { get; set; } = string.Empty;
        public string DepartureIata { get; set; } = string.Empty;

        public string LandingAirportName { get; set; } = string.Empty;
        public string LandingCity { get; set; } = string.Empty;
        public string LandingIata { get; set; } = string.Empty;

        public DateTime DepartureTime { get; set; }
        public TimeSpan FlightDuration { get; set; }

        public string PilotName { get; set; } = string.Empty;
        public string AirplaneModel { get; set; } = string.Empty;
        public string AirplaneId { get; set; } = string.Empty;

        public int TotalEconomySeats { get; set; }
        public int TotalBusinessSeats { get; set; }
        public int AvailableEconomySeats { get; set; }
        public int AvailableBusinessSeats { get; set; }
        public List<FlightPassengerViewModel> Passengers { get; set; } = new();
        public int PassengerPage { get; set; } = 1;
        public int PassengerPageSize { get; set; } = 10;
        public int TotalPassengers { get; set; }
        public int TotalPassengerPages => (int)Math.Ceiling((double)TotalPassengers / PassengerPageSize);
    }
}
