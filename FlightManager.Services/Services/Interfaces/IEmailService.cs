namespace FlightManager.Services.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendReservationConfirmationAsync(
            string toEmail,
            string flightNumber,
            string departureCity,
            string arrivalCity,
            DateTime departureTime,
            string seatClass,
            IEnumerable<string> passengerNames);
    }
}
