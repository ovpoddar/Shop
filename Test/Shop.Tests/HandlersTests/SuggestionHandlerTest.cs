using AutoMapper;
using DataAccess.Entities;
using DataAccess.Repositories;
using Moq;
using Shop.Handlers;
using Shop.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class SuggestionHandlerTest
    {
        private readonly Mock<IGenericRepository<Product>> _repository;
        private readonly Mock<IMapper> _mapper;
        private readonly SuggestionHandler _suggestionHandler;
        public SuggestionHandlerTest()
        {
            _repository = new Mock<IGenericRepository<Product>>();
            _mapper = new Mock<IMapper>();
            _suggestionHandler = new SuggestionHandler(_repository.Object, _mapper.Object);
        }
        [Fact]
        public void SuggestionHandler_test()
        {
            var list = new List<Product>()
            {
                new Product()
                {
                    ProductName = "cola",
                    BarCode = "134654131654135432165",
                    BrandId = It.IsAny<int>(),
                    CategoriesId = It.IsAny<int>(),
                    Id =  It.IsAny<int>(),
                    WholesalePrice = It.IsAny<double>(),
                    StockLevel =  It.IsAny<int>(),
                    Price=  It.IsAny<decimal>(),
                    MinimumWholesaleOrder= It.IsAny<double>(),
                    OrderLevel = It.IsAny<double>()
                },
                new Product()
                {
                    ProductName = "cola milk",
                    BarCode = "564312346495643165",
                    BrandId = It.IsAny<int>(),
                    CategoriesId = It.IsAny<int>(),
                    Id =  It.IsAny<int>(),
                    WholesalePrice = It.IsAny<double>(),
                    StockLevel =  It.IsAny<int>(),
                    Price=  It.IsAny<decimal>(),
                    MinimumWholesaleOrder= It.IsAny<double>(),
                    OrderLevel = It.IsAny<double>()
                }
            }.AsQueryable();

            var output = new List<Suggestion>()
            {
                new Suggestion
                {
                    Id = It.IsAny<int>(),
                    Brand = It.IsAny<string>(),
                    Name = "cola",
                    Price = It.IsAny<decimal>(),
                },
                new Suggestion
                {
                    Id = It.IsAny<int>(),
                    Brand = It.IsAny<string>(),
                    Name = "cola milk",
                    Price = It.IsAny<decimal>(),
                }
            };

            _mapper
                .Setup(e => e.Map<List<Product>, List<Suggestion>>(It.IsAny<List<Product>>()))
                .Returns(output);

            _repository
                .Setup(e => e.GetAll())
                .Returns(list);

            var result = _suggestionHandler.GetSuggestions("cola");

            Assert.NotNull(result);
        }
    }
}
