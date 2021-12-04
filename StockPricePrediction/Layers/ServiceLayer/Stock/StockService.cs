using System.Collections.Generic;
using DomainLayer;
using RepositoryLayer;


namespace ServiceLayer
{
    public class StockService : IStockService
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

        public void AddCommentToStock(Comment comment,int stockId)
        {
            _repository.AddComment(comment,stockId);
        }

        public Stock GetStock(int id)
        {
            return _repository.Get(id);
        }

        public void InsertStock(Stock stock)
        {
            _repository.Insert(stock);
        }

        public void UpdateStock(Stock stock)
        {
            _repository.Update(stock);
        }

        public void DeleteStock(int id)
        {
            Stock stock = GetStock(id);
            _repository.Remove(stock);
            _repository.SaveChanges();
        }
    }
}