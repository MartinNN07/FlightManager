using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using FlightManager.Web.Mappers;
using FlightManager.Web.ViewModels.Airports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AirportsController : Controller
    {
        private readonly IAirportService _airportService;

        public AirportsController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        // GET: Airports
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Airport> airports = await _airportService.GetAllAirportsAsync();
            IEnumerable<AirportIndexViewModel> viewModels = AirportMapper.ToIndexViewModelList(airports);
            return View(viewModels);
        }

        // GET: Airports/Details/SOF
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            Airport? airport = await _airportService.GetAirportByIataCodeAsync(id);
            if (airport is null) return NotFound();

            return View(AirportMapper.ToDetailsViewModel(airport));
        }

        // GET: Airports/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new AirportCreateViewModel());
        }

        // POST: Airports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AirportCreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (await _airportService.AirportExistsAsync(viewModel.IataCode))
            {
                ModelState.AddModelError(nameof(viewModel.IataCode),
                    $"Летище с IATA код '{viewModel.IataCode}' вече съществува.");
                return View(viewModel);
            }

            await _airportService.CreateAirportAsync(AirportMapper.ToModel(viewModel));

            TempData["Success"] = $"Летище '{viewModel.AirportName}' ({viewModel.IataCode}) беше създадено успешно.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Airports/Edit/SOF
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            Airport? airport = await _airportService.GetAirportByIataCodeAsync(id);
            if (airport is null) return NotFound();

            return View(AirportMapper.ToEditViewModel(airport));
        }

        // POST: Airports/Edit/SOF
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, AirportEditViewModel viewModel)
        {
            if (id != viewModel.IataCode) return BadRequest();
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                await _airportService.UpdateAirportAsync(AirportMapper.ToModel(viewModel));
                TempData["Success"] = $"Летище '{viewModel.AirportName}' ({viewModel.IataCode}) беше обновено успешно.";
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}