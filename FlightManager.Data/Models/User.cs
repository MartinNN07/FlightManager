using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Data.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Първото име е задължително.")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ЕГН-то трябва да бъде точно 10 цифри.")]
        public string EGN { get; set; } = string.Empty;

        [StringLength(200, MinimumLength = 5)]
        public string? Address { get; set; } = string.Empty;
    }
}
