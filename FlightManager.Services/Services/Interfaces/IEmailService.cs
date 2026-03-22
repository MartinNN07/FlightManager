using FlightManager.Data.Models;

namespace FlightManager.Services.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendReservationConfirmationAsync(Reservation reservation, Flight flight);
    }
}