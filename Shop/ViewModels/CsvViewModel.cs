using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace Shop.ViewModels
{
    public class CsvViewModel
    {
        [Required]
        public IFormFile Csv { get; set; }
    }
}
