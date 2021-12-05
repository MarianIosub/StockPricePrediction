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
        public IEnumerable<Stock>GetFavouriteStocks(User user);
        void RemoveFavouriteStock(User user, int stockId);
        void AddFavouriteStock(User user, int stockId);
        bool ValidateUser(string token);
        UserResponseModel Authenticate(AuthenticateModel authenticateModel);
    }
}