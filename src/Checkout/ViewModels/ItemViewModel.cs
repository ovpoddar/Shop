using System.ComponentModel.DataAnnotations;

namespace Checkout.ViewModels
{
    public class ItemViewModel
    {
        [Required]
        [RegularExpression(@"^[a-z A-Z]+$", ErrorMessage = "Use letters only please")]
        public string Name { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "must contain valid number")]
        public int Quantity { get; set; }
    }
}
