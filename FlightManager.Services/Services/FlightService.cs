using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Services
{
    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext _context;

        public FlightService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateFlightAsync(Flight flight)
        {
            if (await FlightExistsAsync(flight.FlightNumber))
            {
                throw new InvalidOperationException($"A flight with the number {flight.FlightNumber} already exists.");
            }

            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFlightAsync(string flightNumber)
        {
            var flight = await GetFlightByNumberAsync(flightNumber);
            if (flight is null)
            {
                throw new InvalidOperationException($"Flight with number {flightNumber} does not exist.");
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> FlightExistsAsync(string flightNumber)
        {
            return await _context.Flights.AnyAsync(f => f.FlightNumber == flightNumber);
        }

        public async Task<IEnumerable<Flight>> GetAllFlightsAsync()
        {
            return await _context.Flights
                .Include(f => f.DepartureAirport)
                .Include(f => f.LandingAirport)
                .Include(f => f.Airplane)
                .Include(f => f.Reservations)
                .ToListAsync();
        }

        public async Task<Flight?> GetFlightByNumberAsync(string flightNumber)
        {
            return await _context.Flights
                .Include(f => f.DepartureAirport)
                .Include(f => f.LandingAirport)
                .Include(f => f.Airplane)
                .Include(f => f.Reservations)
                .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
        }

        public async Task<IEnumerable<Flight>> GetFlightsBetweenDates(DateTime startDate, DateTime endDate)
        {
            return await _context.Flights
                .Include(f => f.DepartureAirport)
                .Include(f => f.LandingAirport)
                .Include(f => f.Airplane)
                .Include(f => f.Reservations)
                .Where(f => f.DepartureTime >= startDate && f.DepartureTime <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByLandingAirportAsync(string arrivalAirportIataCode)
        {
            return await _context.Flights
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.LandingAirport)
                    .Include(f => f.Airplane)
                    .Include(f => f.Reservations)
                    .Where(f => f.LandingAirportIataCode == arrivalAirportIataCode)
                    .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetFlightsByDepartureAirportAsync(string departureAirportIataCode)
        {
            return await _context.Flights
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.LandingAirport)
                    .Include(f => f.Airplane)
                    .Include(f => f.Reservations)
                    .Where(f => f.DepartureAirportIataCode == departureAirportIataCode)
                    .ToListAsync();
        }

        public async Task UpdateFlightAsync(Flight flight)
        {
            var existingFlight = await GetFlightByNumberAsync(flight.FlightNumber);
            if (existingFlight is null)
            {
                throw new InvalidOperationException($"Flight with number {flight.FlightNumber} does not exist.");
            }

            existingFlight.DepartureAirportIataCode = flight.DepartureAirportIataCode;
            existingFlight.LandingAirportIataCode = flight.LandingAirportIataCode;
            existingFlight.AirplaneId = flight.AirplaneId;
            existingFlight.PilotName = flight.PilotName;
            existingFlight.DepartureTime = flight.DepartureTime;
            existingFlight.LandingTime = flight.LandingTime;

            _context.Update(existingFlight);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Flight>> GetFlightsBySearchTermsAsync(string? departureTerm, string? arrivalTerm)
        {
            var query = _context.Flights
                .Include(f => f.DepartureAirport)
                .Include(f => f.LandingAirport)
                .Include(f => f.Airplane)
                .Include(f => f.Reservations)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(departureTerm))
            {
                string depNorm = departureTerm.Trim().ToLower();
                query = query.Where(f =>
                    f.FlightNumber.ToLower().Contains(depNorm) ||
                    f.DepartureAirportIataCode.ToLower().Contains(depNorm) ||
                    f.DepartureAirport.City.ToLower().Contains(depNorm)
                );
            }

            if (!string.IsNullOrWhiteSpace(arrivalTerm))
            {
                string arrNorm = arrivalTerm.Trim().ToLower();
                query = query.Where(f =>
                    f.FlightNumber.ToLower().Contains(arrNorm) ||
                    f.LandingAirportIataCode.ToLower().Contains(arrNorm) ||
                    f.LandingAirport.City.ToLower().Contains(arrNorm)
                );
            }

            return await query.ToListAsync();
        }
    }
}
