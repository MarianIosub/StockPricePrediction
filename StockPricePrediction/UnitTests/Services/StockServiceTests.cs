using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using FizzWare.NBuilder;
using NSubstitute;
using NUnit.Framework;
using RepositoryLayer;
using ServiceLayer;
using UnitTests.Generators;


namespace UnitTests
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
        
        public static Stock StockBuilder(int Id, string Title, string Symbol, List<UserStocks> usersStocks = null,
            List<Comment> comments = null)
        {
            return new Builder().CreateNew<Stock>()
                .With(s => s.Id = Id)
                .With(s => s.Title = Title)
                .With(s => s.Symbol = Symbol)
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
            var result = _stockService.GetAllStocks();
            // Assert
            Assert.AreEqual(result, stocks);
        }

        [Test]
        public void GetStock_Should_Return_Specific_Stock()
        {
            //Arrange
            var stock = _generator.GenerateEnum(1);
            _stockRepository.Get(1).Returns(stock.FirstOrDefault());
            
            //Act
            var result = _stockRepository.Get(1);
            
            //Assert
            Assert.AreEqual(result, stock.ElementAt(0));
        }
    }
}