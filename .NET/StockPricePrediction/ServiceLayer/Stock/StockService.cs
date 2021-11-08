using System.Collections.Generic;
using DomainLayer;
using RepositoryLayer;

namespace ServiceLayer
{
    public class StockService:IStockService
    {
        #region Property

        private IStockRepository _repository;

        #endregion

        #region Constructor

        public StockService(IStockRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public IEnumerable<Stock> GetAllStocks()
        {
            return _repository.GetAll();
        }

        public Stock GetStock(int id)
        {
            return _repository.Get(id);
        }

        public void InsertStock(Stock Stock)
        {
            _repository.Insert(Stock);
        }

        public void UpdateStock(Stock Stock)
        {
            _repository.Update(Stock);
        }

        public void DeleteStock(int id)
        {
            Stock Stock = GetStock(id);
            _repository.Remove(Stock);
            _repository.SaveChanges();
        }
    }
}