using FlightManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Services.Interfaces
{
    public interface IAirplaneService
    {
        /// <summary>
        /// Asynchronously retrieves all airplanes available in the data source.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all airplanes.
        /// The collection is empty if no airplanes are found.</returns>
        Task<IEnumerable<Airplane>> GetAllAirplanesAsync();

        /// <summary>
        /// Asynchronously retrieves an airplane by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the airplane to retrieve. Must be a string. </param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the airplane with the specified
        /// identifier, or null if no matching airplane is found.</returns>
        Task<Airplane?> GetAirplaneByIdAsync(string airplaneId);

        /// <summary>
        /// Asynchronously creates a new airplane record using the specified airplane details.
        /// </summary>
        /// <param name="airplane">The airplane entity containing the details to be created. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous create operation.</returns>
        Task CreateAirplaneAsync(Airplane airplane);

        /// <summary>
        /// Asynchronously updates the details of the specified airplane in the data store.
        /// </summary>
        /// <param name="airplane">The airplane entity containing updated information. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        Task UpdateAirplaneAsync(Airplane updatedAirplane);

        /// <summary>
        /// Asynchronously deletes the airplane with the specified identifier.
        /// </summary>
        /// <param name="airplaneId">The unique identifier of the airplane to delete. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        Task DeleteAirplaneAsync(string airplaneId);

        /// <summary>
        /// Determines asynchronously whether an airplane with the specified identifier exists.
        /// </summary>
        /// <param name="airplaneId">The unique identifier of the airplane to check for existence. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains <see langword="true"/> if the
        /// airplane exists; otherwise, <see langword="false"/>.</returns>
        Task<bool> AirplaneExistsAsync(string airplaneId);

    }
}
