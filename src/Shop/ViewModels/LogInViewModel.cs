using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        [Display(Name = "Enter your Username, Number or Email")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
