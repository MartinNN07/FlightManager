using FlightManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Services.Interfaces
{
    public interface IAirportService
    {
        /// <summary>
        /// Asynchronously retrieves a collection of all available airports.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of all
        /// airports. The collection will be empty if no airports are available.</returns>
        Task<IEnumerable<Airport>> GetAllAirportsAsync();

        /// <summary>
        /// Asynchronously retrieves airport information for the specified IATA code.
        /// </summary>
        /// <param name="airportIataCode">The three-letter IATA airport code to search for. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the airport information if
        /// found; otherwise, null.</returns>
        Task<Airport?> GetAirportByIataCodeAsync(string airportIataCode);

        /// <summary>
        /// Asynchronously retrieves a collection of airports located in the specified country.
        /// </summary>
        /// <param name="country">The name of the country for which to retrieve airports. This value cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of airports in the
        /// specified country. The collection is empty if no airports are found.</returns>
        Task<IEnumerable<Airport>> GetAirportsByCountryAsync(string country);

        /// <summary>
        /// Asynchronously retrieves a collection of airports located in the specified city.
        /// </summary>
        /// <param name="city">The name of the city for which to retrieve airports. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of airports in the
        /// specified city. The collection is empty if no airports are found.</returns>
        Task<IEnumerable<Airport>> GetAirportsByCityAsync(string city);

        /// <summary>
        /// Asynchronously retrieves a collection of airports that match the specified name.
        /// </summary>
        /// <remarks>The search is typically case-insensitive and may return multiple airports if the name
        /// is not unique.</remarks>
        /// <param name="airportName">The name or partial name of the airport to search for. This value cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of airports whose
        /// names match the specified value. The collection is empty if no airports are found.</returns>
        Task<IEnumerable<Airport>> GetAirportsByNameAsync(string airportName);

        /// <summary>
        /// Asynchronously creates a new airport record using the specified airport information.
        /// </summary>
        /// <param name="airport">The airport entity containing the details of the airport to create. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateAirportAsync(Airport airport);

        /// <summary>
        /// Asynchronously updates the details of the specified airport in the data store.
        /// </summary>
        /// <param name="airport">The airport entity containing the updated information. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAirportAsync(Airport airport);

        /// <summary>
        /// Asynchronously deletes the airport identified by the specified IATA code.
        /// </summary>
        /// <param name="airportIataCode">The three-letter IATA code of the airport to delete. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteAirportAsync(string airportIataCode);

        /// <summary>
        /// Asynchronously determines whether an airport with the specified IATA code exists.
        /// </summary>
        /// <param name="airportIataCode">The three-letter IATA airport code to check for existence. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if an
        /// airport with the specified IATA code exists; otherwise, <see langword="false"/>.</returns>
        Task<bool> AirportExistsAsync(string airportIataCode);
    }
}
