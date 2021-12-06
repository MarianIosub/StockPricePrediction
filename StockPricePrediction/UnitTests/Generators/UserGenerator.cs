using System;
using System.Collections.Generic;
using DomainLayer;

namespace UnitTests.Generators
{
    public class UserGenerator :IGenerator<User>
    {
        private readonly Random _random;
        
        public UserGenerator()
        {
            _random = new Random();
        }

        public IEnumerable<User> GenerateEnum(int count)
        {
            var users = new List<User>();
            for (var i = 0; i < count; i++)
            {
                var user = new User
                {
                    Id = _random.Next(1,200),
                    Firstname = "Firstname",
                    Lastname = "Lastname",
                    Email = "Email",
                    Password = "password",
                    CreationDate = DateTime.Now,
                    UserStocks = new List<UserStocks>()
                };
                users.Add(user);
            }

            return users;
        }
    }
}