using FluentAssertions;
using Microsoft.VisualBasic.CompilerServices;
using Moq;
using Shop.Entities;
using Shop.Handlers.Interfaces;
using Shop.Helpers;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Xunit;

namespace Shop.Tests.HelpersTests
{
    public class CsvHelperTest
    {
        private readonly Mock<IBrandHandler> _brandHandler;
        private readonly Mock<IWholesaleHandler> _wholesaleHandler;
        private readonly Mock<ICategoryHandler> _categoryHandler;
        private readonly CsvHelper _csvHelper;
        public CsvHelperTest()
        {
            _brandHandler = new Mock<IBrandHandler>();
            _categoryHandler = new Mock<ICategoryHandler>();
            _wholesaleHandler = new Mock<IWholesaleHandler>();
            _csvHelper = new CsvHelper(_brandHandler.Object, _wholesaleHandler.Object, _categoryHandler.Object);
        }
        [Fact]
        public void WholesaleIDTest()
        {
            var output = 2;
            _wholesaleHandler
                .Setup(e => e.Add(It.IsAny<WholeSaleViewModel>()))
                .Returns(true);

            _wholesaleHandler
                .Setup(e => e.GetId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(output);

            var result = _csvHelper.WholesaleID(20, 60);

            result.Should().Be(output);
            _wholesaleHandler.Verify(e => e.Add(It.IsAny<WholeSaleViewModel>()), Times.Once);
        }

        [Fact]
        public void CategoryautoTest()
        {
            var output = 2;

            _categoryHandler
                .Setup(e => e.AddCategory(It.IsAny<CategoryViewModel>()));

            _categoryHandler
                .Setup(e => e.GetId(It.IsAny<string>()))
                .Returns(output);

            var result  = _csvHelper.Categoryauto("name");

            result.Should().Be(output);
            _categoryHandler.Verify(e => e.AddCategory(It.IsAny<CategoryViewModel>()), Times.Once);
        }

        [Fact]
        public void CategoryautoOverloaderTest()
        {

            _categoryHandler
                .Setup(e => e.AddCategory(It.IsAny<CategoryViewModel>()));

            _csvHelper.Categoryauto("mainName", "underName");

            _categoryHandler.Verify(e => e.AddCategory(It.IsAny<CategoryViewModel>()), Times.Once);
        }

        [Fact]
        public void BrandIdTest()
        {
            var output = 2;
            _brandHandler
                .Setup(e => e.AddBrand(It.IsAny<Brand>()))
                .Returns(true);

            _brandHandler
                .Setup(e => e.GetId(It.IsAny<string>()))
                .Returns(output);

            var result = _csvHelper.BrandId("name");
            result.Should().Be(output);
            _brandHandler
                .Verify(e => e.AddBrand(It.IsAny<Brand>()), Times.Once);
        }
    }
}
