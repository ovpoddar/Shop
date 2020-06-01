using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Models;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Handlers.Interfaces;

namespace Shop.Handlers
{
    public class CategoryHandler : ICategoryHandler
    {
        private readonly IGenericRepository<Category> _repository;

        public CategoryHandler(IGenericRepository<Category> repository) =>
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

        public Results<IEnumerable<Category>> AddCategory(CategoryViewModel model)
        {
            var categories = _repository.GetAll().ToList();

            if (categories.Any(category => category.Name.ToUpper() == model.Name.ToUpper() &&
                                           category.ParentId == model.Id) ||
                string.IsNullOrWhiteSpace(model.Name))

                return new Results<IEnumerable<Category>>
                {
                    Result = categories,
                    Success = false
                };

            _repository.Add(new Category
            {
                ParentId = model.Id,
                Name = model.Name
            });

            _repository.Save();

            return new Results<IEnumerable<Category>>
            {
                Result = _repository.GetAll().ToList(),
                Success = true
            };
        }

        public List<Category> Categories() =>
            _repository.GetAll()
            .Where(category => category.ParentId == null)
            .Include(p => p.SubCategories).ToList();

        public List<Category> GetAll() =>
            _repository.GetAll()
            .ToList();

        public int GetId(string name) =>
            _repository.GetAll()
            .Where(o => o.Name.ToUpper() == name.ToUpper())
            .Select(o => o.Id)
            .FirstOrDefault();
    }
}
