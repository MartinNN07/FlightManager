using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.Flights;

namespace FlightManager.Web.Mappers
{
    public static class FlightMapper
    {
        public static FlightIndexViewModel ToIndexViewModel(Flight f) => new()
        {
            FlightNumber = f.FlightNumber,
            DepartureCity = f.DepartureAirport.City,
            DepartureIata = f.DepartureAirportIataCode,
            LandingCity = f.LandingAirport.City,
            LandingIata = f.LandingAirportIataCode,
            DepartureTime = f.DepartureTime,
            LandingTime = f.LandingTime,
            PilotName = f.PilotName,
            AirplaneModel = f.Airplane.Model,
            AvailableEconomySeats = f.AvailableEconomySeats,
            AvailableBusinessSeats = f.AvailableBusinessSeats
        };

        public static FlightDetailsViewModel ToDetailsViewModel(Flight flight) => new()
        {
            FlightNumber = flight.FlightNumber,
            DepartureAirportName = flight.DepartureAirport.AirportName,
            DepartureCity = flight.DepartureAirport.City,
            DepartureIata = flight.DepartureAirportIataCode,
            LandingAirportName = flight.LandingAirport.AirportName,
            LandingCity = flight.LandingAirport.City,
            LandingIata = flight.LandingAirportIataCode,
            DepartureTime = flight.DepartureTime,
            FlightDuration = flight.LandingTime - flight.DepartureTime,
            PilotName = flight.PilotName,
            AirplaneModel = flight.Airplane.Model,
            AirplaneId = flight.AirplaneId,
            TotalEconomySeats = flight.Airplane.EconomyClassSeats,
            TotalBusinessSeats = flight.Airplane.BusinessClassSeats,
            AvailableEconomySeats = flight.AvailableEconomySeats,
            AvailableBusinessSeats = flight.AvailableBusinessSeats
        };
        public static FlightEditViewModel ToEditViewModel(Flight flight) => new()
        {
            FlightNumber = flight.FlightNumber,
            DepartureAirportIataCode = flight.DepartureAirportIataCode,
            LandingAirportIataCode = flight.LandingAirportIataCode,
            AirplaneId = flight.AirplaneId,
            PilotName = flight.PilotName,
            DepartureTime = flight.DepartureTime,
            LandingTime = flight.LandingTime
        };

        public static FlightDeleteViewModel ToDeleteViewModel(Flight flight) => new()
        {
            FlightNumber = flight.FlightNumber,
            DepartureCity = flight.DepartureAirport.City,
            LandingCity = flight.LandingAirport.City,
            DepartureTime = flight.DepartureTime,
            AirplaneModel = flight.Airplane.Model,
            ReservationCount = flight.Reservations.Count
        };

        public static FlightPassengerViewModel ToPassengerViewModel(
            Passenger p, Reservation r) => new()
            {
                FullName = $"{p.FirstName} {p.MiddleName} {p.LastName}",
                Nationality = p.Nationality,
                SeatClass = r.SeatClass == SeatingClass.Business ? "Бизнес" : "Икономична",
                ContactEmail = r.ContactEmail
            };

        public static Flight ToFlight(FlightCreateViewModel vm) => new()
        {
            FlightNumber = vm.FlightNumber,
            DepartureAirportIataCode = vm.DepartureAirportIataCode,
            LandingAirportIataCode = vm.LandingAirportIataCode,
            AirplaneId = vm.AirplaneId,
            PilotName = vm.PilotName,
            DepartureTime = vm.DepartureTime.ToUniversalTime(),
            LandingTime = vm.LandingTime.ToUniversalTime()
        };

        public static Flight ToFlight(FlightEditViewModel vm) => new()
        {
            FlightNumber = vm.FlightNumber,
            DepartureAirportIataCode = vm.DepartureAirportIataCode,
            LandingAirportIataCode = vm.LandingAirportIataCode,
            AirplaneId = vm.AirplaneId,
            PilotName = vm.PilotName,
            DepartureTime = vm.DepartureTime.ToUniversalTime(),
            LandingTime = vm.LandingTime.ToUniversalTime()
        };
    }
}