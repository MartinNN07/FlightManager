using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Data.Models
{
    public class Airplane
    {
        [Key]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Идентификаторът на самолета трябва да е между 2 и 20 символа.")]
        [Display(Name = "Идентификационен номер на самолета")]
        public string Id { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Моделът на самолета трябва да е между 2 и 50 символа.")]
        [Display(Name = "Модел на самолета")]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Капацитетът на икономичната класа трябва да бъде положително число.")]
        [Display(Name = "Капацитет на икономичната класа")]
        public int EconomyClassSeats { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Капацитетът на бизнес класата трябва да бъде неотрицателно число.")]
        [Display(Name = "Капацитет на бизнес класата")]
        public int BusinessClassSeats { get; set; }
    }
}
