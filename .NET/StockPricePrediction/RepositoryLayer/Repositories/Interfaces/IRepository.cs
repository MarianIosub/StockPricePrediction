using System.Collections.Generic;
using DomainLayer;

namespace RepositoryLayer
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int Id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Remove(T entity);
        void SaveChanges();
    }
}