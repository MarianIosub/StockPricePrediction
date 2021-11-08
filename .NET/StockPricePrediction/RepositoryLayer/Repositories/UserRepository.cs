using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<User> _entities;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<User>();
        }

        public void Delete(User entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            _entities.Remove(entity);  
            _appDbContext.SaveChanges();  
        }  
  
        public User Get(int Id)  
        {  
            return _entities.SingleOrDefault(c => c.Id == Id);  
        }  
  
        public IEnumerable<User> GetAll()  
        {  
            return _entities.AsEnumerable();  
        }  
  
        public void Insert(User entity)  
        {  
            if (entity == null)  
            {  
                throw new ArgumentNullException("entity");  
            }  
            _entities.Add(entity);  
            _appDbContext.SaveChanges();  
        }  
  
        public void Remove(User entity)  
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
  
        public void Update(User entity)  
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