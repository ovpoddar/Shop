using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Models;
using Shop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Handlers
{
    public class SuggestionHandler : ISuggestionHandler
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public SuggestionHandler(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(_repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }

        public List<SuggestionModel> GetSuggestions(string name) =>
            _mapper.Map<List<Product>, List<SuggestionModel>>(_repository.GetAll()
                .Include(o => o.Brand)
                .Where(o => o.ProductName.Contains(name) || o.BarCode.Contains(name))
                .ToList());

        public SuggestionModel SelectSuggestion(string name) =>
            _mapper.Map<SuggestionModel>(_repository.GetAll()
                .Include(o => o.Brand)
                .Where(o => o.ProductName == name || o.BarCode.Contains(name))
                .FirstOrDefault());

    }
}
