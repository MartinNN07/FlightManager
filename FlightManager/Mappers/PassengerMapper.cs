using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.Passengers;

namespace FlightManager.Web.Mappers
{
    public static class PassengerMapper
    {
        public static PassengerIndexViewModel ToIndexViewModel(Passenger passenger)
        {
            return new PassengerIndexViewModel
            {
                Id = passenger.Id,
                FirstName = passenger.FirstName,
                MiddleName = passenger.MiddleName,
                LastName = passenger.LastName,
                EGN = passenger.EGN,
                PhoneNumber = passenger.PhoneNumber,
                Nationality = passenger.Nationality,
                ReservationId = passenger.ReservationId
            };
        }

        public static IEnumerable<PassengerIndexViewModel> ToIndexViewModelList(IEnumerable<Passenger> passengers)
        {
            return passengers.Select(ToIndexViewModel);
        }

        public static PassengerDetailsViewModel ToDetailsViewModel(Passenger passenger)
        {
            return new PassengerDetailsViewModel
            {
                Id = passenger.Id,
                FirstName = passenger.FirstName,
                MiddleName = passenger.MiddleName,
                LastName = passenger.LastName,
                EGN = passenger.EGN,
                PhoneNumber = passenger.PhoneNumber,
                Nationality = passenger.Nationality,
                ReservationId = passenger.ReservationId
            };
        }

        public static PassengerEditViewModel ToEditViewModel(Passenger passenger)
        {
            return new PassengerEditViewModel
            {
                Id = passenger.Id,
                FirstName = passenger.FirstName,
                MiddleName = passenger.MiddleName,
                LastName = passenger.LastName,
                EGN = passenger.EGN,
                PhoneNumber = passenger.PhoneNumber,
                Nationality = passenger.Nationality,
                ReservationId = passenger.ReservationId
            };
        }

        public static PassengerDeleteViewModel ToDeleteViewModel(Passenger passenger)
        {
            return new PassengerDeleteViewModel
            {
                Id = passenger.Id,
                FirstName = passenger.FirstName,
                MiddleName = passenger.MiddleName,
                LastName = passenger.LastName,
                EGN = passenger.EGN,
                PhoneNumber = passenger.PhoneNumber,
                Nationality = passenger.Nationality,
                ReservationId = passenger.ReservationId
            };
        }

        public static Passenger ToModel(PassengerCreateViewModel viewModel)
        {
            return new Passenger
            {
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName,
                LastName = viewModel.LastName,
                EGN = viewModel.EGN,
                PhoneNumber = viewModel.PhoneNumber,
                Nationality = viewModel.Nationality,
                ReservationId = viewModel.ReservationId
            };
        }

        public static Passenger ToModel(PassengerEditViewModel viewModel)
        {
            return new Passenger
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                MiddleName = viewModel.MiddleName,
                LastName = viewModel.LastName,
                EGN = viewModel.EGN,
                PhoneNumber = viewModel.PhoneNumber,
                Nationality = viewModel.Nationality,
                ReservationId = viewModel.ReservationId
            };
        }
    }
}