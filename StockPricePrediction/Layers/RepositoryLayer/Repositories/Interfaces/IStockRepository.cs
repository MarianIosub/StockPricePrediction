using System.Collections.Generic;
using DomainLayer;


namespace RepositoryLayer
{
    public interface IStockRepository
    {
        IEnumerable<Stock> GetAll();
        Stock Get(int id);
        Stock GetBySymbol(string symbol);
        void Insert(Stock entity);
        void Update(Stock entity);
        void Delete(Stock entity);
        void Remove(Stock entity);
        void SaveChanges();
        void AddComment(Comment entity, string symbol);
    }
}