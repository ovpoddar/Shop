using Microsoft.AspNetCore.Http;
using Shop.Uilities;
using System.ComponentModel.DataAnnotations;

namespace Shop.ViewModels
{
    public class CsvViewModel
    {
        [Required]
        [ValidFileType(FileName: "Csv", ErrorMessage = "only csv files are allowed")]
        public IFormFile Csv { get; set; }
    }
}
