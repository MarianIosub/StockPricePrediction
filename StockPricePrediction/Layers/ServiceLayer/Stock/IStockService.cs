using System.Collections.Generic;
using DomainLayer;

namespace ServiceLayer
{
    public interface IStockService
    {
        IEnumerable<Stock> GetAllStocks();  
        Stock GetStock(int id);  
        void InsertStock(Stock stock);  
        void UpdateStock(Stock stock);  
        void DeleteStock(int id);  
        
    }
}