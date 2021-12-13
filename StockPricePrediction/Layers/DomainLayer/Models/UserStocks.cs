using System.Reflection;

[assembly: AssemblyTitle("RepositoryLayerAssembly")]
[assembly: AssemblyVersion("1.0")]

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