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
        public async Task<bool> PassengerExistsAsync(int passengerId)
        {
            return await _context.Passengers.AnyAsync(p => p.Id == passengerId);
        }
        public async Task<IEnumerable<Passenger>> GetAllPassengersAsync()
        {
            return await _context.Passengers.ToListAsync();
        }
        public async Task<Passenger?> GetPassengerByIdAsync(int passengerId)
        {
            return await _context.Passengers.FindAsync(passengerId);
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
        public async Task UpdatePassengerAsync(Passenger updatedPassenger)
        {
            Passenger? Passenger = await _context.Passengers.FindAsync(updatedPassenger.Id);

            if (Passenger is null)
            {
                throw new KeyNotFoundException($"Passsenger with ID '{updatedPassenger.Id}' not found.");
            }

            Passenger.FirstName = updatedPassenger.FirstName;
            Passenger.MiddleName = updatedPassenger.MiddleName;
            Passenger.LastName = updatedPassenger.LastName;
            Passenger.EGN = updatedPassenger.EGN;
            Passenger.PhoneNumber = updatedPassenger.PhoneNumber;
            Passenger.Nationality = updatedPassenger.Nationality;
            Passenger.ReservationId = updatedPassenger.ReservationId;
            _context.Passengers.Update(Passenger);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePassengerAsync(int passengerId)
        {
            Passenger? Passenger = await _context.Passengers.FindAsync(passengerId);

            if (Passenger is null)
            {
                throw new KeyNotFoundException($"Passenger with ID '{passengerId}' not found.");
            }

            _context.Passengers.Remove(Passenger);
            await _context.SaveChangesAsync();
        }
    }
}
