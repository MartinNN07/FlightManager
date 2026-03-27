using System.ComponentModel.DataAnnotations;

namespace FlightManager.Web.ViewModels.Passengers
{
    public class PassengerDeleteViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Собствено ime")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Бащино ime")]
        public string MiddleName { get; set; } = string.Empty;

        [Display(Name = "Фамилно ime")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "ЕГН")]
        public string EGN { get; set; } = string.Empty;

        [Display(Name = "Телефонен номер")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Националност")]
        public string Nationality { get; set; } = string.Empty;

        [Display(Name = "Резервация №")]
        public int ReservationId { get; set; }
    }
}
