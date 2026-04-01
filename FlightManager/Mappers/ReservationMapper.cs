using FlightManager.Data.Models;
using FlightManager.Web.ViewModels.Reservations;

namespace FlightManager.Web.Mappers
{
    public class ReservationMapper
    {
        public static ReservationIndexViewModel ToIndexViewModel(Reservation reservation)
        {
            return new ReservationIndexViewModel
            {
                Id = reservation.Id,
                ContactEmail = reservation.ContactEmail,
                CreatedAt = reservation.CreatedAt,
                FlightId = reservation.FlightId,
                SeatClass = reservation.SeatClass,
                PassengerCount = reservation.Passengers?.Count ?? 0
            };
        }

        public static IEnumerable<ReservationIndexViewModel> ToIndexViewModelList(IEnumerable<Reservation> reservations)
        {
            return reservations.Select(ToIndexViewModel);
        }

        public static ReservationDetailsViewModel ToDetailsViewModel(Reservation reservation)
        {
            return new ReservationDetailsViewModel
            {
                Id = reservation.Id,
                ContactEmail = reservation.ContactEmail,
                CreatedAt = reservation.CreatedAt,
                FlightId = reservation.FlightId,
                SeatClass = reservation.SeatClass,
                PassengerNames = reservation.Passengers?
                    .Select(p => $"{p.FirstName} {p.MiddleName} {p.LastName}")
                    .ToList() ?? new List<string>()
            };
        }

        public static Reservation ToModel(ReservationCreateViewModel viewModel)
        {
            return new Reservation
            {
                ContactEmail = viewModel.ContactEmail,
                FlightId = viewModel.FlightId,
                SeatClass = viewModel.SeatClass
            };
        }
    }
}
