using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FlightManager.Data.Validation;
using System.Text;

namespace FlightManager.Data.Models
{
    internal class Reservation
    {
        public enum TicketType
        {
            Economy,
            Business
        }

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

        public class Passenger
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Собственото име е задължително.")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "Собственото име трябва да е между 2 и 50 символа.")]
            [Display(Name = "Собствено име на пътника")]
            public string FirstName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Бащиното име е задължително.")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "Бащиното име трябва да е между 2 и 50 символа.")]
            [Display(Name = "Бащино име на пътника")]
            public string MiddleName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Фамилното име е задължително.")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилното име трябва да е между 2 и 50 символа.")]
            [Display(Name = "Фамилното име на пътника")]
            public string LastName { get; set; } = string.Empty;

            [Required(ErrorMessage = "ЕГН-то е задължително.")]
            [EgnValidation]
            [Display(Name = "ЕГН")]
            public string EGN { get; set; } = string.Empty;

            [Required(ErrorMessage = "Телефонният номер е задължителен.")]
            [Phone(ErrorMessage = "Невалиден телефонен номер.")]
            [StringLength(20)]
            [Display(Name = "Телефон")]
            public string PhoneNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "Националността на пътника е задължителна.")]
            [StringLength(60, MinimumLength = 2, ErrorMessage = "Националността на пътника трябва да е между 2 и 60 символа.")]
            [Display(Name = "Националността на пътника")]
            public string Nationality { get; set; } = string.Empty;

            [Required(ErrorMessage = "Типът билет е задължителен.")]
            [Display(Name = "Тип на билета")]
            public TicketType TicketType { get; set; }
            public int ReservationId { get; set; }
            public Reservation? Reservation { get; set; }
        }
    }
}
