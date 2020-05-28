using FluentAssertions;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Repositories;
using System;
using System.IO;
using System.Text;
using Xunit;

namespace Shop.Tests.HandlersTests
{
    public class CsvHandlerTest
    {
        private readonly Mock<IGenericRepository<Csv>> _mock;
        private readonly CsvHandler _csvHandler;
        public CsvHandlerTest()
        {
            _mock = new Mock<IGenericRepository<Csv>>();
            _csvHandler = new CsvHandler(_mock.Object);
        }
        [Fact]
        public void DeleteTest()
        {
            var path = "C:\\Users\\ayanp\\Desktop\\Shop\\src\\Shop\\wwwroot\\Userfile\\test";

            var file = new StringBuilder();
            File.WriteAllText(path, file.ToString());

            _csvHandler.Delete(path);
            try
            {
                Directory.EnumerateFiles(path);
            }
            catch(Exception ex)
            {
                Assert.NotNull(ex.Message);
                Assert.Equal("System.IO.FileSystem", ex.Source);
            }
        }

        [Fact]
        public void SaveCsvTest()
        {
            _mock.Setup(e => e.Add(It.IsAny<Csv>()));
            _mock.Setup(e => e.Save());

            _csvHandler.SaveCsv("consan", "C:\\Users\\ayanp\\Desktop\\Shop\\src\\Shop\\wwwroot\\Userfile\\test", "ausadhciubabcuibcd", DateTime.Now.ToShortDateString());


            _mock.Verify(method => method.Add(It.IsAny<Csv>()), Times.Once);
            _mock.Verify(method => method.Save(), Times.Once);
        }

        /// stroing left
    }
}
