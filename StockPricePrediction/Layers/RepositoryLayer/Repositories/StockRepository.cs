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
                throw new ArgumentNullException("entity");
            }

            _entities.Remove(entity);
            _appDbContext.SaveChanges();
        }

        public Stock Get(int id)
        {
            return _entities.SingleOrDefault(c => c.Id == id);
        }

        public IEnumerable<Stock> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public void Insert(Stock entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _entities.Add(entity);
            _appDbContext.SaveChanges();
        }

        public void Remove(Stock entity)
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

        public void Update(Stock entity)
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