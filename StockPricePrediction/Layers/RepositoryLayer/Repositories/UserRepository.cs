using System;
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
        private readonly DbSet<User> _entities;
        private readonly DbSet<UserStocks> _eUserStocks;

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
                Console.WriteLine(e.Message);
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

            return _eUserStocks.Where(u => u.UserId == entity.Id).Select(u => u.StockId).ToList();
        }

        public void AddFavouriteStock(User entity, Stock stock)
        {
            if (entity == null || stock == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var userStocks = new UserStocks
            {
                StockId = stock.Id,
                UserId = entity.Id
            };
            entity.UserStocks ??= new List<UserStocks>();

            entity.UserStocks.Add(userStocks);
            _appDbContext.SaveChanges();
        }

        public void RemoveFavouriteStock(User entity, Stock stock)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Include(u => u.UserStocks).Load();

            foreach (var userStocks in entity.UserStocks)
            {
                if (userStocks.StockId != stock.Id) continue;
                entity.UserStocks.Remove(userStocks);
                break;
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