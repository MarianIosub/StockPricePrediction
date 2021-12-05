using System.Collections.Generic;
using DomainLayer;


namespace RepositoryLayer
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        User GetByEmail(string email);
        bool Insert(User entity);
        void Update(User entity);
        void Delete(User entity);
        void Remove(User entity);
        IEnumerable<int> GetFavouriteStocks(User entity);
        void AddFavouriteStock(User entity, Stock stock);
        void RemoveFavouriteStock(User entity, Stock stock);
        void SaveChanges();
    }
}