using System;
using System.Collections.Generic;
using System.Linq;
using DomainLayer;
using Microsoft.EntityFrameworkCore;


namespace RepositoryLayer
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<Stock> _entities;


        public StockRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _entities = _appDbContext.Set<Stock>();
        }

        public void Delete(Stock entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public Stock Get(int id)
        {
            return _entities.SingleOrDefault(c => c.Id == id);
        }

        public Stock GetBySymbol(string symbol)
        {
            return _entities.SingleOrDefault(c => c.Symbol == symbol);
        }

        public IEnumerable<Stock> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public void Insert(Stock entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Remove(Stock entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Stock entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Update(entity);
            _appDbContext.SaveChanges();
        }

        public void AddComment(Comment entity, string stockSymbol)
        {
            var stock = _entities.SingleOrDefault(c => c.Symbol == stockSymbol);
            if (stock == null || entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (stock.Comments == null)
            {
                stock.Comments = new List<Comment>();
            }

            stock.Comments.Add(entity);
            _appDbContext.SaveChanges();
        }
    }
}