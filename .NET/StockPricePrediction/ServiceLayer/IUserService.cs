﻿using System.Collections.Generic;
using DomainLayer;

namespace ServiceLayer
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();  
        User GetUser(int id);  
        void InsertUser(User user);  
        void UpdateUser(User user);  
        void DeleteUser(int id);  
    }
}