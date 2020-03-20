using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Uilities
{
    public class ValidFileTypeAttribute : ValidationAttribute
    {
        private readonly string _fileName;
        public ValidFileTypeAttribute(string fileName) =>
            _fileName = fileName ?? throw new ArgumentNullException(nameof(_fileName));

        public override bool IsValid(object value) => (value as IFormFile).FileName.Split(".").Length == 0 ? false : (value as IFormFile).FileName.Split(".")[(value as IFormFile).FileName.Split(".").Length - 1].ToUpper() == _fileName.ToUpper();
    }
}
