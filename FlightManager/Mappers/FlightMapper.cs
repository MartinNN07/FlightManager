using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.Flights;

namespace FlightManager.Web.Mappers
{
    public static class FlightMapper
    {
        public static FlightIndexViewModel ToIndexViewModel(Flight flight)
        {
            return new FlightIndexViewModel
            {
                FlightNumber = flight.FlightNumber,
                DepartureCity = flight.DepartureAirport.City,
                DepartureIata = flight.DepartureAirportIataCode,
                LandingCity = flight.LandingAirport.City,
                LandingIata = flight.LandingAirportIataCode,
                DepartureTime = flight.DepartureTime,
                LandingTime = flight.LandingTime,
                PilotName = flight.PilotName,
                AirplaneModel = flight.Airplane.Model,
                AvailableEconomySeats = flight.AvailableEconomySeats,
                AvailableBusinessSeats = flight.AvailableBusinessSeats
            };
        }

        public static FlightDetailsViewModel ToDetailsViewModel(Flight flight)
        {
            return new FlightDetailsViewModel
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
        }

        public static FlightPassengerViewModel ToPassengerViewModel(Passenger passenger, Reservation reservation)
        {
            return new FlightPassengerViewModel
            {
                FullName = $"{passenger.FirstName} {passenger.MiddleName} {passenger.LastName}",
                Nationality = passenger.Nationality,
                SeatClass = reservation.SeatClass == SeatingClass.Business ? "Бизнес" : "Икономична",
                ContactEmail = reservation.ContactEmail
            };
        }

        public static Flight ToFlight(FlightCreateViewModel vm)
        {
            return new Flight
            {
                FlightNumber = vm.FlightNumber,
                DepartureAirportIataCode = vm.DepartureAirportIataCode,
                LandingAirportIataCode = vm.LandingAirportIataCode,
                AirplaneId = vm.AirplaneId,
                PilotName = vm.PilotName,
                DepartureTime = vm.DepartureTime,
                LandingTime = vm.LandingTime
            };
        }

        public static Flight ToFlight(FlightEditViewModel vm)
        {
            return new Flight
            {
                FlightNumber = vm.FlightNumber,
                DepartureAirportIataCode = vm.DepartureAirportIataCode,
                LandingAirportIataCode = vm.LandingAirportIataCode,
                AirplaneId = vm.AirplaneId,
                PilotName = vm.PilotName,
                DepartureTime = vm.DepartureTime,
                LandingTime = vm.LandingTime
            };
        }

        public static FlightEditViewModel ToEditViewModel(Flight flight)
        {
            return new FlightEditViewModel
            {
                FlightNumber = flight.FlightNumber,
                DepartureAirportIataCode = flight.DepartureAirportIataCode,
                LandingAirportIataCode = flight.LandingAirportIataCode,
                AirplaneId = flight.AirplaneId,
                PilotName = flight.PilotName,
                DepartureTime = flight.DepartureTime,
                LandingTime = flight.LandingTime
            };
        }
    }
}
