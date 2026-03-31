using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightManager.Data.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LandingAfterDepartureAttribute : ValidationAttribute
    {
        private readonly string _departurePropertyName;

        public LandingAfterDepartureAttribute(string departurePropertyName)
        {
            _departurePropertyName = departurePropertyName;
            ErrorMessage = "Датата на кацане трябва да бъде след датата на излитане.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not DateTime landingTime)
                return ValidationResult.Success;

            var departureProperty = validationContext.ObjectType.GetProperty(_departurePropertyName);
            if (departureProperty == null)
                return new ValidationResult($"Неизвестно свойство: {_departurePropertyName}");

            var departureValue = departureProperty.GetValue(validationContext.ObjectInstance);
            if (departureValue is not DateTime departureTime)
                return ValidationResult.Success;

            if (landingTime.ToUniversalTime() <= departureTime.ToUniversalTime())
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;

        }
    }
}
