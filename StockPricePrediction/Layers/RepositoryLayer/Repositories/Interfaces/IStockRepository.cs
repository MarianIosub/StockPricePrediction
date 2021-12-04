﻿using System.Collections.Generic;
using DomainLayer;


namespace RepositoryLayer
{
    public interface IStockRepository
    {
        IEnumerable<Stock> GetAll();
        Stock Get(int id);
        void Insert(Stock entity);
        void Update(Stock entity);
        void Delete(Stock entity);
        void Remove(Stock entity);
        void SaveChanges();
        void AddComment(Comment comment, int id);
    }
}