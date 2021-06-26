
using System.Threading.Tasks;

namespace Inventory_Management.Server.Data
{
    public interface IProductRepository
    {
        //Product
        Task<Product[]> GetProductsAync();
        Task<Product> GetProductByIDAsync(int Id);
        Task<bool> DeleteProductByIDAsync(int Id);
        Task<Product> AddProductAsync(Product newItem);
        Task<Product> UpdateProductAsync(Product changes);
    }
}
