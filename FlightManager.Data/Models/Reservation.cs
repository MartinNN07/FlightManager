using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        [Display(Name = "Имейл за контакт")]
        public string ContactEmail { get; set; } = string.Empty;

        [Display(Name = "Дата на резервация")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int FlightId { get; set; }
        public Flight? Flight { get; set; }
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
