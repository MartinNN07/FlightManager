namespace FlightManager.Web.ViewModels.Airplane
{
    /// <summary>
    /// Represents the details of an airplane, including its identifier, model, and seat configuration.
    /// </summary>
    public class AirplaneDetailsViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int EconomyClassSeats { get; set; }
        public int BusinessClassSeats { get; set; }
        public int TotalSeats => EconomyClassSeats + BusinessClassSeats;
    }
}
