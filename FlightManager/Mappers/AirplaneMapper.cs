using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.Airplane;

namespace FlightManager.Web.Mappers
{
    public static class AirplaneMapper
    {

        public static AirplaneIndexViewModel ToIndexViewModel(Airplane airplane)
        {
            return new AirplaneIndexViewModel
            {
                Id = airplane.Id,
                Model = airplane.Model,
                EconomyClassSeats = airplane.EconomyClassSeats,
                BusinessClassSeats = airplane.BusinessClassSeats
            };
        }

        public static IEnumerable<AirplaneIndexViewModel> ToIndexViewModelList(IEnumerable<Airplane> airplanes)
        {
            return airplanes.Select(ToIndexViewModel);
        }

        public static AirplaneDetailsViewModel ToDetailsViewModel(Airplane airplane)
        {
            return new AirplaneDetailsViewModel
            {
                Id = airplane.Id,
                Model = airplane.Model,
                EconomyClassSeats = airplane.EconomyClassSeats,
                BusinessClassSeats = airplane.BusinessClassSeats
            };
        }

        public static AirplaneEditViewModel ToEditViewModel(Airplane airplane)
        {
            return new AirplaneEditViewModel
            {
                Id = airplane.Id,
                Model = airplane.Model,
                EconomyClassSeats = airplane.EconomyClassSeats,
                BusinessClassSeats = airplane.BusinessClassSeats
            };
        }
        public static Airplane ToModel(AirplaneCreateViewModel viewModel)
        {
            return new Airplane
            {
                Id = viewModel.Id,
                Model = viewModel.Model,
                EconomyClassSeats = viewModel.EconomyClassSeats,
                BusinessClassSeats = viewModel.BusinessClassSeats
            };
        }
        public static Airplane ToModel(AirplaneEditViewModel viewModel)
        {
            return new Airplane
            {
                Id = viewModel.Id,
                Model = viewModel.Model,
                EconomyClassSeats = viewModel.EconomyClassSeats,
                BusinessClassSeats = viewModel.BusinessClassSeats
            };
        }
    }
}