using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Validation
{
    internal class EgnValidationAttribute : ValidationAttribute
    {
        public EgnValidationAttribute()
        {
            ErrorMessage = "ЕГН-то трябва да съдържа точно 10 цифри с валидна контролна сума.";
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string egn)
                return new ValidationResult(ErrorMessage);
            if (egn.Length != 10 || !egn.All(char.IsDigit))
                return new ValidationResult(ErrorMessage);
            
            int[] weights = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += (egn[i] - '0') * weights[i];
            }

            int controlDigit = sum % 11;
            if (controlDigit == 10)
            {
                controlDigit = 0;
            }

            if (controlDigit != (egn[9] - '0'))
            {
                return new ValidationResult(ErrorMessage);
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}