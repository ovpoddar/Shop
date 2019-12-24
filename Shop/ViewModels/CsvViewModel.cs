using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels
{
    public class CsvViewModel
    {
        [Required]
        public IFormFile Csv { get; set; }
    }
}
