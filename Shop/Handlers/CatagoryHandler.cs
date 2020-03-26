using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryHandler(IGenericRepository<Category> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public CategorieReport AddCategory(CategoryViewModel model)
        {
            if (_repository.GetAll().Any(category => string.Equals(category.Name, model.Name, StringComparison.CurrentCultureIgnoreCase) && 
                                                     category.ParentId == model.Id) || string.IsNullOrWhiteSpace(model.Name))
                return new CategorieReport
                {
                    All = _repository.GetAll().ToList(),
                    Success = false
                };

            _repository.Add(new Category
            {
                ParentId = model.Id,
                Name = model.Name
            });
            _repository.save();
            return new CategorieReport
            {
                All = _repository.GetAll().ToList(),
                Success = true
            };
        }

        public List<Category> Categories() =>
            _repository.GetAll().Where(category => category.ParentId == null)
                .Include(p => p.SubCategories).ToList();

        public List<Category> GetAll() => _repository.GetAll().ToList();

        public int GetId(string name) =>
            _repository.GetAll()
            .Where(o => string.Equals(o.Name, name, StringComparison.CurrentCultureIgnoreCase))
            .Select(o => o.Id)
            .FirstOrDefault();
    }
}
