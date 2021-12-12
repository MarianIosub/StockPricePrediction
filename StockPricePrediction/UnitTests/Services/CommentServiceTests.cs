using System;
using System.Linq;
using DomainLayer;
using NSubstitute;
using NUnit.Framework;
using RepositoryLayer;
using ServiceLayer;
using ServiceLayer.Models;
using UnitTests.Generators;

namespace UnitTests
{
    [TestFixture]
    public class CommentServiceTests
    {
        private ICommentService _service;
        private ICommentRepository _repository;
        private IStockRepository _stockRepository;
        private IGenerator<Comment> _generator;
        private IGenerator<Stock> _stockGenerator;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<ICommentRepository>();
            _stockRepository = Substitute.For<IStockRepository>();
            _service = new CommentService(_repository, _stockRepository);
            _generator = new CommentGenerator();
            _stockGenerator = new StockGenerator();
        }

        [Test]
        public void Get_All_Comments_Should_Be_Have_ApiSuccess_And_NotEmpty()
        {
            //Arrange
            var generatedStock = _stockGenerator.GenerateEnum(1).FirstOrDefault();
            _stockRepository.GetBySymbol(generatedStock.Symbol).Returns(generatedStock);
            var stock = _stockRepository.GetBySymbol(generatedStock.Symbol);
            _repository.GetAll(stock).Returns(stock.Comments);
            //Act
            var result = _service.GetAllComments(stock.Symbol);
            //Assert
            Assert.IsTrue(result.Succeed);
            Assert.IsNotEmpty(result.Data);
        }

        [TestCase(-1)]
        [TestCase(1)]
        public void Get_Comment_Should_Have_ApiSuccess_If_Id_IsValid(int id)
        {
            //Arrange
            var comment = _generator.GenerateEnum(1).FirstOrDefault();
            _repository.Get(id).Returns(comment);
            //Act 
            var result = _service.GetComment(id);
            //Arrange

            Assert.IsTrue(result.Succeed);
            Assert.AreEqual(result.Data, comment);
        }

        [Test]
        public void UpdateComment_Should_Have_ApiResponse_True_If_Comment_IsValid()
        {
            //Arrange
            var comment = _generator.GenerateEnum(1).FirstOrDefault();
            _repository.Get(comment.Id).Returns(comment);
            //Act
            var result = _service.UpdateComment(comment);

            //Assert
            Assert.IsTrue(result.Succeed);
        }

        [TestCase("ABC", "ABC", "ABC")]
        public void InsertComment_Should_Have_ApiResponse_True_If_Comment_IsValid(string author, string message,
            string stockSymbol)
        {
            var date = DateTime.Now;
            //Act
            var result = _service.InsertComment(author, message, stockSymbol, date);
            //Assert
            Assert.IsTrue(result.Succeed);
        }

        [Test]
        public void DeleteComment_Should_Have_ApiResponse_True()
        {
            //Assert
            var comment = _generator.GenerateEnum(1).FirstOrDefault();
            _repository.Get(comment.Id).Returns(comment);
            //Act
            var result = _service.DeleteComment(comment.Id);
            //Assert
            Assert.IsTrue(result.Succeed);
        }

        [Test]
        public void Upvote_Should_Have_ApiResponse_True()
        {
            //Assert
            var comment = _generator.GenerateEnum(1).FirstOrDefault();
            _repository.Get(comment.Id).Returns(comment);
            //Act
            var result = _service.Upvote(comment.Id);
            //Assert
            Assert.IsTrue(result.Succeed);
        }
        [Test]
        public void Downvote_Should_Have_ApiResponse_True()
        {
            //Assert
            var comment = _generator.GenerateEnum(1).FirstOrDefault();
            _repository.Get(comment.Id).Returns(comment);
            //Act
            var result = _service.Downvote(comment.Id);
            //Assert
            Assert.IsTrue(result.Succeed);
        }
        
    }
}