using System.Linq;
using AutoMapper;
using DomainLayer;
using NSubstitute;
using NUnit.Framework;
using RepositoryLayer;
using ServiceLayer;
using ServiceLayer.Mapper;
using UnitTests.Generators;

namespace UnitTests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService _service;
        private IUserRepository _repository;
        private IStockRepository _stockRepository;
        private IGenerator<User> _generator;

        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(typeof(UserMapper)));
            _repository = Substitute.For<IUserRepository>();
            _stockRepository = Substitute.For<IStockRepository>();
            _service = new UserService(_repository, new Mapper(configuration), _stockRepository);
            _generator = new UserGenerator();
        }

        [Test]
        public void GetAllUsers_Should_Return_A_Specific_Number()
        {
            //Arrange
            var users = _generator.GenerateEnum(10);
            _repository.GetAll().Returns(users);
            //Act
            var results = _service.GetAllUsers().Data;
            //Assert
            Assert.AreEqual(results,users);
        }

        [Test]
        public void Insert_Should_Return_False()
        {
            //Arrange
            var user = _generator.GenerateEnum(1).First();
            _repository.Insert(user).Returns(false);
            //Act   
            var result = _service.InsertUser(user);
            //Assert
            Assert.False(result.Succeed);
        }

        [Test]
        public void UpdateUser_Should_Return_ApiResponseTrue()
        {
            //Arrange
            var user = _generator.GenerateEnum(1).First();
            _repository.Get(user.Id).Returns(user);
            //Act
            var result = _service.UpdateUser(user);
            //Assert
            Assert.True(result.Succeed);
        }

        [Test]
        public void DeleteUser_Should_Return_ApiResponseTrue()
        {
            //Arrange
            var user = _generator.GenerateEnum(1).First();
            _repository.Get(user.Id).Returns(user);
            //Act
            var result = _service.DeleteUser(user.Id);
            //Assert
            Assert.True(result.Succeed);
        }

        [Test]
        public void Exists_Should_Return_ApiResponseTrue_If_User_Exists()
        {
            //Arrange
            var user = _generator.GenerateEnum(1).First();
            _repository.GetByEmail(user.Email).Returns(user);
            //Act
            var result = _service.Exists(user);
            //Assert
            Assert.True(result.Succeed);
        }
        
    }
}