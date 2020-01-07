using Microsoft.AspNetCore.Hosting;
using Shop.Entities;
using Shop.Handlers;
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
        private readonly IHostingEnvironment _Hosting;
        private readonly ICsvHandler _csvHandler;
        private readonly IGenericRepository<Csv> _Repository;
        private readonly IBrandHandler _brand;
        private readonly IWholesaleHandler _WholesaleHandler;
        private readonly IProductHandler _product;
        private readonly ICategoryHandler _catagory;

        public CsvManager(IHostingEnvironment hosting, ICsvHandler csv, IGenericRepository<Csv> repository, IBrandHandler brand, IProductHandler product, IWholesaleHandler WholesaleHandler, ICategoryHandler catagory)
        {
            _Hosting = hosting ?? throw new ArgumentNullException(nameof(_Hosting));
            _csvHandler = csv ?? throw new ArgumentNullException(nameof(_csvHandler));
            _Repository = repository ?? throw new ArgumentNullException(nameof(_Repository));
            _brand = brand ?? throw new ArgumentNullException(nameof(_brand));
            _WholesaleHandler = WholesaleHandler ?? throw new ArgumentNullException(nameof(_WholesaleHandler));
            _product = product ?? throw new ArgumentNullException(nameof(_product));
            _catagory = catagory ?? throw new ArgumentNullException(nameof(_catagory));
        }





        private double WholesaleID(int value1, int value2)
        {
            WholeSaleViewModel wholesaleSize = new WholeSaleViewModel()
            {
                Package = value1,
                Size = value2
            };
            _WholesaleHandler.Add(wholesaleSize);
            return _WholesaleHandler.GetId(value1, value2);
        }

        private int Categorieauto(string v1)
        {
            CategoryViewModel model = new CategoryViewModel()
            {
                Name = v1
            };
            _catagory.AddCategory(model);
            return _catagory.GetId(v1);
        }
        private void Categorieauto(string v1, string v2)
        {
            var id = _catagory.GetId(v1);
            CategoryViewModel model = new CategoryViewModel()
            {
                Id = id,
                Name = v2
            };
            _catagory.AddCategory(model);
        }

        private int BrandId(string value)
        {
            Brand brand = new Brand()
            {
                BrandName = value
            };
            _brand.AddBrand(brand);
            return _brand.GetId(value);
        }





        public void Update(string csv)
        {
            string file = new StreamReader(csv).ReadToEnd();
            string fullLine = file.Replace("\r", "");
            string[] lines = fullLine.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(",");
                var catagorieid = Categorieauto(values[1]);
                for(var j =1; j< 6; j++)
                {
                    Categorieauto(values[j], values[j + 1]);
                }
                Product product = new Product()
                {
                    ProductName = values[0],
                    BrandId = BrandId(values[7]),
                    CategoriesId = catagorieid,
                    Price = Convert.ToDouble(values[11]),
                    StockLevel = Convert.ToDouble(values[9]) * Convert.ToDouble(values[15]) * Convert.ToDouble(values[16]),
                    MinimumWholesaleOrder = Convert.ToDouble(values[12]) * Convert.ToDouble(values[13]),
                    OrderLevel = Convert.ToDouble(values[9]) * Convert.ToDouble(values[15]) * Convert.ToDouble(values[16]),
                    wholesalePrice = WholesaleID(Convert.ToInt32(values[8]), Convert.ToInt32(values[9])),
                };
                _product.AddProduct(product);
            }

        }

        public UploadReport Upload(CsvViewModel csv)
        {
            string hashvalue = null;
            Random random = new Random();
            string RelativePath = Path.Combine("Userfile", random.Next().ToString());
            string FullPath = Path.Combine(_Hosting.WebRootPath, RelativePath);
            _csvHandler.Store(FullPath, csv.Csv);
            FileStream stream = new FileStream(FullPath, FileMode.Open);
            hashvalue = BitConverter.ToString(MD5.Create().ComputeHash(stream));
            string time = DateTime.Now.ToString();
            stream.Close();
            if (!_Repository.GetAll().Any(p => p.HashName == hashvalue))
            {
                _csvHandler.Save(csv.Csv.FileName, RelativePath, hashvalue, time);
                UploadReport report = new UploadReport()
                {
                    Path = FullPath,
                    Success = true
                };
                return report;
            }
            else
            {
                _csvHandler.Delete(FullPath); 
                UploadReport report = new UploadReport()
                {
                    Path = null,
                    Success = false
                };
                return report;
            }
        }
    }
}
