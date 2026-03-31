using FlightManager.Services.Services.Interfaces;
using FlightManager.Web.Mappers;
using FlightManager.Web.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace FlightManager.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IFlightService _flightService;
        private readonly IPassengerService _passengerService;

        public ReservationsController(
            IReservationService reservationService,
            IFlightService flightService,
            IPassengerService passengerService)
        {
            _reservationService = reservationService;
            _flightService = flightService;
            _passengerService = passengerService;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            var viewModels = ReservationMapper.ToIndexViewModelList(reservations);
            return View(viewModels);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var reservation = await _reservationService.GetreservationByIdAsync(id.Value);

            if (reservation == null)
                return NotFound();

            var viewModel = ReservationMapper.ToDetailsViewModel(reservation);
            return View(viewModel);
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create()
        {
            await PopulateFlightsDropDownAsync();
            return View(new ReservationCreateViewModel());
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var reservation = ReservationMapper.ToModel(viewModel);
                await _reservationService.CreateReservationAsync(reservation);

                // Save each passenger linked to the new reservation
                foreach (var passengerInput in viewModel.Passengers)
                {
                    var passenger = new FlightManager.Data.Models.Passenger
                    {
                        FirstName = passengerInput.FirstName,
                        MiddleName = passengerInput.MiddleName,
                        LastName = passengerInput.LastName,
                        EGN = passengerInput.EGN,
                        PhoneNumber = passengerInput.PhoneNumber,
                        Nationality = passengerInput.Nationality,
                        ReservationId = reservation.Id
                    };
                    await _passengerService.CreatePassengerAsync(passenger);
                }

                return RedirectToAction(nameof(Index));
            }

            await PopulateFlightsDropDownAsync(viewModel.FlightId);
            return View(viewModel);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reservation = await _reservationService.GetreservationByIdAsync(id.Value);

            if (reservation == null)
                return NotFound();

            var viewModel = ReservationMapper.ToEditViewModel(reservation);
            await PopulateFlightsDropDownAsync(viewModel.FlightId);
            return View(viewModel);
        }

        // POST: Reservations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReservationEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var reservation = ReservationMapper.ToModel(viewModel);
                    await _reservationService.UpdateReservationAsync(reservation);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            await PopulateFlightsDropDownAsync(viewModel.FlightId);
            return View(viewModel);
        }

        // ── Helpers ──────────────────────────────────────────────────────────

        private async Task PopulateFlightsDropDownAsync(string? selectedId = null)
        {
            var flights = await _flightService.GetAllFlightsAsync();

            ViewData["FlightId"] = new SelectList(flights, "Id", "Id", selectedId);

            // Build a JSON dictionary so the Create view can show city/date info
            // without an extra AJAX call. Adjust property names to match your
            // Flight model (e.g. DepartureCity, ArrivalCity, DepartureTime).
            var flightsJson = flights.ToDictionary(
                f => f.FlightNumber.ToString(),
                f => new
                {
                    departureCity = f.DepartureAirport.City,   // adjust to your property name
                    arrivalCity = f.LandingAirport.City,     // adjust to your property name
                    flightDate = f.DepartureTime.ToString("dd.MM.yyyy HH:mm") // adjust to your property name
                });

            ViewBag.FlightsJson = JsonSerializer.Serialize(flightsJson);
        }
    }
}