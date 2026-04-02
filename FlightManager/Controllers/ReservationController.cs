using FlightManager.Data.Models;
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
        private readonly IEmailService _emailService;

        public ReservationsController(
            IReservationService reservationService,
            IFlightService flightService,
            IPassengerService passengerService,
            IEmailService emailService)
        {
            _reservationService = reservationService;
            _flightService = flightService;
            _passengerService = passengerService;
            _emailService = emailService;
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

        // GET: Reservations/Create?flightId=FB101
        public async Task<IActionResult> Create(string? flightId)
        {
            var viewModel = new ReservationCreateViewModel();

            if (!string.IsNullOrWhiteSpace(flightId))
            {
                var flight = await _flightService.GetFlightByNumberAsync(flightId);
                if (flight != null)
                {
                    viewModel.FlightId = flight.FlightNumber;
                    viewModel.DepartureCity = flight.DepartureAirport.City;
                    viewModel.ArrivalCity = flight.LandingAirport.City;
                    viewModel.FlightDate = flight.DepartureTime;
                    viewModel.AvailableEconomySeats = flight.AvailableEconomySeats;
                    viewModel.AvailableBusinessSeats = flight.AvailableBusinessSeats;
                }
            }

            await PopulateFlightsDropDownAsync(viewModel.FlightId);
            return View(viewModel);
        }

        // POST: Reservations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationCreateViewModel viewModel)
        {
            var flight = await _flightService.GetFlightByNumberAsync(viewModel.FlightId);

            if (flight == null)
            {
                ModelState.AddModelError(nameof(viewModel.FlightId), "Полетът не е намерен.");
            }
            else
            {
                viewModel.DepartureCity = flight.DepartureAirport.City;
                viewModel.ArrivalCity = flight.LandingAirport.City;
                viewModel.FlightDate = flight.DepartureTime;
                viewModel.AvailableEconomySeats = flight.AvailableEconomySeats;
                viewModel.AvailableBusinessSeats = flight.AvailableBusinessSeats;

                int requestedSeats = viewModel.Passengers.Count;

                if (viewModel.SeatClass == FlightManager.Data.Models.SeatingClass.Economy
                    && requestedSeats > flight.AvailableEconomySeats)
                {
                    ModelState.AddModelError(nameof(viewModel.SeatClass),
                        $"Недостатъчно места в икономична класа. Свободни: {flight.AvailableEconomySeats}.");
                }
                else if (viewModel.SeatClass == FlightManager.Data.Models.SeatingClass.Business
                    && requestedSeats > flight.AvailableBusinessSeats)
                {
                    ModelState.AddModelError(nameof(viewModel.SeatClass),
                        $"Недостатъчно места в бизнес класа. Свободни: {flight.AvailableBusinessSeats}.");
                }
            }

            if (ModelState.IsValid)
            {
                var reservation = ReservationMapper.ToModel(viewModel);

                var passengers = new List<Passenger>();
                var passengerNames = new List<string>();

                foreach (var passengerInput in viewModel.Passengers)
                {
                    var existingPassenger = await _passengerService.GetPassengerByEGNAsync(passengerInput.EGN);

                    Passenger passenger;

                    if (existingPassenger == null)
                    {
                        passenger = new Passenger
                        {
                            FirstName = passengerInput.FirstName,
                            MiddleName = passengerInput.MiddleName,
                            LastName = passengerInput.LastName,
                            EGN = passengerInput.EGN,
                            PhoneNumber = passengerInput.PhoneNumber,
                            Nationality = passengerInput.Nationality
                        };

                        await _passengerService.CreatePassengerAsync(passenger);
                    }
                    else
                    {
                        passenger = existingPassenger;
                    }

                    passengers.Add(passenger);

                    passengerNames.Add($"{passenger.FirstName} {passenger.MiddleName} {passenger.LastName}");
                }

                reservation.Passengers = passengers;

                await _reservationService.CreateReservationAsync(reservation);

                try
                {
                    string seatClassLabel = viewModel.SeatClass == SeatingClass.Business
                        ? "Бизнес" : "Икономична";

                    /*await _emailService.SendReservationConfirmationAsync(
                        toEmail: viewModel.ContactEmail,
                        flightNumber: viewModel.FlightId,
                        departureCity: viewModel.DepartureCity ?? string.Empty,
                        arrivalCity: viewModel.ArrivalCity ?? string.Empty,
                        departureTime: viewModel.FlightDate ?? DateTime.UtcNow,
                        seatClass: seatClassLabel,
                        passengerNames: passengerNames);*/
                }
                catch
                {
                    // optional: log error
                }

                TempData["Success"] = "Резервацията беше създадена успешно! Изпратено е потвърждение на имейл.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateFlightsDropDownAsync(viewModel.FlightId);
            return View(viewModel);
        }

        private async Task PopulateFlightsDropDownAsync(string? selectedId = null)
        {
            var flights = await _flightService.GetAllFlightsAsync();

            ViewData["FlightId"] = new SelectList(flights, "FlightNumber", "FlightNumber", selectedId);

            var flightsJson = flights.ToDictionary(
                f => f.FlightNumber,
                f => new
                {
                    departureCity = f.DepartureAirport.City,
                    arrivalCity = f.LandingAirport.City,
                    flightDate = f.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    availableEconomySeats = f.AvailableEconomySeats,
                    availableBusinessSeats = f.AvailableBusinessSeats
                });

            ViewBag.FlightsJson = JsonSerializer.Serialize(flightsJson);
        }
    }
}
