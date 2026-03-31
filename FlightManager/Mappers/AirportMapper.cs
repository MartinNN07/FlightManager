using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.Airports;

namespace FlightManager.Web.Mappers
{
    public static class AirportMapper
    {
        public static AirportIndexViewModel ToIndexViewModel(Airport airport)
        {
            return new AirportIndexViewModel
            {
                IataCode = airport.IataCode,
                Country = airport.Country,
                City = airport.City,
                AirportName = airport.AirportName,
            };
        }

        public static IEnumerable<AirportIndexViewModel> ToIndexViewModelList(IEnumerable<Airport> airports)
        {
            return airports.Select(ToIndexViewModel);
        }

        public static AirportDetailsViewModel ToDetailsViewModel(Airport airport)
        {
            return new AirportDetailsViewModel
            {
                IataCode = airport.IataCode,
                Country = airport.Country,
                City = airport.City,
                AirportName = airport.AirportName,
            };
        }

        public static AirportEditViewModel ToEditViewModel(Airport airport)
        {
            return new AirportEditViewModel
            {
                IataCode = airport.IataCode,
                Country = airport.Country,
                City = airport.City,
                AirportName = airport.AirportName
            };
        }

        public static AirportDeleteViewModel ToDeleteViewModel(Airport airport)
        {
            return new AirportDeleteViewModel
            {
                IataCode = airport.IataCode,
                Country = airport.Country,
                City = airport.City,
                AirportName = airport.AirportName,
                DepartingFlightsCount = airport.DepartingFlights?.Count ?? 0,
                ArrivingFlightsCount = airport.ArrivingFlights?.Count ?? 0
            };
        }

        public static Airport ToModel(AirportCreateViewModel viewModel)
        {
            return new Airport
            {
                IataCode = viewModel.IataCode,
                Country = viewModel.Country,
                City = viewModel.City,
                AirportName = viewModel.AirportName
            };
        }

        public static Airport ToModel(AirportEditViewModel viewModel)
        {
            return new Airport
            {
                IataCode = viewModel.IataCode,
                Country = viewModel.Country,
                City = viewModel.City,
                AirportName = viewModel.AirportName
            };
        }
    }
}