using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.Uilities
{
    public class ValidFileTypeAttribute : ValidationAttribute
    {
        private readonly string _fileName;
        public ValidFileTypeAttribute(string FileName)
        {
            _fileName = FileName ?? throw new ArgumentNullException(nameof(_fileName));
        }


        public override bool IsValid(object value)
        {
            IFormFile file = value as IFormFile;
            string[] name = file.FileName.Split(".");
            if (name.Length == 0)
                return false;
            else
                if (name[(name.Length - 1)].ToUpper() == _fileName.ToUpper())
                    return true;
                else
                    return false;
                
        }
    }
}
