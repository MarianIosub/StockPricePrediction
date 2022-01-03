using DomainLayer;

namespace ServiceLayer.Models
{
    public class UserStockModel
    {
        public Stock Stock { get; set; }
        public bool IsFavourite { get; set; }

        public UserStockModel(Stock stock, bool isFavourite)
        {
            Stock = stock;
            IsFavourite = isFavourite;
        }
    }
}