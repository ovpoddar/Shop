using Microsoft.AspNetCore.Hosting;
using Shop.Entities;
using Shop.Handlers;
using Shop.Repositories;
using Shop.ViewModels;
using Shop.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Shop.Managers
{
    public class CsvManager : ICsvManager
    {
        private readonly IHostingEnvironment _Hosting;
        private readonly ICsvHandler _csvHandler;
        private readonly IGenericRepository<Csv> _Repository;
        private readonly IProductHandler _product;
        private readonly ICsvHelper _function;

        public CsvManager(IHostingEnvironment hosting, ICsvHandler csv, IGenericRepository<Csv> repository, IProductHandler product, ICsvHelper function)
        {
            _Hosting = hosting ?? throw new ArgumentNullException(nameof(_Hosting));
            _csvHandler = csv ?? throw new ArgumentNullException(nameof(_csvHandler));
            _Repository = repository ?? throw new ArgumentNullException(nameof(_Repository));
            _product = product ?? throw new ArgumentNullException(nameof(_product));
            _function = function ?? throw new ArgumentNullException(nameof(_function));
        }

        public void Update(string csv)
        {
            var lines = new StreamReader(csv).ReadToEnd().Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(",");
                for (var j =1; j< 6; j++)
                {
                    _function.Categorieauto(values[j], values[j + 1]);
                }
                _product.AddProduct(new Product()
                {
                    ProductName = values[0],
                    BrandId = _function.BrandId(values[7]),
                    CategoriesId = _function.Categorieauto(values[1]),
                    Price = Convert.ToDecimal(values[11]),
                    StockLevel = Convert.ToDouble(values[9]) * Convert.ToDouble(values[15]) * Convert.ToDouble(values[16]),
                    MinimumWholesaleOrder = Convert.ToDouble(values[12]) * Convert.ToDouble(values[13]),
                    OrderLevel = Convert.ToDouble(values[9]) * Convert.ToDouble(values[15]) * Convert.ToDouble(values[16]),
                    WholesalePrice = _function.WholesaleID(Convert.ToInt32(values[8]), Convert.ToInt32(values[9])),
                });
            }
        }

        public UploadReport Upload(CsvViewModel csv)
        {
            var random = new Random();
            var relativePath = Path.Combine("Userfile", random.Next().ToString());
            var fullPath = Path.Combine(_Hosting.WebRootPath, relativePath);
            _csvHandler.StoreCsvAsFile(fullPath, csv.Csv);
            var stream = new FileStream(fullPath, FileMode.Open);
            var hashValue = BitConverter.ToString(MD5.Create().ComputeHash(stream));
            var time = DateTime.Now.ToString();
            stream.Close();
            if (!_Repository.GetAll().Any(p => p.HashName == hashValue))
            {
                _csvHandler.SaveCsv(csv.Csv.FileName, relativePath, hashValue, time);
                return new UploadReport()
                {
                    Path = fullPath,
                    Success = true
                };
            }
            else
            {
                _csvHandler.Delete(fullPath);
                return new UploadReport()
                {
                    Path = null,
                    Success = false
                };
            }
        }
    }
}
