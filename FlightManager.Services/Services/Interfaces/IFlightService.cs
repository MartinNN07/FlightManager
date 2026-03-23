using FlightManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Services.Interfaces
{
    public interface IFlightService
    {
        /// <summary>
        /// Asynchronously retrieves all available flights.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all flights. The
        /// collection will be empty if no flights are available.</returns>
        Task<IEnumerable<Flight>> GetAllFlightsAsync();
        /// <summary>
        /// Asynchronously retrieves the flight information for the specified flight number.
        /// </summary>
        /// <param name="flightNumber">The unique identifier of the flight to retrieve. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the flight information if found;
        /// otherwise, null.</returns>
        Task<Flight?> GetFlightByNumberAsync(string flightNumber);
        /// <summary>
        /// Retrieves a collection of flights scheduled between the specified start and end dates.
        /// </summary>
        /// <param name="startDate">The start date and time of the range to search for flights. Only flights departing on or after this date are
        /// included.</param>
        /// <param name="endDate">The end date and time of the range to search for flights. Only flights departing on or before this date are
        /// included.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of
        /// flights scheduled within the specified date range. The collection is empty if no flights are found.</returns>
        Task <IEnumerable<Flight>> GetFlightsBetweenDates(DateTime startDate, DateTime endDate);
        /// <summary>
        /// Asynchronously retrieves a collection of flights departing from the specified airport.
        /// </summary>
        /// <param name="departureAirportIataCode">The IATA code of the departure airport. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of flights
        /// departing from the specified airport. The collection is empty if no flights are found.</returns>
        Task <IEnumerable<Flight>> GetFlightsByDepartureAirportAsync(string departureAirportIataCode);
        /// <summary>
        /// Asynchronously retrieves a collection of flights that are scheduled to land at the specified airport.
        /// </summary>
        /// <param name="arrivalAirportIataCode">The IATA code of the landing airport to filter flights by. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of
        /// flights arriving at the specified airport. The collection is empty if no matching flights are found.</returns>
        Task <IEnumerable<Flight>> GetFlightsByLandingAirportAsync(string arrivalAirportIataCode);
        /// <summary>
        /// Asynchronously determines whether a flight with the specified flight number exists.
        /// </summary>
        /// <param name="flightNumber">The unique identifier of the flight to check for existence. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if a
        /// flight with the specified flight number exists; otherwise, <see langword="false"/>.</returns>
        Task<bool> FlightExistsAsync(string flightNumber);
        /// <summary>
        /// Asynchronously creates a new flight record using the specified flight details.
        /// </summary>
        /// <param name="flight">The flight information to be created. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateFlightAsync(Flight flight);
        /// <summary>
        /// Asynchronously updates the details of the specified flight in the data store.
        /// </summary>
        /// <param name="flight">The flight entity containing the updated information. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateFlightAsync(Flight flight);
        /// <summary>
        /// Asynchronously deletes the flight with the specified flight number.
        /// </summary>
        /// <param name="flightNumber">The unique identifier of the flight to delete. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteFlightAsync(string flightNumber);
    }
}
