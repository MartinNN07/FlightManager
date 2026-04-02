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

        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // configure deletion behavior
            builder.Entity<Flight>()
                .HasOne(f => f.DepartureAirport)
                .WithMany(a => a.DepartingFlights)
                .HasForeignKey(f => f.DepartureAirportIataCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Flight>()
                .HasOne(f => f.LandingAirport)
                .WithMany(a => a.ArrivingFlights)
                .HasForeignKey(f => f.LandingAirportIataCode)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Passenger>()
                .HasMany(p => p.Reservations)
                .WithMany(r => r.Passengers)
                .UsingEntity(j => j.ToTable("PassengerReservations"));
        

            // ensure EGN is unique
            builder.Entity<User>()
                    .HasIndex(u => u.EGN)
                    .IsUnique();

            builder.Entity<Passenger>()
                .HasIndex(p => p.EGN)
                .IsUnique();
        }
    }
}