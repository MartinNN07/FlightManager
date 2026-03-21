using FlightManager.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static FlightManager.Data.Models.Reservation;

namespace FlightManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reservation>()
                .HasOne(r => r.Flight)
                .WithMany(f => f.Reservations)
                .HasForeignKey(r => r.FlightId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Passenger>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Passengers)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}