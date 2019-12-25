using Microsoft.AspNetCore.Http;
using Shop.Entities;
using Shop.Repositories;
using System;
using System.IO;
using System.Text;

namespace Shop.Handlers
{
    public class CsvHandler : ICsvHandler
    {
        private readonly IGenericRepository<Csv> _Repository;

        public CsvHandler(IGenericRepository<Csv> repository)
        {
            _Repository = repository ?? throw new ArgumentNullException(nameof(_Repository));
        }
        public void Delete(string path)
        {
            File.Delete(path);
        }

        public void Save(string name, string Path, string hash, string time)
        {
            Csv csv = new Csv()
            {
                FileName = name,
                Filepath = Path,
                HashName = hash,
                UpdateDate = time
            };
            _Repository.Add(csv);
            _Repository.save();
        }

        public void Store(string name, IFormFile csv)
        {
            var file = new StringBuilder();
            var text = new StreamReader(csv.OpenReadStream());
            while (text.Peek() >= 0)
                file.AppendLine(text.ReadLine());
            var filetext = file.ToString();
            File.WriteAllText(name, filetext);
            
        }
    }
}
