using DomainLayer;

namespace ServiceLayer.Models
{
    public class UserStockModel
    {
        public Stock Stock;
        public bool IsFavourite;

        public UserStockModel(Stock stock, bool isFavourite)
        {
            Stock = stock;
            IsFavourite = isFavourite;
        }

        public override string ToString()
        {
            return "{\n" + Stock + ",\n\"IsFavourite\" : " + IsFavourite + ",\n}";
        }
    }
}