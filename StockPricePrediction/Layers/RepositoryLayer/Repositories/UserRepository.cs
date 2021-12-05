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
        private DbSet<UserStocks> _eUserStocks;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _eUserStocks = _appDbContext.Set<UserStocks>();
            _entities = _appDbContext.Set<User>();
            _entities.Include(u => u.UserStocks);
        }

        public void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
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
                throw new ArgumentNullException(nameof(entity));
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
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public IEnumerable<int> GetFavouriteStocks(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            // _eUserStocks.Include(u => u).Where(u => u.UserId == entity.Id);
            return _eUserStocks.Where(u => u.UserId == entity.Id).Select(u => u.StockId).ToList();
        }

        public void AddFavouriteStock(User entity, Stock stock)
        {
            if (entity == null || stock == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var userStocks = new UserStocks();
            userStocks.StockId = stock.Id;
            userStocks.UserId = entity.Id;
            if (entity.UserStocks == null)
            {
                entity.UserStocks = new List<UserStocks>();
            }

            entity.UserStocks.Add(userStocks);
            _appDbContext.SaveChanges();
        }

        public void RemoveFavouriteStock(User entity, int stockId)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            foreach (UserStocks stock in entity.UserStocks)
            {
                if (stock.StockId == stockId)
                {
                    entity.UserStocks.Remove(stock);
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
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
            _appDbContext.SaveChanges();
        }
    }
}