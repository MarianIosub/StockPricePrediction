using System.Collections.Generic;
using DomainLayer;
using RepositoryLayer;
using ServiceLayer.Models;


namespace ServiceLayer
{
    public class StockService : IStockService
    {
        #region Property

        private readonly IStockRepository _repository;

        #endregion

        #region Constructor

        public StockService(IStockRepository repository)
        {
            _repository = repository;
        }

        #endregion

        public ApiResponse<IEnumerable<Stock>> GetAllStocks()
        {
            return ApiResponse<IEnumerable<Stock>>.Success(_repository.GetAll());
        }

        public ApiResponse<Stock> GetStock(int id)
        {
            return id < 1 ? ApiResponse<Stock>.Fail("Invalid id") : ApiResponse<Stock>.Success(_repository.Get(id));
        }

        public ApiResponse<Stock> GetStock(string stockSymbol)
        {
            return stockSymbol == null
                ? ApiResponse<Stock>.Fail("Invalid stock symbol")
                : ApiResponse<Stock>.Success(_repository.GetBySymbol(stockSymbol));
        }

        public ApiResponse<bool> InsertStock(Stock stock)
        {
            if (stock == null)
            {
                return ApiResponse<bool>.Fail("Invalid stock");
            }

            _repository.Insert(stock);
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> UpdateStock(Stock stock)
        {
            if (stock == null)
            {
                return ApiResponse<bool>.Fail("Invalid stock");
            }

            _repository.Update(stock);
            return ApiResponse<bool>.Success(true);
        }

        public ApiResponse<bool> DeleteStock(int id)
        {
            var stock = GetStock(id).Data;
            if (stock == null)
            {
                return ApiResponse<bool>.Fail("Invalid id stock");
            }

            _repository.Remove(stock);
            _repository.SaveChanges();
            return ApiResponse<bool>.Success(true);
        }
    }
}