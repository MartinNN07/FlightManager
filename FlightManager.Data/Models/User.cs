using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string LastName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "EGN must be exactly 10 digits.")]
        public required string EGN { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public required string Address { get; set; }
    }
}
