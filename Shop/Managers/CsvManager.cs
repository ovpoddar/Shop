﻿using Microsoft.AspNetCore.Hosting;
using Shop.Entities;
using Shop.Handlers;
using Shop.Helpers;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Shop.Managers
{
    public class CsvManager : ICsvManager
    {
        private readonly IHostingEnvironment _hosting;
        private readonly ICsvHandler _csvHandler;
        private readonly IGenericRepository<Csv> _repository;
        private readonly IProductHandler _product;
        private readonly ICsvHelper _csvHelper;

        public CsvManager(IHostingEnvironment hosting, ICsvHandler csv, IGenericRepository<Csv> repository, IProductHandler product, ICsvHelper function)
        {
            _hosting = hosting ?? throw new ArgumentNullException(nameof(_hosting));
            _csvHandler = csv ?? throw new ArgumentNullException(nameof(_csvHandler));
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _product = product ?? throw new ArgumentNullException(nameof(_product));
            _csvHelper = function ?? throw new ArgumentNullException(nameof(_csvHelper));
        }

        public void Update(string csv)
        {
            var lines = new StreamReader(csv).ReadToEnd().Replace("\r", "").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(",");
                for (var j = 1; j < 6; j++)
                    _csvHelper.Categoryauto(values[j], values[j + 1]);
                _product.AddProduct(new Product()
                {
                    ProductName = values[0],
                    BrandId = _csvHelper.BrandId(values[7]),
                    CategoriesId = _csvHelper.Categoryauto(values[1]),
                    Price = Convert.ToDecimal(values[11]),
                    StockLevel = Convert.ToDouble(values[9]) * Convert.ToDouble(values[15]) * Convert.ToDouble(values[16]),
                    MinimumWholesaleOrder = Convert.ToDouble(values[12]) * Convert.ToDouble(values[13]),
                    OrderLevel = Convert.ToDouble(values[9]) * Convert.ToDouble(values[15]) * Convert.ToDouble(values[16]),
                    WholesalePrice = _csvHelper.WholesaleID(Convert.ToInt32(values[8]), Convert.ToInt32(values[9])),
                });
            }
        }

        public UploadReport Upload(CsvViewModel csv)
        {
            var random = new Random();
            var relativePath = Path.Combine("Userfile", random.Next().ToString());
            var fullPath = Path.Combine(_hosting.WebRootPath, relativePath);
            _csvHandler.StoreCsvAsFile(fullPath, csv.Csv);
            using (var stream = new FileStream(fullPath, FileMode.Open))
            {
                var hashValue = BitConverter.ToString(MD5.Create().ComputeHash(stream));
                if (!_repository.GetAll().Any(p => p.HashName == hashValue))
                {
                    _csvHandler.SaveCsv(csv.Csv.FileName, relativePath, hashValue, DateTime.Now.ToString());
                    return new UploadReport()
                    {
                        Path = fullPath,
                        Success = true
                    };
                }
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
