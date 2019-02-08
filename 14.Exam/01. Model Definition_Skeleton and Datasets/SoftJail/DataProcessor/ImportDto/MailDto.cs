using System.ComponentModel.DataAnnotations;

namespace SoftJail.DataProcessor.ImportDto
{
    public class MailDto
    {
        
        public string Description { get; set; }

        
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9\s]+str.$")]
        public string Address { get; set; }
    }
}