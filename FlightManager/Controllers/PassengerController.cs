using FlightManager.Data;
using FlightManager.Data.Models;
using FlightManager.Services.Services.Interfaces;
using FlightManager.Web.Mappers;
using FlightManager.Web.ViewModels.Passengers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Web.Controllers
{
    public class PassengerController : Controller
    {
        private readonly IPassengerService _passengerService;
        private readonly IReservationService _reservationService;

        public PassengerController(IPassengerService passengerService, IReservationService reservationService)
        {
            _passengerService = passengerService;
            _reservationService = reservationService;
        }

        // GET: Passengers
        public async Task<IActionResult> Index()
        {
            var passengers = await _passengerService.GetAllPassengersAsync();
            var viewModels = PassengerMapper.ToIndexViewModelList(passengers);
            return View(viewModels);
        }

        // GET: Passengers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var passenger = await _passengerService.GetPassengerByIdAsync(id.Value);

            if (passenger == null)
                return NotFound();

            var viewModel = PassengerMapper.ToDetailsViewModel(passenger);
            return View(viewModel);
        }

        // GET: Passengers/Create
        public async Task<IActionResult> Create()
        {
            await PopulateReservationsDropDownAsync();
            return View();
        }

        // POST: Passengers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PassengerCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var passenger = PassengerMapper.ToModel(viewModel);
                await _passengerService.CreatePassengerAsync(passenger);
                return RedirectToAction(nameof(Index));
            }

            await PopulateReservationsDropDownAsync(viewModel.ReservationId);
            return View(viewModel);
        }

        // GET: Passengers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var passenger = await _passengerService.GetPassengerByIdAsync(id.Value);

            if (passenger == null)
                return NotFound();

            var viewModel = PassengerMapper.ToEditViewModel(passenger);
            await PopulateReservationsDropDownAsync(viewModel.ReservationId);
            return View(viewModel);
        }

        // POST: Passengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PassengerEditViewModel viewModel)
        {
            if (id != viewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var passenger = PassengerMapper.ToModel(viewModel);
                    await _passengerService.UpdatePassengerAsync(passenger);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Index));
            }

            await PopulateReservationsDropDownAsync(viewModel.ReservationId);
            return View(viewModel);
        }

        // GET: Passengers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var passenger = await _passengerService.GetPassengerByIdAsync(id.Value);

            if (passenger == null)
                return NotFound();

            var viewModel = PassengerMapper.ToDeleteViewModel(passenger);
            return View(viewModel);
        }

        // POST: Passengers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _passengerService.DeletePassengerAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateReservationsDropDownAsync(int? selectedId = null)
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            ViewData["ReservationId"] = new SelectList(reservations, "Id", "Id", selectedId);
        }
    }
}