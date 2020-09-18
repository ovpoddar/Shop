using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic;
using Moq;
using Shop.Handlers.Interfaces;
using Shop.Helpers.Interfaces;
using Shop.Managers;
using Shop.ViewModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace Shop.Tests.ManagersTests
{
    public class CsvManagerTest
    {
        private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        private readonly Mock<ICsvHandler> _csvHandler;
        private readonly Mock<IGenericRepository<Csv>> _genericRepository;
        private readonly Mock<IProductHandler> _productHandler;
        private readonly Mock<ICsvHelper> _csvHelper;
        private readonly Mock<IProtectorHandler> _protectorHandler;
        private readonly CsvManager _csvManager;
        public CsvManagerTest()
        {
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _protectorHandler = new Mock<IProtectorHandler>();
            _productHandler = new Mock<IProductHandler>();
            _genericRepository = new Mock<IGenericRepository<Csv>>();
            _csvHelper = new Mock<ICsvHelper>();
            _csvHandler = new Mock<ICsvHandler>();
            _csvManager = new CsvManager(_webHostEnvironment.Object, _csvHandler.Object, _genericRepository.Object, _productHandler.Object, _csvHelper.Object, _protectorHandler.Object);
        }
        [Fact]
        public void UpdateTest()
        {
            var csv = "C:\\Users\\ayanp\\Desktop\\Shop\\src\\Shop\\wwwroot\\Userfile\\1099075911";
            _productHandler
                .Setup(e => e.AddProduct(It.IsAny<Product>()))
                .Returns(true);
            _csvHelper
                .Setup(e => e.Categoryauto(It.IsAny<string>()))
                .Returns(1);
            _csvHelper
                .Setup(e => e.BrandId(It.IsAny<string>()))
                .Returns(1);
            _csvHelper
                .Setup(e => e.WholesaleID(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(20);
            _csvHelper
                .Setup(e => e.Categoryauto(It.IsAny<string>(), It.IsAny<string>()));

            _csvManager.Update(csv);

            _productHandler
                .Verify(e => e.AddProduct(It.IsAny<Product>()), Times.AtLeastOnce);
            _csvHelper
                .Verify(e => e.Categoryauto(It.IsAny<string>()), Times.AtLeastOnce);
            _csvHelper
                .Verify(e => e.BrandId(It.IsAny<string>()), Times.AtLeastOnce);
            _csvHelper
                .Verify(e => e.WholesaleID(It.IsAny<int>(), It.IsAny<int>()), Times.AtLeastOnce);
            _csvHelper
                .Verify(e => e.Categoryauto(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void UploadTest()
        {
            var file = new CsvViewModel()
            {
                Csv = It.IsAny<IFormFile>(),
            };
            //need to replace the stream
            _webHostEnvironment
                .Setup(e => e.WebRootPath)
                .Returns(It.IsAny<string>())
                .Verifiable();
            _csvHandler
                .Setup(e => e.StoreCsvAsFile(It.IsAny<string>(), It.IsAny<IFormFile>()))
                .Verifiable();
            _csvHandler
                .Setup(e => e.SaveCsv(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();
            _csvHandler
                .Setup(e => e.Delete(It.IsAny<string>()))
                .Verifiable();
            _genericRepository
                .Setup(e => e.GetAll())
                .Returns(new List<Csv> { new Csv() { FileName = "coco", Filepath = "path", HashName = "secratename", Id = 1, UpdateDate = DateAndTime.Now.ToLongDateString() } }.AsQueryable())
                .Verifiable();
            _protectorHandler
                .Setup(e => e.HashMd5(It.IsAny<Stream>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            _csvManager.Upload(file);
        }


    }
}
