using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Utilities
{
    public class ValidFileTypeAttribute : ValidationAttribute
    {
        private readonly string _fileName;
        public ValidFileTypeAttribute(string FileName) =>
            _fileName = FileName ?? throw new ArgumentNullException(nameof(_fileName));

        public override bool IsValid(object value) =>
            (value as IFormFile).FileName.Split(".").Length == 0 || value == null ?
            false :
            (value as IFormFile).FileName.Split(".")[^1].ToUpper() == _fileName.ToUpper();
    }
}
