using Microsoft.AspNetCore.Hosting;
using Shop.Entities;
using Shop.Handlers;
using Shop.Helpers;
using Shop.Models;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Shop.Managers
{
    public class CsvManager : ICsvManager
    {
        private readonly IWebHostEnvironment _hosting;
        private readonly ICsvHandler _csvHandler;
        private readonly IGenericRepository<Csv> _repository;
        private readonly IProductHandler _product;
        private readonly ICsvHelper _csvHelper;
        private readonly IProtectorHandler _protector;

        public CsvManager(IWebHostEnvironment hosting, ICsvHandler csv, IGenericRepository<Csv> repository, IProductHandler product, ICsvHelper function, IProtectorHandler protector)
        {
            _hosting = hosting ?? throw new ArgumentNullException(nameof(_hosting));
            _csvHandler = csv ?? throw new ArgumentNullException(nameof(_csvHandler));
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _product = product ?? throw new ArgumentNullException(nameof(_product));
            _csvHelper = function ?? throw new ArgumentNullException(nameof(_csvHelper));
            _protector = protector ?? throw new ArgumentNullException(nameof(_protector));
        }

        public void Update(string csv)
        {
            var lines = new StreamReader(csv).ReadToEnd().Replace("\r", "").Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(",");
                _product.AddProduct(new Product()
                {
                    ProductName = values[0],
                    BarCode = Convert.ToString(values[1]),
                    CategoriesId = _csvHelper.Categoryauto(values[2]),
                    BrandId = _csvHelper.BrandId(values[8]),
                    WholesalePrice = _csvHelper.WholesaleID(Convert.ToInt32(values[9]), Convert.ToInt32(values[10])),
                    StockLevel = Convert.ToInt32(Convert.ToDouble(values[10]) * Convert.ToDouble(values[16]) * Convert.ToDouble(values[17])),
                    OrderLevel = Convert.ToDouble(values[10]) * Convert.ToDouble(values[16]) * Convert.ToDouble(values[17]),
                    Price = Convert.ToDecimal(values[12]),
                    MinimumWholesaleOrder = Convert.ToDouble(values[13]) * Convert.ToDouble(values[14]),
                });
                for (var j = 2; j < 7; j++)
                    _csvHelper.Categoryauto(values[j], values[j + 1]);
            }
        }

        public Upload Upload(CsvViewModel csv)
        {
            var relativePath = Path.Combine("Userfile", new Random().Next().ToString());
            var fullPath = Path.Combine(_hosting.WebRootPath, relativePath);
            _csvHandler.StoreCsvAsFile(fullPath, csv.Csv);

            using var stream = new FileStream(fullPath, FileMode.Open);
            var hashValue = _protector.HashMd5(stream);
            if (!_repository.GetAll().Any(p => p.HashName == hashValue))
            {
                _csvHandler.SaveCsv(csv.Csv.FileName, relativePath, hashValue, DateTime.Now.ToString(CultureInfo.InvariantCulture));
                return new Upload()
                {
                    Path = fullPath,
                    Success = true
                };
            }
            stream.Close();
            _csvHandler.Delete(fullPath);
            return new Upload()
            {
                Path = null,
                Success = false
            };
        }
    }
}
