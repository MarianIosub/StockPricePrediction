﻿using System.Collections.Generic;
using DomainLayer;

namespace RepositoryLayer
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int Id);
        User GetByEmail(string email);
        bool Insert(User entity);
        void Update(User entity);
        void Delete(User entity);
        void Remove(User entity);
        void SaveChanges();
    }
}