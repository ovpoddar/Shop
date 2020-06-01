using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using Shop.Handlers.Interfaces;
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

        public List<Suggestion> GetSuggestions(string name) =>
            _mapper.Map<List<Product>, List<Suggestion>>(_repository.GetAll()
                .Include(o => o.Brands)
                .Where(o => o.ProductName.Contains(name) || o.BarCode.Contains(name))
                .ToList());
    }
}
