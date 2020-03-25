using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Uilities
{
    public class ValidFileTypeAttribute : ValidationAttribute
    {
        private readonly string _fileName;
        public ValidFileTypeAttribute(string FileName) =>
            _fileName = FileName ?? throw new ArgumentNullException(nameof(_fileName));

        public override bool IsValid(object value) =>
            (value as IFormFile).FileName.Split(".").Length == 0 ?
            false :
            string.Equals((value as IFormFile).FileName.Split(".")[(value as IFormFile).FileName.Split(".").Length - 1], _fileName, StringComparison.CurrentCultureIgnoreCase);
    }
}
