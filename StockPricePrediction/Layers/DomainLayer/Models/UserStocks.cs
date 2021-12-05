namespace DomainLayer
{
    public class UserStocks
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int StockId { get; set; }
        public Stock Stock { get; set; }
    }
}