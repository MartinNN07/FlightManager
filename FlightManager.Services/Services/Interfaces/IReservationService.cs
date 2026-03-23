using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data.Models;

namespace FlightManager.Services.Services.Interfaces
{
    public interface IReservationService
    {
        /// <summary>
        /// Asynchronously retrieves all reservations available in the data source.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all reservations.
        /// The collection is empty if no reservations are found.</returns>
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();

        /// <summary>
        /// Asynchronously retrieves an reservation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation to retrieve. Must be an int. </param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the reservation with the specified
        /// identifier, or null if no matching reservation is found.</returns>
        Task<Reservation?> GetreservationByIdAsync(int reservationId);

        /// <summary>
        /// Asynchronously retrieves an reservation by its contact email.
        /// </summary>
        /// <param name="contactEmail">The contact emial to retrieve. Must be an string. </param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the reservation with the specified
        /// contact emial, or null if no matching contact emial is found.</returns>
        Task<Reservation?> GetreservationByContactEmailAsync(string contactEmail);

        /// <summary>
        /// Asynchronously creates a new reservation record using the specified reservation details.
        /// </summary>
        /// <param name="reservation">The reservation entity containing the details to be created. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateReservationAsync(Reservation reservation);

        /// <summary>
        /// Asynchronously updates the details of the specified reservation in the data store.
        /// </summary>
        /// <param name="updatedReservation">The reservation entity containing updated information. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateReservationAsync(Reservation updatedReservation);

        /// <summary>
        /// Asynchronously deletes the reservation with the specified identifier.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation to delete. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteReservationAsync(int reservationId);

        /// <summary>
        /// Determines asynchronously whether an reservation with the specified identifier exists.
        /// </summary>
        /// <param name="reservationId">The unique identifier of the reservation to check for existence. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the
        /// reservation exists; otherwise, <see langword="false"/>.</returns>
        Task<bool> ReservationExistsAsync(int reservationId);
    }
}
