using System.Collections.Generic;
using DomainLayer;
using ServiceLayer.Models;


namespace ServiceLayer
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();  
        User GetUser(int id);  
        bool InsertUser(User user);  
        void UpdateUser(User user);  
        void DeleteUser(int id);
        UserResponseModel Authenticate(AuthenticateModel authenticateModel);

    }
}