using System.ComponentModel.DataAnnotations;

namespace FlightManager.Data.Validation
{
    public class EgnValidationAttribute : ValidationAttribute
    {
        public EgnValidationAttribute()
        {
            ErrorMessage = "ЕГН-то трябва да съдържа точно 10 цифри с валидна контролна сума.";
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value is not string egn)
                return new ValidationResult(ErrorMessage);

            if (egn.Length != 10 || !egn.All(char.IsDigit))
                return new ValidationResult(ErrorMessage);

            int[] weights = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (egn[i] - '0') * weights[i];

            int checkDigit = sum % 11;
            if (checkDigit == 10) checkDigit = 0;

            if (checkDigit != (egn[9] - '0'))
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}