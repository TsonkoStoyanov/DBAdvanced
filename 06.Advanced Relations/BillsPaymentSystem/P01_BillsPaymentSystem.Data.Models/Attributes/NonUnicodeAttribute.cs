using System.ComponentModel.DataAnnotations;

namespace P01_BillsPaymentSystem.Data.Models.Attributes
{
    public class NonUnicodeAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value==null)
            {
                return new ValidationResult("value cannot be null");
            }


            string text = (string) value;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i]>255)
                {
                    return new ValidationResult("Value cannot contain unicode character");
                }
            }

            return ValidationResult.Success;
        }
    }
}