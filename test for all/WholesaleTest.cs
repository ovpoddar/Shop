using Moq;
using Shop.Entities;
using Shop.Handlers;
using Shop.Repositories;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Xunit;

namespace test_for_all
{
    public class WholesaleTest
    {
        private readonly Mock<IGenericRepository<WholesaleSize>> _mock;
        private readonly Mock<IGenericRepository<ProductWholeSale>> _mockcon;
        private readonly WholesaleHandler _wholesaleHandler;
        public WholesaleTest()
        {
            _mock = new Mock<IGenericRepository<WholesaleSize>>();
            _mockcon = new Mock<IGenericRepository<ProductWholeSale>>();
            _wholesaleHandler = new WholesaleHandler(_mock.Object, _mockcon.Object);
        }
        [Theory]
        [InlineData(1,1,6)]
        [InlineData(5,40,90)]
        public void GetId_ReturnAll(int id, int size, int pack)
        {
            _mock
                .Setup(_ => _.GetAll())
                .Returns(new List<WholesaleSize>
                {
                    new WholesaleSize()
                    {
                        Id = id,
                        Size = size,
                        Package = pack
                    }

                }.AsQueryable());
            var result = _wholesaleHandler.GetId(size,pack);
            Assert.Equal(result, id);
        }

        [Theory]
        [InlineData(1 ,1, false)]
        [InlineData(6, 25, true)]
        public void Add_checkingbothSituation(int addsi, int addpa ,bool type)
        {
            _mock
                .Setup(_ => _.GetAll())
                .Returns(new List<WholesaleSize>
                {
                    new WholesaleSize()
                    {
                        Id = 1,
                        Size = 1,
                        Package = 1
                    },
                    new WholesaleSize()
                    {
                        Id = 2,
                        Size = 2,
                        Package = 2
                    },

                }.AsQueryable());

            _mock
                .Setup(_ => _.Add(It.IsAny<WholesaleSize>()));

            if (type)
                Assert.True(_wholesaleHandler.Add(new WholeSaleViewModel()
                {
                    Size = addsi,
                    Package = addpa
                }));
            else
                Assert.False(_wholesaleHandler.Add(new WholeSaleViewModel()
                {
                    Size = addsi,
                    Package = addpa
                }));
        }
    }
}
