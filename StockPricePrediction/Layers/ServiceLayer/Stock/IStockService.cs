using System.Collections.Generic;
using DomainLayer;
using ServiceLayer.Models;


namespace ServiceLayer
{
    public interface IStockService
    {
        ApiResponse<IEnumerable<Stock>> GetAllStocks();
        ApiResponse<Stock> GetStock(int id);
        ApiResponse<Stock> GetStock(string stockSymbol);
        ApiResponse<bool> InsertStock(Stock stock);
        ApiResponse<bool> UpdateStock(Stock stock);
        ApiResponse<bool> DeleteStock(int id);
    }
}