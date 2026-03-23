namespace FlightManager.Web.ViewModels.Airplane
{
    /// <summary>
    /// Represents the data required to confirm the deletion of an airplane in the user interface.
    /// </summary>
    /// <remarks>This view model is typically used to display airplane details to the user before deletion,
    /// allowing confirmation of the action. It includes identifying and descriptive information about the airplane,
    /// such as its model and seat configuration.</remarks>
    public class AirplaneDeleteViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int EconomyClassSeats { get; set; }
        public int BusinessClassSeats { get; set; }
    }
}
