using FlightManager.Data.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightManager.Data.Models
{
    public enum SeatingClass
    {
        Economy,
        Business
    }
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        [Display(Name = "Имейл за контакт")]
        public string ContactEmail { get; set; } = string.Empty;

        [Display(Name = "Дата на резервация")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey(nameof(Flight))]
        public string FlightId { get; set; } = string.Empty;
        public Flight? Flight { get; set; }

        [Required]
        public SeatingClass SeatClass { get; set; } = SeatingClass.Economy;
        public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
