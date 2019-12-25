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

        public CsvManager(IHostingEnvironment hosting, ICsvHandler csv, IGenericRepository<Csv> repository)
        {
            _Hosting = hosting ?? throw new ArgumentNullException(nameof(_Hosting));
            _csvHandler = csv ?? throw new ArgumentNullException(nameof(_csvHandler));
            _Repository = repository ?? throw new ArgumentNullException(nameof(_Repository));
        }

        public void Update(string csv)
        {
            string file = new StreamReader(csv).ReadToEnd();
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
