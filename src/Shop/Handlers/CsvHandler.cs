using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Shop.Handlers.Interfaces;
using System;
using System.IO;
using System.Text;

namespace Shop.Handlers
{
    public class CsvHandler : ICsvHandler
    {
        private readonly IGenericRepository<Csv> _repository;

        public CsvHandler(IGenericRepository<Csv> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));

        public void Delete(string path) =>
            File.Delete(path);

        public void SaveCsv(string name, string path, string hash, string time)
        {
            _repository.Add(new Csv()
            {
                FileName = name,
                Filepath = path,
                HashName = hash,
                UpdateDate = time
            });
            _repository.Save();
        }

        public void StoreCsvAsFile(string name, IFormFile csv)
        {
            var file = new StringBuilder();
            var text = new StreamReader(csv.OpenReadStream());
            while (text.Peek() >= 0)
                file.AppendLine(text.ReadLine());

            File.WriteAllText(name, file.ToString());
        }
    }
}
