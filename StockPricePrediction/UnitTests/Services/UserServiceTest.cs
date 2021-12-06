using System;
using System.Linq;
using AutoMapper;
using DomainLayer;
using NSubstitute;
using NUnit.Framework;
using RepositoryLayer;
using ServiceLayer;
using ServiceLayer.Mapper;
using UnitTests.Generators;

namespace UnitTests
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService _service;
        private IUserRepository _repository;
        private IMapper _mapper;
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
            var results = _service.GetAllUsers();
            //Assert
            Assert.AreEqual(results,users);
        }

        [Test]
        public void Insert_Should_Return_False()
        {
            //Arrange
            var user = _generator.GenerateEnum(1).FirstOrDefault();
            _repository.Insert(user).Returns(true);
            //Act   
            Console.WriteLine(user.Id);
            var result = _repository.Insert(user);
            //Asert
            Assert.True(result);
        }

        
    }
}