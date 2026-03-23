using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Services.Services
{
    public class AirplaneService : IAirplaneService
    {
        private readonly ApplicationDbContext _context;

        public AirplaneService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AirplaneExistsAsync(string airplaneId)
        {
            return await _context.Airplanes.AnyAsync(a => a.Id == airplaneId);
        }
        public async Task<IEnumerable<Airplane>> GetAllAirplanesAsync()
        {
            return await _context.Airplanes.ToListAsync();
        }
        public async Task<Airplane?> GetAirplaneByIdAsync(string airplaneId)
        {
            return await _context.Airplanes.FindAsync(airplaneId);
        }
        public async Task CreateAirplaneAsync(Airplane airplane)
        {
            if (await AirplaneExistsAsync(airplane.Id))
            {
                throw new InvalidOperationException($"Airplane with ID '{airplane.Id}' already exists.");
            }

            await _context.Airplanes.AddAsync(airplane);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAirplaneAsync(Airplane airplane)
        {
            Airplane? existingAirplane = await GetAirplaneByIdAsync(airplane.Id);

            if (existingAirplane is null)
            {
                throw new KeyNotFoundException($"Airplane with ID '{airplane.Id}' not found.");
            }

            existingAirplane.Model = airplane.Model;
            existingAirplane.EconomyClassSeats = airplane.EconomyClassSeats;
            existingAirplane.BusinessClassSeats = airplane.BusinessClassSeats;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAirplaneAsync(string airplaneId)
        { 
            Airplane? airplane = await GetAirplaneByIdAsync(airplaneId);

            if (airplane is null)
            {
                throw new KeyNotFoundException($"Airplane with ID '{airplaneId}' not found.");
            }

            _context.Airplanes.Remove(airplane);
            await _context.SaveChangesAsync();
        }
    }
}
