using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using FlightManager.Web.Mappers;
using FlightManager.Web.ViewModels.Airplane;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Web.Controllers
{
    public class AirplanesController : Controller
    {
        private readonly IAirplaneService _airplaneService;

        public AirplanesController(IAirplaneService airplaneService)
        {
            _airplaneService = airplaneService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Airplane> airplanes = await _airplaneService.GetAllAirplanesAsync();
            IEnumerable<AirplaneIndexViewModel> viewModels = AirplaneMapper.ToIndexViewModelList(airplanes);
            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            Airplane? airplane = await _airplaneService.GetAirplaneByIdAsync(id);
            if (airplane is null) return NotFound();

            return View(AirplaneMapper.ToDetailsViewModel(airplane));
        }

        // CREATE (GET)
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View(new AirplaneCreateViewModel());
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(AirplaneCreateViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (await _airplaneService.AirplaneExistsAsync(viewModel.Id))
            {
                ModelState.AddModelError(nameof(viewModel.Id),
                    $"Самолет с идентификатор '{viewModel.Id}' вече съществува.");
                return View(viewModel);
            }

            await _airplaneService.CreateAirplaneAsync(AirplaneMapper.ToModel(viewModel));

            TempData["Success"] = $"Самолет '{viewModel.Model}' беше създаден успешно.";
            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            Airplane? airplane = await _airplaneService.GetAirplaneByIdAsync(id);
            if (airplane is null) return NotFound();

            return View(AirplaneMapper.ToEditViewModel(airplane));
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(string id, AirplaneEditViewModel viewModel)
        {
            if (id != viewModel.Id) return BadRequest();
            if (!ModelState.IsValid) return View(viewModel);

            try
            {
                await _airplaneService.UpdateAirplaneAsync(AirplaneMapper.ToModel(viewModel));
                TempData["Success"] = $"Самолет '{viewModel.Model}' беше обновен успешно.";
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();

            Airplane? airplane = await _airplaneService.GetAirplaneByIdAsync(id);
            if (airplane is null) return NotFound();

            return View(AirplaneMapper.ToDeleteViewModel(airplane));
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _airplaneService.DeleteAirplaneAsync(id);
                TempData["Success"] = "Самолетът беше изтрит успешно.";
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}