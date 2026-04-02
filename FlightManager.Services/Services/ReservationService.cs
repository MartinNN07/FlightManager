using System;
using System.Collections.Generic;
using System.Text;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Services.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        private IQueryable<Reservation> ReservationsWithDetails()
        {
            return _context.Reservations
                .Include(r => r.Passengers)
                .Include(r => r.Flight)
                    .ThenInclude(f => f!.DepartureAirport)
                .Include(r => r.Flight)
                    .ThenInclude(f => f!.LandingAirport);
        }

        public async Task<bool> ReservationExistsAsync(int reservationId)
        {
            return await _context.Reservations.AnyAsync(r => r.Id == reservationId);
        }

        public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
        {
            return await ReservationsWithDetails().ToListAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(int reservationId)
        {
            return await ReservationsWithDetails()
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }

        public async Task<Reservation?> GetReservationByContactEmailAsync(string contactEmail)
        {
            return await ReservationsWithDetails()
                .FirstOrDefaultAsync(r => r.ContactEmail == contactEmail);
        }

        public async Task CreateReservationAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }
    }
}