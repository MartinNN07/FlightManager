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

        [HttpGet]
        public async Task<IActionResult> Index(
            string? departureTerm,
            string? arrivalTerm,
            DateTime? date,
            string? sortBy,
            int page = 1,
            int pageSize = 10)
        {
            var flights = (string.IsNullOrWhiteSpace(departureTerm) && string.IsNullOrWhiteSpace(arrivalTerm))
                ? await _flightService.GetAllFlightsAsync()
                : await _flightService.GetFlightsBySearchTermsAsync(departureTerm, arrivalTerm);

            if (date.HasValue)
                flights = flights.Where(f => f.DepartureTime.Date == date.Value.Date);

            flights = sortBy switch
            {
                "departure_desc" => flights.OrderByDescending(f => f.DepartureTime),
                "from" => flights.OrderBy(f => f.DepartureAirport.City),
                "to" => flights.OrderBy(f => f.LandingAirport.City),
                _ => flights.OrderBy(f => f.DepartureTime)
            };

            var totalCount = flights.Count();

            var paged = flights
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(FlightMapper.ToIndexViewModel)
                .ToList();

            ViewBag.Date = date?.ToString("yyyy-MM-dd");
            ViewBag.SortBy = sortBy;
            ViewBag.DepartureTerm = departureTerm;
            ViewBag.ArrivalTerm = arrivalTerm;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalCount = totalCount;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            return View(paged);
        }

        // GET: Flights/SearchFlights?departureTerm=AB&arrivalTerm=CD&date=...
        [HttpGet]
        public async Task<IActionResult> SearchFlights(
            string? departureTerm,
            string? arrivalTerm,
            DateTime? date,
            string? sortBy,
            int page = 1,
            int pageSize = 10)
        {
            var flights = (string.IsNullOrWhiteSpace(departureTerm) && string.IsNullOrWhiteSpace(arrivalTerm))
                ? await _flightService.GetAllFlightsAsync()
                : await _flightService.GetFlightsBySearchTermsAsync(departureTerm, arrivalTerm);

            if (date.HasValue)
                flights = flights.Where(f => f.DepartureTime.Date == date.Value.Date);

            flights = sortBy switch
            {
                "departure_desc" => flights.OrderByDescending(f => f.DepartureTime),
                "from" => flights.OrderBy(f => f.DepartureAirport.City),
                "to" => flights.OrderBy(f => f.LandingAirport.City),
                _ => flights.OrderBy(f => f.DepartureTime)
            };

            var totalCount = flights.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var paged = flights
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(FlightMapper.ToIndexViewModel)
                .ToList();

            return Json(new
            {
                totalCount,
                totalPages,
                currentPage = page,
                flights = paged.Select(f => new
                {
                    f.FlightNumber,
                    f.DepartureIata,
                    f.DepartureCity,
                    f.LandingIata,
                    f.LandingCity,
                    DepartureTime = f.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    f.AirplaneModel,
                    f.AvailableEconomySeats,
                    f.AvailableBusinessSeats
                })
            });
        }

        // GET: Flights/SearchSuggestions?term=AB
        [HttpGet]
        public async Task<IActionResult> SearchSuggestions(string term)
        {
            if (string.IsNullOrWhiteSpace(term) || term.Length < 2)
                return Json(Array.Empty<string>());

            // Query using both parameters to find matches anywhere to ensure universal autocomplete functionality
            var departureMatches = await _flightService.GetFlightsBySearchTermsAsync(term, null);
            var arrivalMatches = await _flightService.GetFlightsBySearchTermsAsync(null, term);

            var flights = departureMatches.Union(arrivalMatches).ToList();

            var suggestions = flights
                .SelectMany(f => new[]
                {
                    f.FlightNumber,
                    f.DepartureAirportIataCode,
                    f.LandingAirportIataCode,
                    f.DepartureAirport?.City,
                    f.LandingAirport?.City
                })
                .Where(s => s != null && s.Contains(term, StringComparison.OrdinalIgnoreCase))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(s => s)
                .Take(8)
                .ToList();

            return Json(suggestions);
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