using System.Linq;
using NUnit.Framework;
using RepositoryLayer;
using ServiceLayer;

namespace UnitTests
{
    [TestFixture]
    public class UserServiceTest
    {
        private UserService _service;
        private IUserRepository _repository;
        
        [SetUp]
        public void Setup()
        {
            _service = new UserService(_repository);
        }

        [Test]
        public void ShouldNotBeEmpty()
        {
            var results = _service.GetAllUsers();
            Assert.NotZero(results.Count());
        }

        [TestCase(1)]
        public void ShouldDeleteAnEntry(int nr)
        {
            var expected = _service.GetAllUsers().Count();
            _service.DeleteUser(nr);
            var actual = _service.GetAllUsers().Count();
            Assert.Equals(expected, actual);
        }

        [TestCase(1)]
        public void ShouldHaveTheSameNumber(int nr)
        {
            var user = _service.GetUser(nr);
            Assert.Equals(user.Id, nr);
        }
    
        
        
        
    }
}