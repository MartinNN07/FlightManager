using FlightManager.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Data
{
    public static class Seeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await context.Database.MigrateAsync();

            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
            await SeedAirplanesAsync(context);
            await SeedAirportsAsync(context);
            await SeedFlightsAsync(context);
            await SeedReservationsAsync(context);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Employee" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private static async Task SeedUsersAsync(UserManager<User> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@flightmanager.com") == null)
            {
                var admin = new User
                {
                    UserName = "admin@flightmanager.com",
                    Email = "admin@flightmanager.com",
                    FirstName = "Admin",
                    LastName = "User",
                    EGN = "1234567890",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }

            if (await userManager.FindByEmailAsync("employee@flightmanager.com") == null)
            {
                var employee = new User
                {
                    UserName = "employee@flightmanager.com",
                    Email = "employee@flightmanager.com",
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    EGN = "9876543210",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(employee, "Employee@123");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(employee, "Employee");
            }
        }

        private static async Task SeedAirplanesAsync(ApplicationDbContext context)
        {
            if (await context.Airplanes.AnyAsync()) return;

            var airplanes = new List<Airplane>
            {
                new Airplane { Id = "LZ-BHE", Model = "Boeing 737-800",    EconomyClassSeats = 162, BusinessClassSeats = 12 },
                new Airplane { Id = "LZ-FBD", Model = "Airbus A320",       EconomyClassSeats = 150, BusinessClassSeats = 12 },
                new Airplane { Id = "LZ-CGY", Model = "Airbus A321",       EconomyClassSeats = 180, BusinessClassSeats = 16 },
                new Airplane { Id = "LZ-AOB", Model = "Boeing 737-700",    EconomyClassSeats = 128, BusinessClassSeats = 8  },
            };

            await context.Airplanes.AddRangeAsync(airplanes);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAirportsAsync(ApplicationDbContext context)
        {
            if (await context.Airports.AnyAsync()) return;

            var airports = new List<Airport>
            {
                new Airport { IataCode = "SOF", Country = "Bulgaria",      City = "Sofia",     AirportName = "Sofia Airport" },
                new Airport { IataCode = "LHR", Country = "UK",            City = "London",    AirportName = "Heathrow Airport" },
                new Airport { IataCode = "CDG", Country = "France",        City = "Paris",     AirportName = "Charles de Gaulle Airport" },
                new Airport { IataCode = "AMS", Country = "Netherlands",   City = "Amsterdam", AirportName = "Amsterdam Airport Schiphol" },
                new Airport { IataCode = "FRA", Country = "Germany",       City = "Frankfurt", AirportName = "Frankfurt Airport" },
                new Airport { IataCode = "FCO", Country = "Italy",         City = "Rome",      AirportName = "Leonardo da Vinci Airport" },
            };

            await context.Airports.AddRangeAsync(airports);
            await context.SaveChangesAsync();
        }

        private static async Task SeedFlightsAsync(ApplicationDbContext context)
        {
            if (await context.Flights.AnyAsync()) return;

            var flights = new List<Flight>
            {
                new Flight
                {
                    FlightNumber = "FB101",
                    DepartureAirportIataCode = "SOF",
                    LandingAirportIataCode = "LHR",
                    AirplaneId = "LZ-BHE",
                    PilotName = "Georgi Ivanov",
                    DepartureTime = DateTime.UtcNow.AddDays(1),
                    LandingTime = DateTime.UtcNow.AddDays(1).AddHours(3),
                },
                new Flight
                {
                    FlightNumber = "FB202",
                    DepartureAirportIataCode = "SOF",
                    LandingAirportIataCode = "CDG",
                    AirplaneId = "LZ-FBD",
                    PilotName = "Dimitar Kolev",
                    DepartureTime = DateTime.UtcNow.AddDays(2),
                    LandingTime = DateTime.UtcNow.AddDays(2).AddHours(3).AddMinutes(30),
                },
                new Flight
                {
                    FlightNumber = "FB303",
                    DepartureAirportIataCode = "LHR",
                    LandingAirportIataCode = "SOF",
                    AirplaneId = "LZ-CGY",
                    PilotName = "Stefan Nikolov",
                    DepartureTime = DateTime.UtcNow.AddDays(3),
                    LandingTime = DateTime.UtcNow.AddDays(3).AddHours(3),
                },
                new Flight
                {
                    FlightNumber = "FB404",
                    DepartureAirportIataCode = "AMS",
                    LandingAirportIataCode = "SOF",
                    AirplaneId = "LZ-AOB",
                    PilotName = "Nikolay Georgiev",
                    DepartureTime = DateTime.UtcNow.AddDays(4),
                    LandingTime = DateTime.UtcNow.AddDays(4).AddHours(2).AddMinutes(45),
                },
            };

            await context.Flights.AddRangeAsync(flights);
            await context.SaveChangesAsync();
        }

        private static async Task SeedReservationsAsync(ApplicationDbContext context)
        {
            if (await context.Reservations.AnyAsync()) return;

            var reservations = new List<Reservation>
            {
                new Reservation
                {
                    ContactEmail = "maria.petrova@email.com",
                    FlightId = "FB101",
                    SeatClass = SeatingClass.Economy,
                    Passengers = new List<Passenger>
                    {
                        new Passenger { FirstName = "Maria",  MiddleName = "Ivanova",  LastName = "Petrova",  EGN = "8501154231", PhoneNumber = "+359888111222", Nationality = "Bulgarian" },
                        new Passenger { FirstName = "Petar",  MiddleName = "Ivanov",   LastName = "Petrov",   EGN = "8203271543", PhoneNumber = "+359888333444", Nationality = "Bulgarian" },
                    }
                },
                new Reservation
                {
                    ContactEmail = "john.smith@email.com",
                    FlightId = "FB101",
                    SeatClass = SeatingClass.Business,
                    Passengers = new List<Passenger>
                    {
                        new Passenger { FirstName = "John",   MiddleName = "Robert",   LastName = "Smith",    EGN = "7912083456", PhoneNumber = "+447911123456",  Nationality = "British"   },
                    }
                },
                new Reservation
                {
                    ContactEmail = "elena.todorova@email.com",
                    FlightId = "FB202",
                    SeatClass = SeatingClass.Economy,
                    Passengers = new List<Passenger>
                    {
                        new Passenger { FirstName = "Elena",  MiddleName = "Todorova", LastName = "Hristova", EGN = "9106224512", PhoneNumber = "+359877555666", Nationality = "Bulgarian" },
                        new Passenger { FirstName = "Viktor", MiddleName = "Hristov",  LastName = "Todorov",  EGN = "8807193287", PhoneNumber = "+359877777888", Nationality = "Bulgarian" },
                        new Passenger { FirstName = "Sofia",  MiddleName = "Georgieva",LastName = "Dimitrova",EGN = "9304082341", PhoneNumber = "+359877999000", Nationality = "Bulgarian" },
                    }
                },
            };

            await context.Reservations.AddRangeAsync(reservations);
            await context.SaveChangesAsync();
        }
    }
}