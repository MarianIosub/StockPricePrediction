using System.Collections.Generic;
using DomainLayer;

namespace RepositoryLayer
{
    public interface IStockRepository
    {
        IEnumerable<Stock> GetAll();
        Stock Get(int Id);
        void Insert(Stock entity);
        void Update(Stock entity);
        void Delete(Stock entity);
        void Remove(Stock entity);
        void SaveChanges();
    }
}