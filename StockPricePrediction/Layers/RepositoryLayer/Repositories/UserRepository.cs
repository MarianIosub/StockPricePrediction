using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using Microsoft.Data.SqlClient;
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

        public User Get(int id)
        {
            return _entities.SingleOrDefault(c => c.Id == id);
        }

        public User GetByEmail(string email)
        {
            return _entities.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public bool Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                _entities.Add(entity);
                _appDbContext.SaveChanges();
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        public void Remove(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Remove(entity);
        }

        public IEnumerable<Stock> GetFavouriteStocks(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return entity.FavouriteStocks;
        }

        public void AddFavouriteStock(User entity, Stock stock)
        {
            if (entity == null || stock == null)
            {
                throw new ArgumentNullException("entity");
            }

            entity.FavouriteStocks.Append(stock);
            _appDbContext.SaveChanges();
        }

        public void RemoveFavouriteStock(User entity, int stockId)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            foreach (Stock stock in entity.FavouriteStocks)
            {
                if (stock.Id == stockId)
                {
                    entity.FavouriteStocks.Remove(stock);
                    break;
                }
            }

            _appDbContext.SaveChanges();
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