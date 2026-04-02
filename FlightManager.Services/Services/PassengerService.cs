using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Services.Services
{
    public class PassengerService : IPassengerService
    {
        private readonly ApplicationDbContext _context;

        public PassengerService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> PassengerExistsAsync(string passengerEGN)
        {
            return await _context.Passengers.AnyAsync(p => p.EGN == passengerEGN);
        }
        public async Task<IEnumerable<Passenger>> GetAllPassengersAsync()
        {
            return await _context.Passengers.ToListAsync();
        }
        public async Task<Passenger?> GetPassengerByEGNAsync(string passengerEGN)
        {
            return await _context.Passengers.Where(p => p.EGN == passengerEGN).FirstOrDefaultAsync();
        }
        public async Task<Passenger?> GetPassengerByReservationIdAsync(int reservationId)
        {
            return await _context.Passengers.FindAsync(reservationId);
        }
        public async Task CreatePassengerAsync(Passenger passenger)
        {
            await _context.Passengers.AddAsync(passenger);
            await _context.SaveChangesAsync();
        }
    }
}
