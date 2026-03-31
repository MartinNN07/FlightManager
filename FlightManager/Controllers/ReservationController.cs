using FlightManager.Services.Services.Interfaces;
using FlightManager.Web.Mappers;
using FlightManager.Web.ViewModels.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManager.Web.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IFlightService _flightService;

        public ReservationsController(IReservationService reservationService, IFlightService flightService)
        {
            _reservationService = reservationService;
            _flightService = flightService;
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
            return View();
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

        private async Task PopulateFlightsDropDownAsync(string? selectedId = null)
        {
            var flights = await _flightService.GetAllFlightsAsync();
            ViewData["FlightId"] = new SelectList(flights, "Id", "Id", selectedId);
        }
    }
}