using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class Repository<T> : IRepository<T> where T : User
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<T> _entities;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<T>();
        }

        public void Delete(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            _entities.Remove(entity);  
            _appDbContext.SaveChanges();  
        }  
  
        public T Get(int Id)  
        {  
            return _entities.SingleOrDefault(c => c.Id == Id);  
        }  
  
        public IEnumerable<T> GetAll()  
        {  
            return _entities.AsEnumerable();  
        }  
  
        public void Insert(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            _entities.Add(entity);  
            _appDbContext.SaveChanges();  
        }  
  
        public void Remove(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            _entities.Remove(entity);  
        }  
  
        public void SaveChanges()  
        {  
            _appDbContext.SaveChanges();  
        }  
  
        public void Update(T entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            _entities.Update(entity);  
            _appDbContext.SaveChanges();  
        }  
    }
}