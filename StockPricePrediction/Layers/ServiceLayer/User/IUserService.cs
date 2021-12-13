using System.Collections.Generic;
using DomainLayer;
using ServiceLayer.Models;

namespace ServiceLayer
{
    public interface IUserService
    {
        ApiResponse<IEnumerable<User>> GetAllUsers();
        ApiResponse<User> GetUser(int id);
        ApiResponse<bool> InsertUser(User user);
        ApiResponse<bool> UpdateUser(User user);
        ApiResponse<bool> DeleteUser(int id);
        ApiResponse<bool> Exists(User user);
        public ApiResponse<IEnumerable<Stock>> GetFavouriteStocks(User user);
        ApiResponse<bool> RemoveFavouriteStock(User user, string stockSymbol);
        ApiResponse<bool> AddFavouriteStock(User user, string stockSymbol);
        ApiResponse<int?> ValidateUser(string token);
        ApiResponse<UserResponseModel> Authenticate(AuthenticateModel authenticateModel);
    }
}