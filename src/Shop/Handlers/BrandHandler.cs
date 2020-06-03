using DataAccess.Entities;
using Shop.Handlers.Interfaces;
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
            if (_repository.GetAll().Any(p => p.BrandName.ToUpper() == brand.BrandName.ToUpper()))
                return false;
            _repository.Add(brand);
            _repository.Save();
            return true;

        }

        public Brand AddBrandWithReturn(Brand brand)
        {
            if (_repository.GetAll().Any(p => p.BrandName.ToUpper() == brand.BrandName.ToUpper()))
                return brand;
            _repository.Add(brand);
            _repository.Save();
            return brand;
        }

        public int GetId(string name) =>
            _repository.GetAll()
            .FirstOrDefault(o => o.BrandName.ToUpper() == name.ToUpper())?.Id ?? 0;
    }
}
