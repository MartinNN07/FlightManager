using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Services.Services
{
    internal class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ReservationExistsAsync(int reservationId)
        {
            return await _context.Reservations.AnyAsync(r => r.Id == reservationId);
        }
        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await _context.Reservations.ToListAsync();
        }
        public async Task<Reservation?> GetreservationByIdAsync(int reservationId)
        {
            return await _context.Reservations.FindAsync(reservationId);
        }
        public async Task<Reservation?> GetreservationByContactEmailAsync(string contactEmail)
        {
            return await _context.Reservations.Where(r => r.ContactEmail == contactEmail).FirstOrDefaultAsync();
        }
        public async Task CreateReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateReservationAsync(Reservation updatedReservation)
        {
            Reservation? Reservation = await _context.Reservations.FindAsync(updatedReservation.Id);

            if (Reservation is null)
            {
                throw new KeyNotFoundException($"Reservation with ID '{updatedReservation.Id}' not found.");
            }

            Reservation.ContactEmail = updatedReservation.ContactEmail;
            Reservation.CreatedAt = updatedReservation.CreatedAt;
            Reservation.FlightId = updatedReservation.FlightId;
            Reservation.Flight = updatedReservation.Flight;
            Reservation.SeatClass = updatedReservation.SeatClass;
            Reservation.Passengers = updatedReservation.Passengers;
            _context.Reservations.Update(Reservation);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteReservationAsync(int reservationId)
        {
            Reservation? Reservation = await _context.Reservations.FindAsync(reservationId);

            if (Reservation is null)
            {
                throw new KeyNotFoundException($"Reservation with ID '{reservationId}' not found.");
            }

            _context.Reservations.Remove(Reservation);
            await _context.SaveChangesAsync();
        }
    }
}
