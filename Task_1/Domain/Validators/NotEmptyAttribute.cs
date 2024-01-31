using System.ComponentModel.DataAnnotations;

namespace Task_1.Domain.validators
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string model = value as string;

            if (model is null)
            {
                return new ValidationResult("Не удалось получить поле");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                var errorMessage = $"Поле состоит из пустых символов ({validationContext.DisplayName})";

                return new ValidationResult(errorMessage, new List<string>() { nameof(model) });
            }
            
            return ValidationResult.Success;
        }
    }
}