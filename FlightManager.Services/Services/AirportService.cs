using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Services
{
    public class AirportService : IAirportService
    {
        private readonly ApplicationDbContext _context;
        public AirportService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AirportExistsAsync(string airportIataCode)
        {
            return await _context.Airports.AnyAsync(a => a.IataCode == airportIataCode);
        }

        public async Task<IEnumerable<Airport>> GetAllAirportsAsync()
        {
            return await _context.Airports.ToListAsync();
        }

        public async Task<Airport?> GetAirportByIataCodeAsync(string airportIataCode)
        {
            return await _context.Airports.FindAsync(airportIataCode);
        }

        public async Task<IEnumerable<Airport>> GetAirportsByCountryAsync(string country)
        { 
            return await _context.Airports.Where(a => a.Country == country).ToListAsync();
        }

        public async Task<IEnumerable<Airport>> GetAirportsByCityAsync(string city)
        {
            return await _context.Airports.Where(a => a.City == city).ToListAsync();
        }

        public async Task<IEnumerable<Airport>> GetAirportsByNameAsync(string airportName)
        {
            return await _context.Airports.Where(a => a.AirportName == airportName).ToListAsync();
        }

        public async Task CreateAirportAsync(Airport airport)
        {
            if (await AirportExistsAsync(airport.IataCode))
            {
                throw new InvalidOperationException($"Airport with IATA code {airport.IataCode} already exists.");
            }

            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAirportAsync(Airport airport)
        {
            var existingAirport = await GetAirportByIataCodeAsync(airport.IataCode);
            if (existingAirport == null)
            {
                throw new InvalidOperationException($"Airport with IATA code {airport.IataCode} does not exist.");
            }

            existingAirport.Country = airport.Country;
            existingAirport.City = airport.City;
            existingAirport.AirportName = airport.AirportName;

            _context.Airports.Update(existingAirport);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAirportAsync(string airportIataCode)
        {
            var airport = await GetAirportByIataCodeAsync(airportIataCode);

            if (airport == null)
            {
                throw new InvalidOperationException($"Airport with IATA code {airportIataCode} does not exist.");
            }

            _context.Airports.Remove(airport);
            await _context.SaveChangesAsync();
        }
    }
}
