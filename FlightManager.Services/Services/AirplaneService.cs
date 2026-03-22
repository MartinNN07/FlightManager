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
            await _context.Airplanes.AddAsync(airplane);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAirplaneAsync(Airplane updatedAirplane)
        {
            Airplane? airplane = await _context.Airplanes.FindAsync(updatedAirplane.Id);

            if (airplane is null)
            {
                throw new KeyNotFoundException($"Airplane with ID '{updatedAirplane.Id}' not found.");
            }

            airplane.Model = updatedAirplane.Model;
            airplane.EconomyClassSeats = updatedAirplane.EconomyClassSeats;
            airplane.BusinessClassSeats = updatedAirplane.BusinessClassSeats;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAirplaneAsync(string airplaneId)
        { 
            Airplane? airplane = await _context.Airplanes.FindAsync(airplaneId);

            if (airplane is null)
            {
                throw new KeyNotFoundException($"Airplane with ID '{airplaneId}' not found.");
            }

            _context.Airplanes.Remove(airplane);
            await _context.SaveChangesAsync();
        }
    }
}
