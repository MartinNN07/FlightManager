using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlightManager.Web.ViewModels;

namespace FlightManager.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public ReservationController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string? emailFilter, int page = 1)
        {
            int pageSize = 10;

            var query = _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .AsQueryable();

            if (!string.IsNullOrEmpty(emailFilter))
            {
                query = query.Where(r => r.ContactEmail.Contains(emailFilter));
            }

            int totalCount = await query.CountAsync();

            var reservations = await query
                .OrderByDescending(r => r.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.EmailFilter = emailFilter;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return View(reservations);
        }

        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .FirstOrDefaultAsync(r => r.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        public async Task<IActionResult> Create(int flightId)
        {
            var flights = await _context.Flights.FindAsync(flightId);
            if (flights == null)
            {
                return NotFound();
            }

            var vm = new ReservationCreateViewModel
            {
                FlightId = flightId,
                Flight = flight,
                Passengers = new List<PassengerInputModel> { new PassengerInputModel() }
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationCreateViewModel vm)
        {
            var flight = await _context.Flights.FindAsync(vm.FlightId);
            if (flight == null) return NotFound();

            vm.Flight = flight;

            if (!ModelState.IsValid) return View(vm);

            int economyCount = vm.Passengers.Count(p => p.TicketType == TicketType.Economy);
            int businessCount = vm.Passengers.Count(p => p.TicketType == TicketType.Business);

            if (economyCount > flight.AvailableEconomySeats)
            {
                ModelState.AddModelError("", $"Недостатъчно места в Икономична класа. Налични: {flight.AvailableEconomySeats}");
                return View(vm);
            }

            if (businessCount > flight.AvailableBusinessSeats)
            {
                ModelState.AddModelError("", $"Недостатъчно места в Бизнес класа. Налични: {flight.AvailableBusinessSeats}");
                return View(vm);
            }

            var reservation = new Reservation
            {
                FlightId = vm.FlightId,
                ContactEmail = vm.ContactEmail,
                CreatedAt = DateTime.UtcNow,
                Passengers = vm.Passengers.Select(p => new Passenger
                {
                    FirstName = p.FirstName,
                    MiddleName = p.MiddleName,
                    LastName = p.LastName,
                    EGN = p.EGN,
                    PhoneNumber = p.PhoneNumber,
                    Nationality = p.Nationality,
                    TicketType = p.TicketType
                }).ToList()
            };

            flight.AvailableEconomySeats -= economyCount;
            flight.AvailableBusinessSeats -= businessCount;

            _context.Reservations.Add(reservation);
            _context.Update(flight);
            await _context.SaveChangesAsync();

            await _emailService.SendReservationConfirmationAsync(reservation, flight);

            return RedirectToAction("Confirmation", new { id = reservation.Id });
        }

        public async Task<IActionResult> Confirmation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passengers)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return NotFound();
            return View(reservation);
        }
    }
}
