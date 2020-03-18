using Shop.Entities;
using Shop.Repositories;
using System;
using System.Linq;

namespace Shop.Handlers
{
    public class BrandHandler : IBrandHandler

    {
        private readonly IGenericRepository<Brand> _repository;

        public BrandHandler(IGenericRepository<Brand> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        
        public bool AddBrand(Brand brand)
        {
            if (_repository.GetAll().Any(p => p.BrandName.ToLower() == brand.BrandName.ToLower()))
                return false;
            _repository.Add(brand);
            _repository.save();
            return true;
        }

        public int GetId(string Name)
        {
            var brand = _repository.GetAll()
.FirstOrDefault(o => string.Equals(o.BrandName, Name, StringComparison.CurrentCultureIgnoreCase));
            return brand?.Id ?? 0;

        }
    }
}
