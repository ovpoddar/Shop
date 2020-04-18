using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Shop.Utilities;

namespace Shop.ViewModels
{
    public class CsvViewModel
    {
        [Required(ErrorMessage = "select a csv file")]
        [ValidFileType(FileName: "Csv", ErrorMessage = "only csv files are allowed")]
        public IFormFile Csv { get; set; }
    }
}
