using System.Collections.Generic;

namespace DomainLayer
{
    public class Stock
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Symbol { get; set; }
        public ICollection<UserStocks> UsersStocks { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}