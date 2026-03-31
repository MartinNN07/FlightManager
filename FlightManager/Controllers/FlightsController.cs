using FlightManager.Services.Services.Interfaces;
using FlightManager.Web.Mappers;
using FlightManager.Web.ViewModels.Flights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FlightManager.Web.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IAirplaneService _airplaneService;

        public FlightsController(
            IFlightService flightService,
            IAirportService airportService,
            IAirplaneService airplaneService)
        {
            _flightService = flightService;
            _airportService = airportService;
            _airplaneService = airplaneService;
        }

        // GET: Flights
        [HttpGet]
        public async Task<IActionResult> Index(
            string? fromIata,
            string? toIata,
            DateTime? date,
            string? sortBy,
            int page = 1,
            int pageSize = 10)
        {
            var flights = await _flightService.GetAllFlightsAsync();

            if (!string.IsNullOrWhiteSpace(fromIata))
                flights = flights.Where(f => f.DepartureAirportIataCode == fromIata);

            if (!string.IsNullOrWhiteSpace(toIata))
                flights = flights.Where(f => f.LandingAirportIataCode == toIata);

            if (date.HasValue)
                flights = flights.Where(f => f.DepartureTime.Date == date.Value.Date);

            flights = sortBy switch
            {
                "departure_desc" => flights.OrderByDescending(f => f.DepartureTime),
                "from" => flights.OrderBy(f => f.DepartureAirport.City),
                "to" => flights.OrderBy(f => f.LandingAirport.City),
                _ => flights.OrderBy(f => f.DepartureTime)   // default: soonest first
            };

            var totalCount = flights.Count();

            var paged = flights
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(FlightMapper.ToIndexViewModel)
                .ToList();

            var airports = await _airportService.GetAllAirportsAsync();
            ViewBag.Airports = new SelectList(airports, "IataCode", "IataCode");
            ViewBag.FromIata = fromIata;
            ViewBag.ToIata = toIata;
            ViewBag.Date = date?.ToString("yyyy-MM-dd");
            ViewBag.SortBy = sortBy;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(paged);
        }

        // GET: Flights/Details/FB101
        [HttpGet]
        public async Task<IActionResult> Details(string id, int passengerPage = 1)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var flight = await _flightService.GetFlightByNumberAsync(id);
            if (flight is null) return NotFound();

            var allPassengers = flight.Reservations
                .SelectMany(r => r.Passengers.Select(p => FlightMapper.ToPassengerViewModel(p, r)))
                .ToList();

            const int pageSize = 10;
            var paginatedPassengers = allPassengers
                .Skip((passengerPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var vm = FlightMapper.ToDetailsViewModel(flight);
            vm.Passengers = paginatedPassengers;
            vm.PassengerPage = passengerPage;
            vm.PassengerPageSize = pageSize;
            vm.TotalPassengers = allPassengers.Count;

            return View(vm);
        }

        // CREATE (GET) — Admin only
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View(new FlightCreateViewModel());
        }

        // CREATE (POST) — Admin only
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(FlightCreateViewModel viewModel)
        {
            if (viewModel.DepartureAirportIataCode == viewModel.LandingAirportIataCode)
                ModelState.AddModelError(nameof(viewModel.LandingAirportIataCode),
                    "Летището на кацане не може да съвпада с летището на излитане.");

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return View(viewModel);
            }

            if (await _flightService.FlightExistsAsync(viewModel.FlightNumber))
            {
                ModelState.AddModelError(nameof(viewModel.FlightNumber),
                    $"Полет с номер '{viewModel.FlightNumber}' вече съществува.");
                await PopulateDropdownsAsync();
                return View(viewModel);
            }

            var flight = FlightMapper.ToFlight(viewModel);
            await _flightService.CreateFlightAsync(flight);

            TempData["Success"] = $"Полет {flight.FlightNumber} беше създаден успешно.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET) — Admin only
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var flight = await _flightService.GetFlightByNumberAsync(id);
            if (flight is null) return NotFound();

            await PopulateDropdownsAsync();
            return View(FlightMapper.ToEditViewModel(flight));
        }

        // EDIT (POST) — Admin only
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id, FlightEditViewModel viewModel)
        {
            if (id != viewModel.FlightNumber) return BadRequest();

            if (viewModel.DepartureAirportIataCode == viewModel.LandingAirportIataCode)
                ModelState.AddModelError(nameof(viewModel.LandingAirportIataCode),
                    "Летището на кацане не може да съвпада с летището на излитане.");

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return View(viewModel);
            }

            try
            {
                await _flightService.UpdateFlightAsync(FlightMapper.ToFlight(viewModel));
                TempData["Success"] = $"Полет {id} беше обновен успешно.";
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET) — Admin only
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            var flight = await _flightService.GetFlightByNumberAsync(id);
            if (flight is null) return NotFound();

            return View(FlightMapper.ToDeleteViewModel(flight));
        }

        // DELETE (POST) — Admin only
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _flightService.DeleteFlightAsync(id);
                TempData["Success"] = $"Полет {id} беше изтрит успешно.";
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync(
            string? selectedAirport = null,
            string? selectedAirplane = null)
        {
            var airports = await _airportService.GetAllAirportsAsync();
            var airplanes = await _airplaneService.GetAllAirplanesAsync();

            ViewBag.Airports = new SelectList(airports, "IataCode", "IataCode", selectedAirport);
            ViewBag.Airplanes = new SelectList(airplanes, "Id", "Model", selectedAirplane);
        }
    }
}