using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using FizzWare.NBuilder;
using NSubstitute;
using NUnit.Framework;
using RepositoryLayer;
using ServiceLayer;
using UnitTests.Generators;

namespace UnitTests.Services
{
    [TestFixture]
    public class StockServiceTests
    {
        private  IStockService _stockService;
        private  IStockRepository _stockRepository;
        private IGenerator<Stock> _generator;

        [SetUp]
        public void Setup()
        {
            _stockRepository = Substitute.For<IStockRepository>();
            _stockService = new StockService(_stockRepository);
            _generator = new StockGenerator();
        }
        
        public static Stock StockBuilder(int id, string title, string symbol, List<UserStocks> usersStocks = null,
            List<Comment> comments = null)
        {
            return new Builder().CreateNew<Stock>()
                .With(s => s.Id = id)
                .With(s => s.Title = title)
                .With(s => s.Symbol = symbol)
                .With(s => s.UsersStocks = usersStocks ??= new List<UserStocks>())
                .With(s => s.Comments = comments ??= new List<Comment>())
                .Build();
        }

        [Test]
        public void GetStock_Should_Return_Specific_Stocks()

        {
            //Arrange
            var stocks = _generator.GenerateEnum(5);
            _stockRepository.GetAll().Returns(stocks);
            //Act
            var result = _stockService.GetAllStocks().Data;
            // Assert
            Assert.AreEqual(result, stocks);
        }

        [Test]
        public void GetStock_Should_Return_Specific_Stock()
        {
            //Arrange
            var stock = _generator.GenerateEnum(1).First();
            _stockRepository.Get(stock.Id).Returns(stock);
            
            //Act
            var result = _stockRepository.Get(stock.Id);
            
            //Assert
            Assert.AreEqual(result, stock);
        }

        [Test]
        public void GetStock_BySymbol_Should_Return_Stock_And_ApiResponse_True()
        {
            //Arrange
            var stock = _generator.GenerateEnum(1).First();
            _stockRepository.GetBySymbol(stock.Symbol).Returns(stock);
            //Act
            var result = _stockService.GetStock(stock.Symbol);
            //Assert
            Assert.AreEqual(stock,result.Data);
            Assert.IsTrue(result.Succeed);
        }
        

        [Test]
        public void InsertStock_Should_Return_ApiResponseTrue()
        {
            //Arrange
            var stock = _generator.GenerateEnum(1).First();

            //Act

            var result = _stockService.InsertStock(stock);

            //Arrange

            Assert.IsTrue(result.Succeed);

        }

        [Test]
        public void UpdateStock_Should_Return_ApiResponseTrue()
        {
            //Assert
            var stock = _generator.GenerateEnum(1).First();
            _stockRepository.Get(stock.Id).Returns(stock);
            //Act
            var result = _stockService.DeleteStock(stock.Id);
            //Assert
            Assert.IsTrue(result.Succeed);
        }
        
    }
}