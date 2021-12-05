using System.Collections.Generic;
using DomainLayer;


namespace ServiceLayer
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUser(int id);
        bool InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool Exists(User user);
        public IEnumerable<Stock> GetFavouriteStocks(User user);
        void RemoveFavouriteStock(User user, string stockSymbol);
        void AddFavouriteStock(User user, string stockSymbol);
        int? ValidateUser(string token);
        UserResponseModel Authenticate(AuthenticateModel authenticateModel);
    }
}