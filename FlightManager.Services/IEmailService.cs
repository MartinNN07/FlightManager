using FlightManager.Data.Models;

namespace FlightManager.Services
{
    public interface IEmailService
    {
        Task SendReservationConfirmationAsync(Reservation reservation, Flight flight);
    }
}