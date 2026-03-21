using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data.Models;

namespace FlightManager.Services
{
    public interface IEmailService
    {
        Task SendReservationConfirmationAsync(Reservation reservations);
    }
}
