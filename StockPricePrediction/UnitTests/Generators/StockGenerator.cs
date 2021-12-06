using System;
using System.Collections.Generic;
using DomainLayer;

namespace UnitTests.Generators
{
    public class StockGenerator: IGenerator<Stock>
    {
        private readonly Random _random;
        public StockGenerator()
        {
            _random = new Random();
        }
        public IEnumerable<Stock> GenerateEnum(int count)
        {
            var userStocks = new List<UserStocks>();
            var comments = new List<Comment>();
            var stocks = new List<Stock>();
            for (var i = 1; i <= count; i++)
            {
                var stock = new Stock
                {
                    Id = _random.Next(1, 200),
                    Symbol = "ABC",
                    Title = "ABC",
                    UsersStocks = userStocks,
                    Comments = comments
                };
                stocks.Add(stock);
            }

            return stocks;
        }
    }
}