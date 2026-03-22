using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data.Models;

namespace FlightManager.Services.Services.Interfaces
{
    internal interface IPassengerService
    {
        /// <summary>
        /// Asynchronously retrieves all passengers available in the data source.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all passengers.
        /// The collection is empty if no passengers are found.</returns>
        Task<IEnumerable<Passenger>> GetAllPassengersAsync();

        /// <summary>
        /// Asynchronously retrieves an passenger by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the passenger to retrieve. Must be an int. </param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the passenger with the specified
        /// identifier, or null if no matching passenger is found.</returns>
        Task<Passenger?> GetPassengerByIdAsync(int passengerId);

        /// <summary>
        /// Asynchronously retrieves an passenger by its EGN (Единен граждански номер).
        /// </summary>
        /// <param name="EGN">The EGN (Единен граждански номер) of the passenger to retrieve. Must be a string. </param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the passenger with the specified
        /// EGN (Единен граждански номер), or null if no matching passenger is found.</returns>
        Task<Passenger?> GetPassengerByEGNAsync(string passengerEGN);

        /// <summary>
        /// Asynchronously retrieves an passenger by its reservation unique identifier.
        /// </summary>
        /// <param name="reservationId">The reservation unique identifier of the passenger to retrieve. Must be an int. </param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the passenger with the specified
        /// reservation unique identifier, or null if no matching passenger is found.</returns>
        Task<Passenger?> GetPassengerByReservationIdAsync(int reservationId);

        /// <summary>
        /// Asynchronously creates a new passenger record using the specified passenger details.
        /// </summary>
        /// <param name="passenger">The passenger entity containing the details to be created. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreatePassengerAsync(Passenger passenger);

        /// <summary>
        /// Asynchronously updates the details of the specified passenger in the data store.
        /// </summary>
        /// <param name="updatedPassenger">The passenger entity containing updated information. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdatePassengerAsync(Passenger updatedPassenger);

        /// <summary>
        /// Asynchronously deletes the passenger with the specified identifier.
        /// </summary>
        /// <param name="passengerId">The unique identifier of the passenger to delete. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeletePassengerAsync(int passengerId);

        /// <summary>
        /// Determines asynchronously whether an passenger with the specified identifier exists.
        /// </summary>
        /// <param name="passengerId">The unique identifier of the passenger to check for existence. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the
        /// passenger exists; otherwise, <see langword="false"/>.</returns>
        Task<bool> PassengerExistsAsync(int passengerId);
    }
}
