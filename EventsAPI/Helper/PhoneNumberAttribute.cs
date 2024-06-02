using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace EventsAPI.Helper
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        public PhoneNumberAttribute()
        {
            ErrorMessage = "The telephone number must be in the format (+55 31 12345-6789).";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string phoneNumber = value as string;

            if (string.IsNullOrEmpty(phoneNumber))
            {
                return ValidationResult.Success;
            }

            // Regex para validar e extrair os componentes do número de telefone
            var regex = new Regex(@"^\+(\d{2}) (\d{2}) (\d{4,5})-(\d{4})$");
            if (!regex.IsMatch(phoneNumber))
            {
                return new ValidationResult(ErrorMessage);
            }

            var match = regex.Match(phoneNumber);
            if (match.Success)
            {
                var countryCode = match.Groups[1].Value;
                var areaCode = match.Groups[2].Value;
                var part1 = match.Groups[3].Value;
                var part2 = match.Groups[4].Value;

                // Formatar o número de telefone
                var formattedPhoneNumber = $"+{countryCode} {areaCode} {part1}-{part2}";
                validationContext.ObjectInstance.GetType()
                    .GetProperty(validationContext.MemberName)
                    .SetValue(validationContext.ObjectInstance, formattedPhoneNumber);

                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage);
        }
    }
}
