using Microsoft.AspNetCore.Mvc;
using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services;
using FlightManager.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
